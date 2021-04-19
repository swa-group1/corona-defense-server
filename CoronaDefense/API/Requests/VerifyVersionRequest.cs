// <copyright file="VerifyVersionRequest.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.Communication.API.Requests
{
  /// <summary>
  /// Request to verify version.
  /// </summary>
  public class VerifyVersionRequest
  {
    /// <summary>
    /// Gets clients version.
    /// </summary>
    public string Version { get; init; }
  }
}
