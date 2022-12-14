
## Books Spot Library

A simple library application for storing and searching books.

- ### Pre-requisites:
	- #### Some passwords are needed for the initial build: 
		Passwords are set from the project's directory using the Secret Manager tool with the following commands: 

			dotnet user-secrets  --project BooksSpotLibrary set SeedLibrarianPW1 "123Abc?"
			dotnet user-secrets  --project BooksSpotLibrary set SeedLibrarianPW2 "456Def?"
			dotnet user-secrets  --project BooksSpotLibrary set SeedUserPW1 "789Ghi?"
			dotnet user-secrets  --project BooksSpotLibrary set SeedUserPW2 "123Jkl?"
		The passwords chosen might be different, but provided passwords guarantee fulfillment of complexity requirements. 
				
	- #### Installation of either Microsoft SQL Server or Microsoft SQL Server Express LocalDB is required: 
		This project is running SQL Server Express LocalDB. For more information please see: https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16
	
	- #### To create local databases, database migrations are needed the first time this project is run:

			Add-Migration InitialCreateUsers -Context "ApplicationDbContext"
			Update-Database -Context "ApplicationDbContext"
			Add-Migration InitialCreateBooks -Context "BooksSpotLibraryContext"
			Update-Database -Context "BooksSpotLibraryContext"
   		They can be launched from the Visual Studio's Package Manager Console with the commands specified above.
- ### Roles
	The application is configured to have these Roles:
	
	- #### Librarian: 
		- All permissions. 
		- Can borrow, reserve, and return books. 
		- Can cancel others' reservations and borrow books reserved by someone else. 
		- Can see who borrowed or reserved a book. 
		- Can create, edit, or delete book entries. 

				librarian1@test.com
				123Abc?

				librarian2@test.com
				456Def?

	- #### User: 
		- Limited permissions.
		- Can borrow or reserve free books and cancel their own reservations. 
		- Can search for books they borrowed or reserved.
		- Users created through Register webpage have User role by default.

				user1@test.com
				789Ghi?

				user2@test.com
				123Jkl?

- ### Notes:

	- Division of results into pages was not implemented this time and would have to be added in the future. 
	- Many of the built-in authentification services were purposefully disabled to keep the application simple.
