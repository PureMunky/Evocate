# stop existing container
docker stop tete-api
docker stop tete-web

# remove existing container
docker rm tete-api
docker rm tete-web

# clean the dotnet build files
dotnet clean

# build a new version of core
docker build -f Api.Dockerfile -t tete-api-img .
#docker build -f Web.Dockerfile -t tete-web-img .

# run core app
docker run -dit --name tete-api -p 80:80 tete-api-img
#docker run -dit --name tete-web -p 80:80 tete-web-img

dotnet run --project Tete.Web/Tete.Web.csproj

# ./run.sh