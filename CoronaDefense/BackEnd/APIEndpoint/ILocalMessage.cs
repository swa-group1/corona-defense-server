// <copyright file="ILocalMessage.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.APIEndpoint
{
  /// <summary>
  /// A message intended for a non-global aspects of the game, such as <see cref="ModelInstance"/>. The name <see cref="ILocalMessage"/> is in contrast to <see cref="IGlobalMessage"/>, which concerns global aspects of the game, such as a request to create a new lobby.
  /// </summary>
  public interface ILocalMessage
  {
    /// <summary>
    /// Gets an address of the intended recipient of this <see cref="ILocalMessage"/>.
    /// </summary>
    long Address { get; }
  }
}
