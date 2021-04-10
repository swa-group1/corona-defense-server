// <copyright file="API.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using API.Requests;
using API.Schemas;
using System;

namespace API
{
  /// <summary>
  /// Receives requests from clients through a REST-api and exposes such events to attached <see cref="IObserver{T}"/>s.
  /// </summary>
  public class API
  {
    private IRequestHandler<ActivateClientRequest, RequestResult> activateClientHandler;
    private IRequestHandler<CreateLobbyRequest, CreateLobbyResult> createLobbyHandler;
    private IRequestHandler<HighScoreListResult> highScoreListHandler;
    private IRequestHandler<JoinLobbyRequest, JoinLobbyResult> joinLobbyHandler;
    private IRequestHandler<LeaveLobbyRequest, RequestResult> leaveLobbyHandler;
    private IRequestHandler<LobbyRequest, LobbyResult> lobbyHandler;
    private IRequestHandler<LobbyList> lobbyListHandler;
    private IRequestHandler<PlaceTowerRequest, RequestResult> placeTowerHandler;
    private IRequestHandler<SelltowerRequest, RequestResult> sellTowerHandler;
    private IRequestHandler<StartRoundRequest, RequestResult> startRoundHandler;
    private IRequestHandler<VerifyVersionRequest, VerifyVersionResult> verifyVersionHandler;

    /// <summary>
    /// Attach handler to the ActivateClient endpoint.
    /// </summary>
    public void AttachActivateClientHandler(IRequestHandler<ActivateClientRequest, RequestResult> handler)
    {
      this.activateClientHandler = handler;
    }

    /// <summary>
    /// Attach handler to the CreateLobby endpoint.
    /// </summary>
    public void AttachCreateLobbyHandler(IRequestHandler<CreateLobbyRequest, CreateLobbyResult> handler)
    {
      this.createLobbyHandler = handler;
    }

    /// <summary>
    /// Attach handler to the HighScoreList endpoint.
    /// </summary>
    public void AttachHighScoreListHandler(IRequestHandler<HighScoreListResult> handler)
    {
      this.highScoreListHandler = handler;
    }

    /// <summary>
    /// Attach handler to the JoinLobby endpoint.
    /// </summary>
    public void AttachJoinLobbyHandler(IRequestHandler<JoinLobbyRequest, JoinLobbyResult> handler)
    {
      this.joinLobbyHandler = handler;
    }

    /// <summary>
    /// Attach handler to the LeaveLobby endpoint.
    /// </summary>
    public void AttachLeaveLobbyHandler(IRequestHandler<LeaveLobbyRequest, RequestResult> handler)
    {
      this.leaveLobbyHandler = handler;
    }

    /// <summary>
    /// Attach handler to the Lobby endpoint.
    /// </summary>
    public void AttachLobbyHandler(IRequestHandler<LobbyRequest, LobbyResult> handler)
    {
      this.lobbyHandler = handler;
    }

    /// <summary>
    /// Attach handler to the LobbyList endpoint.
    /// </summary>
    public void AttachLobbyListHandler(IRequestHandler<LobbyList> handler)
    {
      this.lobbyListHandler = handler;
    }

    /// <summary>
    /// Attach handler to the PlaceTower endpoint.
    /// </summary>
    public void AttachPlaceTowerHandler(IRequestHandler<PlaceTowerRequest, RequestResult> handler)
    {
      this.placeTowerHandler = handler;
    }

    /// <summary>
    /// Attach handler to the SellTower endpoint.
    /// </summary>
    public void AttachSellTowerHHandler(IRequestHandler<SelltowerRequest, RequestResult> handler)
    {
      this.sellTowerHandler = handler;
    }

    /// <summary>
    /// Attach handler to the StartRound endpoint.
    /// </summary>
    public void AttachStartRoundHandler(IRequestHandler<StartRoundRequest, RequestResult> handler)
    {
      this.startRoundHandler = handler;
    }

    /// <summary>
    /// Attach handler to the VerifyVersion endpoint.
    /// </summary>
    public void AttachVerifyVersionHandler(IRequestHandler<VerifyVersionRequest, VerifyVersionResult> handler)
    {
      this.verifyVersionHandler = handler;
    }
  }
}
