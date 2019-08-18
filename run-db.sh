# stop existing container
docker stop tete-db

# remove existing container
docker rm tete-db

docker build -f Db.Dockerfile -t tete-db-img .
docker run -dit --name tete-db -p 1433:1433 tete-db-img

# ./run.sgh