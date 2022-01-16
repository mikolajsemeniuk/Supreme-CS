# Supreme-CS
get started using `dotnet 6.0.0`
* Solution
* Data
* Service
* Web
* Tests
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
## Tests
```
dotnet new xunit -n Tests
dotnet sln add Tests
```