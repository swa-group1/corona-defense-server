// <copyright file="Orchestrator.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using API;
using API.Requests;
using API.Schemas;
using System.Collections.Generic;

namespace BackEnd
{
  /// <summary>
  /// A class describing a back-end orchestrator that handles global client-requests that do not concern a specific <see cref="Lobby"/>s. This includes creating new <see cref="Lobby"/>s.
  /// </summary>
  internal class Orchestrator :
    IRequestHandler<CreateLobbyRequest, CreateLobbyResult>,
    IRequestHandler<HighScoreListResult>,
    IRequestHandler<LobbyRequest, LobbyResult>,
    IRequestHandler<LobbyList>,
    IRequestHandler<VerifyVersionRequest, VerifyVersionResult>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Orchestrator"/> class.
    /// </summary>
    internal Orchestrator()
    {
      API.API.Instance.AttachCreateLobbyHandler(this);
      API.API.Instance.AttachHighScoreListHandler(this);
      API.API.Instance.AttachLobbyHandler(this);
      API.API.Instance.AttachLobbyListHandler(this);
      API.API.Instance.AttachVerifyVersionHandler(this);
    }

    /// <summary>
    /// Method that processes create lobby request.
    /// </summary>
    /// <param name="request">The <see cref="CreateLobbyRequest"/>.</param>
    /// <returns>The <see cref="CreateLobbyResult"/>.</returns>
    CreateLobbyResult IRequestHandler<CreateLobbyRequest, CreateLobbyResult>.ProcessRequest(CreateLobbyRequest request)
    {
      return new CreateLobbyResult()
      {
        Details = "Lobby was created.",

        LobbyId = 1,
      };
    }

    /// <summary>
    /// Method that processes high score list retrieval request.
    /// </summary>
    /// <returns>The <see cref="HighScoreListResult"/></returns>
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
      return new LobbyResult()
      {
        Lobby = new API.Schemas.Lobby()
        {
          Id = request.Id,
          Name = "Tarjeis Lobby",
          PlayerCount = 99,
        },
      };
    }

    /// <summary>
    /// Method that processes lobby list retrieval request.
    /// </summary>
    /// <returns>The <see cref="LobbyList"/>.</returns>
    LobbyList IRequestHandler<LobbyList>.ProcessRequest()
    {
      return new LobbyList()
      {
        Lobbies = new List<API.Schemas.Lobby>()
        {
          new API.Schemas.Lobby() { Id = 1L, Name = "Tarjeis Lobby", PlayerCount = 99, },
          new API.Schemas.Lobby() { Id = 2L, Name = "Sultans Lobby", PlayerCount = -1, },
        },
      };
    }

    /// <summary>
    /// Method that processes verify version request.
    /// </summary>
    /// <param name="request">The <see cref="VerifyVersionRequest"/>.</param>
    /// <returns>The <see cref="VerifyVersionResult"/>.</returns>
    VerifyVersionResult IRequestHandler<VerifyVersionRequest, VerifyVersionResult>.ProcessRequest(VerifyVersionRequest request)
    {
      return new VerifyVersionResult() {
        ValidVersion = true,
      };
    }
  }
}
