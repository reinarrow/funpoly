# Funpoly

Funpoly is a web application for a Monopoly-like game. It consists in a VS solution with 2 containerised projects:

- funpoly: The web application
- funpoly_postgres: Persistence layer as a Postgres DB

## Execution for development

1. Open the solution in Visual Studio
1. Select the docker-compose project as the Startup Project if it is not already.
1. Run in Debug mode with the option Docker Compose


## Data model

The data model can be checked in the image Inkeddatamodel_LI.jpg at the root of the project. The solution uses Entity Framework Core for the interaction with the database and as data model and querying engine (together with Linq).

Some basic instructions for data model modifications:

- The project uses the repository pattern. There is a repository for each data model for CRUD operations.
- Repositories are injected using interfaces, that's why there is an interface for every repository (e.g. PlayerRepository.cs and IPlayerRepository.cs). Please respect naming.
- Everytime a change is done in the data model, we have to create a migration for the database. To do so, open a cmd under the src/ folder of the protect and run:

  `dotnet ef migrations add vX`, with X being the next version (check Migrations folder to know it)

- When an important change is done in the data model, all developers shall drop their database by executing `dotnet ef database drop -f` (again from within src folder)

- The database seeding is done in DbInitializer.cs. There is a space at the end for development seeding. Use it for tests.

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[GNU GPLv3](https://choosealicense.com/licenses/gpl-3.0/)