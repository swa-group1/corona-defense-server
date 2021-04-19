// <copyright file="LobbyListResult.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Communication.API.Schemas
{
  /// <summary>
  /// List of the available lobbies.
  /// </summary>
  public class LobbyListResult : RequestResult
  {
    /// <summary>
    /// Gets or sets list of available lobbies.
    /// </summary>
    [Required]
    public List<Lobby> Lobbies { get; set; } = new List<Lobby>();
  }
}
