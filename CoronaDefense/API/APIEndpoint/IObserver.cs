// <copyright file="IObserver.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using API.GlobalMessages;

namespace API.APIEndpoint
{
  /// <summary>
  /// Observer of an <see cref="IAPIEndpoint"/>.
  /// </summary>
  public interface IObserver
  {
    /// <summary>
    /// React to a request to create a new lobby.
    /// </summary>
    /// <param name="message"><see cref="CreateLobbyMessage"/> with information about the request.</param>
    /// <returns><see langword="true"/> if </returns>
    bool OnCreateLobbyMessage(CreateLobbyMessage message)
    {
      return true;
    }

    /// <summary>
    /// React to a <see cref="IGlobalMessage"/> from a <see cref="APIEndpoint"/>.
    /// </summary>
    /// <param name="message"><see cref="IGlobalMessage"/> to react to.</param>
    void OnGlobalMessage(IGlobalMessage message)
    {
    }

    /// <summary>
    /// React to a <see cref="ILocalMessage"/> from an <see cref="APIEndpoint"/>.
    /// </summary>
    /// <param name="message"><see cref="ILocalMessage"/> to react to.</param>
    void OnLocalMessage(ILocalMessage message)
    {
    }
  }
}
