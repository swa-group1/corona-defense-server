// <copyright file="API.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Communication.API.Requests;
using BackEnd.Communication.API.Schemas;
using System;

namespace BackEnd.Communication.API
{
  /// <summary>
  /// Receives requests from clients through a REST-api and exposes such events to attached <see cref="IRequestHandler{TRequest,TResult}"/>s.
  /// </summary>
  /// <remarks>
  /// <para>This object is part of the Communication Layer – Layer 1.</para>
  /// <para>The API is an example of Deferred binding, as the methods to be called when a message in one of the 11 categories is not determined compile-time.</para>
  /// <para>The API is the only server-side singleton.</para>
  /// </remarks>
  public class API
  {
    /// <summary>
    /// Backing field for singleton.
    /// </summary>
    private static readonly Lazy<API> Lazy = new Lazy<API>(() => { return new API(); });

    /// <summary>
    /// Gets request handler for the CreateLobby endpoint.
    /// </summary>
    internal IRequestHandler<CreateLobbyRequest, CreateLobbyResult> CreateLobbyHandler { get; private set; }

    /// <summary>
    /// Gets request handler for the HighScoreList endpoint.
    /// </summary>
    internal IRequestHandler<HighScoreListResult> HighScoreListHandler { get; private set; }

    /// <summary>
    /// Gets request handler for the JoinLobby endpoint.
    /// </summary>
    internal IRequestHandler<JoinLobbyRequest, JoinLobbyResult> JoinLobbyHandler { get; private set; }

    /// <summary>
    /// Gets request handler for the LeaveLobby endpoint.
    /// </summary>
    internal IRequestHandler<LocalRequest, RequestResult> LeaveLobbyHandler { get; private set; }

    /// <summary>
    /// Gets request handler for the Lobby endpoint.
    /// </summary>
    internal IRequestHandler<LobbyRequest, LobbyResult> LobbyHandler { get; private set; }

    /// <summary>
    /// Gets request handler for the LobbyList endpoint.
    /// </summary>
    internal IRequestHandler<LobbyListResult> LobbyListHandler { get; private set; }

    /// <summary>
    /// Gets request handler for the PlaceTower endpoint.
    /// </summary>
    internal IRequestHandler<PlaceTowerRequest, RequestResult> PlaceTowerHandler { get; private set; }

    /// <summary>
    /// Gets request handler for the SellTower endpoint.
    /// </summary>
    internal IRequestHandler<SelltowerRequest, RequestResult> SellTowerHandler { get; private set; }

    /// <summary>
    /// Gets request handler for the StartGame endpoint.
    /// </summary>
    internal IRequestHandler<StartGameRequest, StartGameResult> StartGameHandler { get; private set; }

    /// <summary>
    /// Gets request handler for the StartRound endpoint.
    /// </summary>
    internal IRequestHandler<LocalRequest, RequestResult> StartRoundHandler { get; private set; }

    /// <summary>
    /// Gets request handler for the VerifyVersion endpoint.
    /// </summary>
    internal IRequestHandler<VerifyVersionRequest, VerifyVersionResult> VerifyVersionHandler { get; private set; }

    /// <summary>
    /// Gets singleton <see cref="API"/> instance.
    /// </summary>
    public static API Instance
    {
      get { return Lazy.Value; }
    }

    private API()
    {
    }

    /// <summary>
    /// Attach handler to the CreateLobby endpoint.
    /// </summary>
    /// <param name="handler">Handler to attach.</param>
    public void AttachCreateLobbyHandler(IRequestHandler<CreateLobbyRequest, CreateLobbyResult> handler)
    {
      this.CreateLobbyHandler = handler;
    }

    /// <summary>
    /// Attach handler to the HighScoreList endpoint.
    /// </summary>
    /// <param name="handler">Handler to attach.</param>
    public void AttachHighScoreListHandler(IRequestHandler<HighScoreListResult> handler)
    {
      this.HighScoreListHandler = handler;
    }

    /// <summary>
    /// Attach handler to the JoinLobby endpoint.
    /// </summary>
    /// <param name="handler">Handler to attach.</param>
    public void AttachJoinLobbyHandler(IRequestHandler<JoinLobbyRequest, JoinLobbyResult> handler)
    {
      this.JoinLobbyHandler = handler;
    }

    /// <summary>
    /// Attach handler to the LeaveLobby endpoint.
    /// </summary>
    /// <param name="handler">Handler to attach.</param>
    public void AttachLeaveLobbyHandler(IRequestHandler<LocalRequest, RequestResult> handler)
    {
      this.LeaveLobbyHandler = handler;
    }

    /// <summary>
    /// Attach handler to the Lobby endpoint.
    /// </summary>
    /// <param name="handler">Handler to attach.</param>
    public void AttachLobbyHandler(IRequestHandler<LobbyRequest, LobbyResult> handler)
    {
      this.LobbyHandler = handler;
    }

    /// <summary>
    /// Attach handler to the LobbyList endpoint.
    /// </summary>
    /// <param name="handler">Handler to attach.</param>
    public void AttachLobbyListHandler(IRequestHandler<LobbyListResult> handler)
    {
      this.LobbyListHandler = handler;
    }

    /// <summary>
    /// Attach handler to the PlaceTower endpoint.
    /// </summary>
    /// <param name="handler">Handler to attach.</param>
    public void AttachPlaceTowerHandler(IRequestHandler<PlaceTowerRequest, RequestResult> handler)
    {
      this.PlaceTowerHandler = handler;
    }

    /// <summary>
    /// Attach handler to the SellTower endpoint.
    /// </summary>
    /// <param name="handler">Handler to attach.</param>
    public void AttachSellTowerHHandler(IRequestHandler<SelltowerRequest, RequestResult> handler)
    {
      this.SellTowerHandler = handler;
    }

    /// <summary>
    /// Attach handler to the StartGame endpoint.
    /// </summary>
    /// <param name="handler">Handler to attach.</param>
    public void AttachStartGameHandler(IRequestHandler<StartGameRequest, StartGameResult> handler)
    {
      this.StartGameHandler = handler;
    }

    /// <summary>
    /// Attach handler to the StartRound endpoint.
    /// </summary>
    /// <param name="handler">Handler to attach.</param>
    public void AttachStartRoundHandler(IRequestHandler<LocalRequest, RequestResult> handler)
    {
      this.StartRoundHandler = handler;
    }

    /// <summary>
    /// Attach handler to the VerifyVersion endpoint.
    /// </summary>
    /// <param name="handler">Handler to attach.</param>
    public void AttachVerifyVersionHandler(IRequestHandler<VerifyVersionRequest, VerifyVersionResult> handler)
    {
      this.VerifyVersionHandler = handler;
    }
  }
}
