// <copyright file="VerifyVersionResult.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace API.Schemas
{
  /// <summary>
  /// Results from a request to verify a client version number.
  /// </summary>
  public class VerifyVersionResult : RequestResult
  {
    /// <summary>
    /// Gets or sets a value indicating whether supplied version can be used with this server.
    /// </summary>
    [Required]
    public bool ValidVersion { get; set; }
  }
}
