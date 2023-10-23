# tibber-bot

A friendly and helpful office cleaner bot. Just tell it where to start and plot a course for its cleaning route and it'll have your office space cleaned in no time! 

### Getting started

The service can be started via `docker compose up`, which should build the service and all its dependencies before starting a container. 

You will need to define an environment variable `db_connection` containing a connection string to the database where the robot's [ExecutionRecord](./TibberBot/Dto/ExecutionRecord.cs) will be stored after each successful run. 

See the `ConnectionStrings` section of the [appsettings](./TibberBot/appsettings.json) file for how to format your connection string.

> Tip: You can add the `db_connection` environment variable via a `.env` file.

The database should contain a table called `executions` in order to store results. See [executionsTable.sql](./TibberBot/sql/executionsTable.sql) for details on how the table should be specified.

The robot listens for new instructions on port `5000` via the `/tibber-developer-test/enter-path` endpoint. Here's an example request:

```(cURL)
curl --location 'http://localhost:5000/tibber-developer-test/enter-path' \
--header 'Content-Type: application/json' \
--data '{
    "start": {
        "x": 0,
        "y": 0
    },
    "commands": [
        {
            "direction": "north",
            "steps": 25
        },
        {
            "direction": "east",
            "steps": 25
        },
        {
            "direction": "south",
            "steps": 10
        },
        {
            "direction": "west",
            "steps": 10
        },
    ]
}'
```
