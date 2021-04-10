// <copyright file="ActivateClientRequest.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace API.Requests
{
  /// <summary>
  /// Request to activate a client.
  /// </summary>
  public class ActivateClientRequest
  {
    /// <summary>
    /// Gets access token of client to activate.
    /// </summary>
    public string AccessToken { get; init; }
  }
}
