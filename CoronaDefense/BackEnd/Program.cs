// <copyright file="Program.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.Game;
using System;

namespace BackEnd
{
  /// <summary>
  /// Entry point of <see cref="BackEnd"/>.
  /// </summary>
  internal static class Program
  {
    private static void Main(string[] args)
    {
      ConnectionBroker connectionBroker = new ConnectionBroker();
      Router.Router router = new Router.Router();
      Orchestrator orchestrator = new Orchestrator(connectionBroker, router);

      API.Program.Main(Array.Empty<string>());
    }
  }
}
