FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Do a little two-step, copying only the csproj file
# and doing a restore is faster than copying everything.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FIAB.csproj", "."]
RUN dotnet restore "FIAB.csproj"

# Copy the rest of the source, build it, and publish.
COPY . .
WORKDIR "/src"
RUN dotnet build "FIAB.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FIAB.csproj" -c Release -o /app/publish

FROM base AS final

#Copy our built program over.
WORKDIR /app
ENV ConnectionString="Server=localhost;Port=5432;Database=fiab;User Id=myUser;Password=myPassword;"
ENV AdminUsername="admin"
ENV AdminPassword="change me"
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FIAB.dll"]