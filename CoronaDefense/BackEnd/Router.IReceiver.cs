// <copyright file="Router.IReceiver.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd
{
  internal partial class Router
  {
    /// <summary>
    /// Receiver to receive messages routed via a <see cref="Router"/>.
    /// </summary>
    internal interface IReceiver
    {
      /// <summary>
      /// Notify this <see cref="IReceiver"/>.
      /// </summary>
      internal void Notify()
      {
      }
    }
  }
}
