using System;

namespace BackEnd
{
  internal static class Program
  {
    private static void Main(string[] args)
    {
      Router.Router router = new Router.Router();
      Orchestrator orchestrator = new Orchestrator(router);

      API.Program.Main(Array.Empty<string>());
    }
  }
}
