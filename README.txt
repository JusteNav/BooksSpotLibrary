Before running, migrations are needed:
-Migration 1 
update
-migration 2 
update

Passwords are set from the project's directory with the following: 

dotnet user-secrets set SeedLibrarianPW1 "123Abc?"
dotnet user-secrets set SeedLibrarianPW2 "456Def?"
dotnet user-secrets set SeedUserPW1 "789Ghi?"
dotnet user-secrets set SeedUserPW2 "123Jkl?"

User accounts: 

Librarian: //all permissions. Can also borrow, reserve and return books.Can cancel others' reservations and borrow books reserved by someone else. Can see who borrowed or reserved a book.
librarian1@test.com
123Abc?

librarian2@test.com
456Def?

User: //limited permissions - no editing, deleting, or creating books, no seeing who borrowed a book. Can borrow or reserve free books and cancel their own reservations.

user1@test.com
789Ghi?

user2@test.com
123Jkl?

Newly created users have User role by default.

Did not add book result pagination because of lack of time - that would have to be implemented in the future. 
