﻿// <copyright file="ApiEndpoint.Observer.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd
{
  internal partial class ApiEndpoint
  {
    /// <summary>
    /// Observer of the <see cref="ApiEndpoint"/>.
    /// </summary>
    internal interface IObserver
    {
      /// <summary>
      /// What to do when notified by the <see cref="ApiEndpoint"/>.
      /// </summary>
      internal void GetNotified();
    }
  }
}