// <copyright file="StartRoundRequest.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace API.Requests
{
  /// <summary>
  /// Request to verify version.
  /// </summary>
  public class VerifyVersionRequest
  {
    /// <summary>
    /// Clients version.
    /// </summary>
    public string Version { get; init; }
  }
}

