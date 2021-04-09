// <copyright file="CreateLobbyResult.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace API.Schemas
{
  /// <summary>
  /// Results from a request to create a new lobby.
  /// </summary>
  public class CreateLobbyResult : RequestResult
  {
    /// <summary>
    /// Gets or sets ID of the created lobby.
    /// </summary>
    public long LobbyId { get; set; }
  }
}
