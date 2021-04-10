// <copyright file="Lobby.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace API.Schemas
{
  /// <summary>
  /// A personal indicator and the value of their game score.
  /// </summary>
  public class Lobby
  {
    /// <summary>
    /// Gets or sets unique ID of this <see cref="Lobby"/>.
    /// </summary>
    [Required]
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets name of this <see cref="Lobby"/>.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets number of players currently in this <see cref="Lobby"/>.
    /// </summary>
    [Required]
    public int PlayerCount { get; set; }
  }
}
