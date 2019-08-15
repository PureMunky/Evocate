# stop existing container
docker stop tete-api
docker stop tete-web

# remove existing container
docker rm tete-api
docker rm tete-web

# clean the dotnet build files
dotnet clean

# build a new version of core
docker build -f Db.Dockerfile -t tete-db-img .
docker build -f Api.Dockerfile -t tete-api-img .
docker build -f Web.Dockerfile -t tete-web-img .

docker tag tete-db-img:latest puremunky/tete-db:latest
docker tag tete-api-img:latest puremunky/tete-api:latest
docker tag tete-web-img:latest puremunky/tete-web:latest

docker push puremunky/tete-db:latest
docker push puremunky/tete-api:latest
docker push puremunky/tete-web:latest

# kubectl apply -f tete-deployment.yml

# ./deploy.sh