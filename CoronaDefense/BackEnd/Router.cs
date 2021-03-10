// <copyright file="Router.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd
{
  using System;
  using System.Collections.Generic;

  /// <summary>
  /// Class with objects that route messages from <see cref="ApiEndpoint"/> to <see cref="GameInstance"/>s.
  /// </summary>
  internal class Router : ApiEndpoint.IObserver
  {
        /// <summary>
        /// Initializes a new instance of the <see cref="Router"/> class.
        /// </summary>
        /// <param name="apiEndpoint"> The Api Endpoint to connect to. </param>
        public Router(ApiEndpoint apiEndpoint)
    {
      apiEndpoint.AttachObserver(this);
    }

        private static Dictionary<string, Delegate> routeMap = new Dictionary<string, Delegate>()
    {
      { "/placeTower/", new Action(PlaceTower) },
    };

        private static void PlaceTower()
    {
      throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
        void ApiEndpoint.IObserver.GetNotified()
    {
      throw new System.NotImplementedException();
    }
  }
}
