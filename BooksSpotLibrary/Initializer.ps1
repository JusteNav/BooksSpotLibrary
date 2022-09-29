dotnet user-secrets set SeedLibrarianPW1 "123Abc?"

dotnet user-secrets set SeedLibrarianPW2 "456Def?"

dotnet user-secrets set SeedUserPW1 "789Ghi?"

dotnet user-secrets set SeedUserPW2 "123Jkl?"

dotnet ef migrations add InitialCreateUsers --startup-project BooksSpotLibrary --Context ApplicationDbContext

dotnet ef database update --Context ApplicationDbContext

dotnet ef migrations add InitialCreateBooks --Context BooksSpotLibraryContext

dotnet ef database update --startup-project BooksSpotLibrary --Context BooksSpotLibraryContext
