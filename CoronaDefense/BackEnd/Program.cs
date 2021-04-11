using System;

namespace BackEnd
{
  internal static class Program
  {
    private static void Main(string[] args)
    {
      Orchestrator orchestrator = new Orchestrator();
      Router.Router router = new Router.Router();

      API.Program.Main(Array.Empty<string>());
    }
  }
}
