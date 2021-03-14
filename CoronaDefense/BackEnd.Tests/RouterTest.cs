// <copyright file="RouterTest.cs" company="MostlyIT">
// Copyright (c) MostlyIT. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace BackEnd.Tests
{
    using BackEnd;
    using Xunit;

    /// <summary>
    /// Unit-testing of <see cref="Router"/>
    /// </summary>
    public class RouterTest
    {
        /// <summary>
        /// Testing that registering <see cref="Router.IReceiver"/>s to <see cref="Router"/>s work correctly.
        /// </summary>
        [Fact]
        public void RegisterTest()
        {
            Router router = new Router();
        }

        /// <summary>
        /// <see cref="Router.IReceiver"/> for testing purposes.
        /// </summary>
        private class TestReceiver : Router.IReceiver
        {
            /// <summary>
            /// Gets a queue of received messages.
            /// </summary>
            public Queue<string> Messages { get; } = new Queue<string>();
            
            /// <inheritdoc/>
            public void OnMessage(APIEndpoint.ILocalMessage message)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}