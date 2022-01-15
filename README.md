# Supreme-CS
get started using `dotnet 6.0.0`
* Solution
* Data
* Service
## Solution
```
dotnet new sln -n Project
```
## Data
```
dotnet new classlib -n Data
dotnet sln add Data
```
## Service
```
dotnet new classlib -n Service
dotnet sln add Service
```
## API
```
dotnet new webapi -n API
dotnet sln add API
dotnet dev-certs https --project API --trust

dotnet ef migrations add Initial --project API
dotnet ef database update --project API

dotnet run --project API
dotnet watch --project API
```
## Web
```
dotnet new webapp -n Web
dotnet sln add Web
dotnet dev-certs https --project Web --trust

dotnet ef migrations add Initial --project Web
dotnet ef database update --project Web

dotnet run --project Web
dotnet watch --project Web
```