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
docker build -f Web.Dockerfile -t tete-web-img .

kubectl apply -f tete-deployment.yml

# ./deploy.sh