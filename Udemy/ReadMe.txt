// Dot Net commands list (API)

code .

dotnet new list
dotnet new sln
dotnet new webapi -controllers -n API
dotnet sln list
dotnet sln add API 
dotnet run
dotnet watch
dotnet build

dotnet dev-certs https --clean
dotnet dev-certs https
dotnet dev-certs https --trust

dotnet tool list -g
dotnet tool install --global dotnet-ef --version 9.0.4

dotnet ef migrations add InitialCreate -o Data/Migrations
dotnet ef migrations remove // Before update statement  
dotnet ef database update
dotnet ef database drop

dotnet ef migrations add IdentityAdded
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

// Component
ng g c nav --dry-run
ng g c nav --skip-tests
ng g c modals/roles-modal --skip-tests


// Services
ng g s _services/account --dry-run
ng g s _services/account --skip-tests

// Route Gaurd 
ng g g _guards/auth --dry-run
ng g g _guards/auth --skip-tests 

// Interceptor
ng g interceptor _interceptors/error --dry-run
ng g interceptor _interceptors/error --skip-tests

// Resolver
ng g r _resolver/member-detailed --dry-run
ng g r _resolver/member-detailed --skip-tests

// Configuration 
ng g environments

// Directive
ng g d _directives/has-role --dry-run
ng g d _directives/has-role --skip-tests

-------------------------------------------------------------------------
npm install ngx-bootstrap@18 bootstrap font-awesome
npm install ngx-toastr
npm i ng-gallery

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
AutoMapper
CloudinaryDotNet
Microsoft.AspNetCore.Identity.EntityFrameworkCore

SignalR
npm install @microsoft/signalr
-------------------------------------------------------------------------

// Environmental Variables
PATH="$PATH:/usr/local/share/dotnet/sdk"
PATH="$PATH:/Applications/Visual Studio Code.app/Contents/Resources/app/bin"

-------------------------------------------------------------------------
Thememing

https://bootswatch.com/superhero/