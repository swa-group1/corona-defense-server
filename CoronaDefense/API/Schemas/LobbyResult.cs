﻿// <copyright file="LobbyResult.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.Communication.API.Schemas
{
  /// <summary>
  /// Result of a request to get information about a lobby.
  /// </summary>
  public class LobbyResult : RequestResult
  {
    /// <summary>
    /// Gets or sets lobby requested.
    /// </summary>
    public Lobby Lobby { get; set; }
  }
}
