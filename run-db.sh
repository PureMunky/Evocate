# https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-ver15&pivots=cs1-bash
# TODO: Update password process.
# TODO: Create an app account to use instead of SA.

export teteDBUser="sa"
export teteDBPass="tetePassword!"

# stop existing container
docker stop tete-db

# remove existing container
docker rm tete-db

# build docker image
docker build -f Db.Dockerfile -t tete-db-img . --build-arg saPassword=$teteDBPass

# run docker image
docker run -dit --name tete-db -p 1433:1433 tete-db-img

# create database migration
# cd Tete.Web && dotnet-ef migrations add build

# ./run-db.sh