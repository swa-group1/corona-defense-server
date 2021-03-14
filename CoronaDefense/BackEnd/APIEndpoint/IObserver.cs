// <copyright file="IObserver.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd.APIEndpoint
{
  /// <summary>
  /// Observer of an <see cref="IAPIEndpoint"/>.
  /// </summary>
  public interface IObserver
  {
    /// <summary>
    /// React to a <see cref="IGlobalMessage"/> from a <see cref="APIEndpoint"/>.
    /// </summary>
    /// <param name="message"><see cref="IGlobalMessage"/> to react to.</param>
    public void OnGlobalMessage(IGlobalMessage message);

    /// <summary>
    /// React to a <see cref="ILocalMessage"/> from an <see cref="APIEndpoint"/>.
    /// </summary>
    /// <param name="message"><see cref="ILocalMessage"/> to react to.</param>
    public void OnLocalMessage(ILocalMessage message);
  }
}
