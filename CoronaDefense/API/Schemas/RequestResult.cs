// <copyright file="RequestResult.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace API.Schemas
{
  /// <summary>
  /// A generic result message for a request.
  /// </summary>
  public class RequestResult
  {
    /// <summary>
    /// Gets or sets text describing normal.
    /// </summary>
    [Required]
    public string Details { get; set; } = "Success.";

    /// <summary>
    /// Gets or sets a value indicating whether the operation was successful.
    /// </summary>
    [Required]
    public bool Success { get; set; } = true;
  }
}
