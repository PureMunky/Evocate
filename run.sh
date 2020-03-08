# stop existing container
docker stop tete-api
docker stop tete-web
docker stop tete-db

# remove existing container
docker rm tete-api
docker rm tete-web
docker rm tete-db

# clean the dotnet build files
dotnet clean

#docker run --name tete-db -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=tetePassword!' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-CU8-ubuntu
docker run -dit --name tete-db -p 1433:1433 tete-db-img
sleep 5

#dotnet ef database update -p Tete.Api

# build a new version of core
docker build -f Api.Dockerfile -t tete-api-img .
docker build -f Web.Dockerfile -t tete-web-img .
docker build -f Db.Dockerfile -t tete-db-img .

# run core app
docker run -dit --name tete-api -p 80:80 tete-api-img 
docker run -dit --name tete-web -p 80:80 tete-web-img
docker run -dit --name tete-db -p 1433:1433 tete-db-img

#dotnet run --project Tete.Web/Tete.Web.csproj

# ./run.sh