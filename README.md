
##Books Spot Library

This is my (Justė Navickaitė) submission to Baltic Amadeus #ITChallenge!


### Pre-requisites:
######The project is running SQL Server Express LocalDB. To instantiate databases, database migrations are needed the first time this project is run:

  `Add-Migration InitialCreateUsers -Context "ApplicationDbContext"
   Update-Database -Context "ApplicationDbContext"
   Add-Migration InitialCreateBooks -Context "BooksSpotLibraryContext"
   Update-Database -Context "BooksSpotLibraryContext"`
   
	They can be launched from within the Visual Studio's Package Manager Console.

######Some passwords are needed for initial data seeding. 
Passwords are set from the project's directory using the Secret Manager tool with the following commands: 

 `dotnet user-secrets set SeedLibrarianPW1 "123Abc?"
  dotnet user-secrets set SeedLibrarianPW2 "456Def?"
  dotnet user-secrets set SeedUserPW1 "789Ghi?"
  dotnet user-secrets set SeedUserPW2 "123Jkl?"`

	The passwords chosen might be different, but provided passwords guarantee the fulfillment of complexity requirements.

### Roles 
	The application is configured to have these Roles:

######Librarian: 
	All permissions. 
	Can borrow, reserve and return books. 
	Can cancel others' reservations and borrow books reserved by someone else. 
	Can see who borrowed or reserved a book. 
	Can create, edit or delete book entries. 

		librarian1@test.com
		123Abc?

		librarian2@test.com
		456Def?

######User: 
	Limited permissions 
	Can borrow or reserve free books and cancel their own reservations. 
	Can search for books they borrowed or reserved.

		user1@test.com
		789Ghi?

		user2@test.com
		123Jkl?

	Newly created users have User role by default.

### Notes:

###### Division of results into pages was not implemented this time and would have to be added in the future. 
###### Many of built-in authentification services were purposefully disabled to keep the application simple.
