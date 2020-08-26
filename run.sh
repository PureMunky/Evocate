export teteDBUser="sa"
export teteDBPassword=$(</dev/urandom tr -dc '12345!@#$%qwertQWERTasdfgASDFGzxcvbZXCVB' | head -c16; echo "aA!")
export teteDBServer="tete-db"

# stop existing container
docker stop tete-web
docker stop tete-db

# remove existing container
docker rm tete-web
docker rm tete-db

# clean the dotnet build files
dotnet clean

# build a new version of core
docker build -f Web.Dockerfile -t tete-web-img .
docker build -f Db.Dockerfile -t tete-db-img .

# run core app
docker run -dit --name tete-db -p 1433:1433 --env SA_PASSWORD=$teteDBPassword tete-db-img
docker run -dit --name tete-web -p 80:80 --link tete-db --env ConnectionStrings__DefaultConnection="Server=$teteDBServer; Database=Tete; User ID=$teteDBUser; Password=$teteDBPassword" tete-web-img

#dotnet run --project Tete.Web/Tete.Web.csproj

# ./run.sh