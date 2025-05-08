// Dot Net commands list

code .

dotnet new list
dotnet new sln
dotnet new webapi -controllers -n API
dotnet sln list
dotnet sln add API 
dotnet run
dotnet watch

dotnet dev-certs https --clean
dotnet dev-certs https
dotnet dev-certs https --trust

dotnet tool list -g
dotnet tool install --global dotnet-ef --version 9.0.4

dotnet ef migrations add InitialCreate -o Data/Migrations
dotnet ef database update      
-------------------------------------------------------------------------
Packages

Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Sqlite

-------------------------------------------------------------------------

// Environmental Variables
PATH="$PATH:/usr/local/share/dotnet/sdk"
PATH="$PATH:/Applications/Visual Studio Code.app/Contents/Resources/app/bin"