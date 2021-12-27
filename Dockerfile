# First Phase, build the application from an image with SDK
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS builder
WORKDIR /src/

COPY AsyncExample.sln AsyncExample.sln
COPY AsyncExample/AsyncExample.csproj AsyncExample/AsyncExample.csproj
RUN dotnet restore AsyncExample/.

COPY AsyncExample/* AsyncExample/
RUN dotnet build --configuration Release AsyncExample/.


# Second Phase, copy the application to an image with Runtime-only 
FROM mcr.microsoft.com/dotnet/runtime:6.0 AS production
WORKDIR /exe/

COPY --from=builder /src/AsyncExample/bin/Release/net6.0/ /exe/
CMD ["dotnet", "/exe/AsyncExample.dll"]