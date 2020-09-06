#!/bin/bash
echo "$DOCKER_TOKEN" | docker login -u "$DOCKER_USERNAME" --password-stdin

docker build -f Db.Dockerfile -t tete-db-img .
docker build -f Web.Dockerfile -t tete-web-img .

docker tag tete-db-img:latest puremunky/tete-db:$TRAVIS_BUILD_NUMBER
docker tag tete-web-img:latest puremunky/tete-web:$TRAVIS_BUILD_NUMBER

docker push puremunky/tete-db:$TRAVIS_BUILD_NUMBER
docker push puremunky/tete-web:$TRAVIS_BUILD_NUMBER
