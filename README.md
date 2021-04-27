# Welcome to Corona Defense back-end
.NET


## How to run a instance
The server runs continously on the Google Cloud Platform, and in most instances does not need to be started locally to run the application as a whole. If one, however, do wish to run the server, one must clone the repository, open the the solution file at path CoronaDefense/CoronaDefense.sln with a recent version of Visual Studio 2019, and start the BackEnd project through the user interface.

To connect to this local server, one can manually change the IP address constants in the front-end and rebuild that.

## Structure

### API
Receives requests from the clients.

### Orchestrator
Processes requests not intended for one specific lobby, such as highscore list functionality.

### Router
Transmits requests to specific lobbies.

### Lobby
Contains all game logic

### Broadcaster and ConnectionBroker
Accepts socket connections and broadcasts game changes to all attached clients.
