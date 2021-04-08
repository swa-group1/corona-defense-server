// <copyright file="LobbyResult.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace API.Schemas
{
  /// <summary>
  /// Result of a request to get information about a lobby.
  /// </summary>
  public class LobbyResult : RequestResult
  {
    /// <summary>
    /// Lobby requested.
    /// </summary>
    public Lobby Lobby { get; set; }
  }
}
