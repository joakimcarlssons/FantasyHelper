# Fantasy Football Helper

A system based around fantasy football with the intent to help you make up the perfect plan for you fantasy teams.
The system include planners and information from the following fantasy games:

<ul>
  <li><a href="https://fantasy.premierleague.com/">Fantasy Premier League</a></li>
  <li><a href="https://fantasy.allsvenskan.se/">Fantasy Allsvenskan</a></li>
</ul>

For all games above there are services handling the following type of data:
- Fixture difficulty
- Fixture and gameweek plannings

## Backend
The backend is built on a .NET microservices achitecture with each service communicating asynchronous via RabbitMQ.

Services worth mentioning:
- **Data Provider Services**
  - Fetches the main data from each fantasy games global API and maps it to the systems internal structure.
    These services are running on a set interval to continously update the base data. Every time the data is being updated new messages will be published to the message     bus.
    
- **Fixture Provider Services**
  - These services fetches data from external sources and uses that together with the base data to calculate the difficulty of the fixtures for each team and gameweek.

- **Gameweek Planner Services**
  - Provides the players available in each gameweek and the fixture (including fixture difficulty) for the players.


## Frontend
Current frontend is built in Blazor WebAssembly, but a new one is in progress using React.js/Next.js.
