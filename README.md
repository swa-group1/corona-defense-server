# Corona Defense Game Server

Server for the Corona Defense game, written in C# for the ASP.NET runtime. Client implemented in [`swa-group1/corona-defense-client`](https://github.com/swa-group1/corona-defense-client).

## Project Structure

- `API` receives requests from the clients.
- `BackEnd`
  - `Orchestrator` processes requests not intended for one specific lobby, such as highscore list functionality.
  - `Router` transmits requests to specific lobbies.
  - `Game` contains all game logic.
  - `Communication` accepts socket connections and broadcasts game changes to all attached clients.

## How To Run

The server runs continously on the Google Cloud Platform, and in most instances does not need to be started locally to run the application as a whole. If one still wishes to run the server locally, one must clone the repository, open the the solution file at path `CoronaDefense/CoronaDefense.sln` with a recent version of Visual Studio 2019, and start the `BackEnd` project through the user interface.

To connect to this local server, one can manually change the IP address constants in the client and rebuild it.
