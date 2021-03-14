// <copyright file="Broadcaster.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.ModelEvents;
using System;

namespace BackEnd
{
  /// <summary>
  /// Broadcaster for a <see cref="ModelInstance"/>. Functionality to send game events to all connected clients.
  /// </summary>
  internal class Broadcaster
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Broadcaster"/> class.
    ///
    /// <para>TODO: Add parameters for information about clients.</para>
    /// </summary>
    internal Broadcaster()
    {
    }

    /// <summary>
    /// Broadcast event to all clients.
    /// </summary>
    /// <param name="modelEvent">Event to broadcast to clients.</param>
    internal void Broadcast(IModelEvent modelEvent)
    {
      throw new NotImplementedException();
    }
  }
}
