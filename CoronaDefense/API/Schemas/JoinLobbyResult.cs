// <copyright file="JoinLobbyResult.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace BackEnd.Communication.API.Schemas
{
  /// <summary>
  /// Token used to authenticate requests made for a specific lobby.
  /// </summary>
  public class JoinLobbyResult : RequestResult
  {
    /// <summary>
    /// Gets or sets token used to authenticate requests made for a specific lobby.
    /// </summary>
    public long AccessToken { get; set; }

    /// <summary>
    /// Gets or sets ID of the lobby that was attempted joined.
    /// </summary>
    [Required]
    public long LobbyId { get; set; }
  }
}
