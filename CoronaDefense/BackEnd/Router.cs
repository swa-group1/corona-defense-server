// <copyright file="Router.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

namespace BackEnd
{
  /// <summary>
  /// Class with objects that route messages from <see cref="ApiEndpoint"/> to <see cref="ModelInstance"/>.
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

        /// <inheritdoc/>
        void ApiEndpoint.IObserver.GetNotified()
    {
      throw new System.NotImplementedException();
    }
  }
}
