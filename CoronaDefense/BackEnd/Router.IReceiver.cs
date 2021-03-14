﻿// <copyright file="Router.IReceiver.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.APIEndpoint;

namespace BackEnd
{
  internal partial class Router
  {
    /// <summary>
    /// Receiver to receive <see cref="ILocalMessage"/>s routed via a <see cref="Router"/>.
    /// </summary>
    internal interface IReceiver
    {
      /// <summary>
      /// Notify this <see cref="IReceiver"/> about a incoming <see cref="ILocalMessage"/> addressed to it.
      /// </summary>
      /// <param name="message">Message to react to.</param>
      public void OnMessage(ILocalMessage message);
    }
  }
}
