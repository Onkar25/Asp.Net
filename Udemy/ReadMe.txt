// Dot Net commands list (API)

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
Angular (client)

# Download and install nvm:
curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.40.3/install.sh | bash

# in lieu of restarting the shell
\. "$HOME/.nvm/nvm.sh"

# Download and install Node.js:
nvm install 22

# Verify the Node.js version:
node -v # Should print "v22.15.0".
nvm current # Should print "v22.15.0".

# Verify npm version:
npm -v # Should print "10.9.2".


npm install -g @angular/cli@18
ng version

ng new client
ng serve


npm install ngx-bootstrap@18 bootstrap font-awesome


brew install mkcert
brew install nss # if you use Firefox
mkcert -install  // client
mkdir ssl
cd ssl
mkcert localhost
-------------------------------------------------------------------------
Packages

Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Sqlite
System.IdentityModel.Tokens.Jwt
Microsoft.AspNetCore.Authentication.JwtBearer
-------------------------------------------------------------------------

// Environmental Variables
PATH="$PATH:/usr/local/share/dotnet/sdk"
PATH="$PATH:/Applications/Visual Studio Code.app/Contents/Resources/app/bin"