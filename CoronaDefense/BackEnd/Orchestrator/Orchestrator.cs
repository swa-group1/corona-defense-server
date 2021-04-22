// <copyright file="Orchestrator.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Communication;
using BackEnd.Communication.API;
using BackEnd.Communication.API.Requests;
using BackEnd.Communication.API.Schemas;
using BackEnd.Game;
using System.Collections.Generic;
using System.Linq;

namespace BackEnd.Orchestrator
{
  /// <summary>
  /// A class describing a back-end orchestrator that handles global client-requests that do not concern a specific <see cref="Lobby"/>s. This includes creating new <see cref="Lobby"/>s.
  /// </summary>
  /// <remarks>
  /// This object is part of the Orchestrator Layer – Layer 4.
  /// </remarks>
  internal class Orchestrator :
    IRequestHandler<CreateLobbyRequest, CreateLobbyResult>,
    IRequestHandler<HighScoreListResult>,
    IRequestHandler<LobbyRequest, LobbyResult>,
    IRequestHandler<LobbyListResult>,
    IRequestHandler<VerifyVersionRequest, VerifyVersionResult>,
    Game.Lobby.IObserver
  {
    /// <summary>
    /// Gets the <see cref="ConnectionBroker"/> that lobbies created by this <see cref="Orchestrator"/> should request sockets from.
    /// </summary>
    private ConnectionBroker ConnectionBroker { get; }

    /// <summary>
    /// Gets lobbies on this server, associated with their addresses.
    /// </summary>
    private Dictionary<long, Game.Lobby> Lobbies { get; } = new Dictionary<long, Game.Lobby>();

    /// <summary>
    /// Gets router to connect new lobbies to.
    /// </summary>
    private Router.Router Router { get; }

    private HashSet<string> VersionsInvalid { get; } = new HashSet<string>()
    {
    };

    private HashSet<string> VersionsValid { get; } = new HashSet<string>()
    {
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="Orchestrator"/> class.
    /// </summary>
    /// <param name="connectionBroker">The <see cref="ConnectionBroker"/> that lobbies created by this <see cref="Orchestrator"/> should request sockets from.</param>
    /// <param name="router">Router to connect new lobbies to.</param>
    internal Orchestrator(ConnectionBroker connectionBroker, Router.Router router)
    {
      this.ConnectionBroker = connectionBroker;
      this.Router = router;

      API.Instance.AttachCreateLobbyHandler(this);
      API.Instance.AttachHighScoreListHandler(this);
      API.Instance.AttachLobbyHandler(this);
      API.Instance.AttachLobbyListHandler(this);
      API.Instance.AttachVerifyVersionHandler(this);
    }

    /// <summary>
    /// Method that processes create lobby request.
    /// </summary>
    /// <param name="request">The <see cref="CreateLobbyRequest"/>.</param>
    /// <returns>The <see cref="CreateLobbyResult"/>.</returns>
    CreateLobbyResult IRequestHandler<CreateLobbyRequest, CreateLobbyResult>.ProcessRequest(CreateLobbyRequest request)
    {
      Game.Lobby lobby = new Game.Lobby(request.Name, request.Password, this.ConnectionBroker, this.Router);
      this.Lobbies.Add(lobby.Id, lobby);
      lobby.AddObserver(this);

      return new CreateLobbyResult()
      {
        Success = true,
        Details = $"Lobby with name {lobby.Name} was created.",

        LobbyId = lobby.Id,
      };
    }

    /// <summary>
    /// Method that processes high score list retrieval request.
    /// </summary>
    /// <returns>The <see cref="HighScoreListResult"/>.</returns>
    HighScoreListResult IRequestHandler<HighScoreListResult>.ProcessRequest()
    {
      return new HighScoreListResult()
      {
        Scores = new List<HighScoreListResult.Score>()
        {
          new HighScoreListResult.Score()
          {
            Name = "Laonard",
            Value = 100,
          },
          new HighScoreListResult.Score()
          {
            Name = "Maka",
            Value = 99,
          },
        },
      };
    }

    /// <summary>
    /// Method that processes lobby retrieval request.
    /// </summary>
    /// <param name="request">The <see cref="LobbyRequest"/>.</param>
    /// <returns>The <see cref="LobbyResult"/>.</returns>
    LobbyResult IRequestHandler<LobbyRequest, LobbyResult>.ProcessRequest(LobbyRequest request)
    {
      if (!this.Lobbies.TryGetValue(request.Id, out Game.Lobby lobby))
      {
        return new LobbyResult()
        {
          Success = false,
          Details = "Could not find lobby with id: " + request.Id.ToString(),
        };
      }

      return new LobbyResult()
      {
        Lobby = new Communication.API.Schemas.Lobby()
        {
          Id = lobby.Id,
          Name = lobby.Name,
          PlayerCount = lobby.PlayerCount,
        },
      };
    }

    /// <summary>
    /// Method that processes lobby list retrieval request.
    /// </summary>
    /// <returns>The <see cref="LobbyListResult"/>.</returns>
    LobbyListResult IRequestHandler<LobbyListResult>.ProcessRequest()
    {
      return new LobbyListResult()
      {
        Lobbies = this.Lobbies.Values
          .Select(delegate (Game.Lobby lobby) { return new Communication.API.Schemas.Lobby() { Id = lobby.Id, Name = lobby.Name, PlayerCount = lobby.PlayerCount, }; })
          .ToList(),
      };
    }

    /// <summary>
    /// Method that processes verify version request.
    /// </summary>
    /// <param name="request">The <see cref="VerifyVersionRequest"/>.</param>
    /// <returns>The <see cref="VerifyVersionResult"/>.</returns>
    VerifyVersionResult IRequestHandler<VerifyVersionRequest, VerifyVersionResult>.ProcessRequest(VerifyVersionRequest request)
    {
      string details;
      bool valid;
      if (this.VersionsValid.Contains(request.Version))
      {
        details = "Supplied version is supported by this server.";
        valid = true;
      }
      else if (this.VersionsInvalid.Contains(request.Version))
      {
        details = "Supplied version is unsupported by this server.";
        valid = false;
      }
      else
      {
        details = "Supplied version is unrecognized by this server.";
        valid = false;
      }

      return new VerifyVersionResult()
      {
        Success = true,
        Details = details,

        ValidVersion = valid,
      };
    }

    /// <inheritdoc/>
    public void OnClose(long lobbyId)
    {
      _ = this.Lobbies.Remove(lobbyId);
    }
  }
}
