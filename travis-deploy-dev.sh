#!/bin/bash
echo "$DOCKER_TOKEN" | docker login -u "$DOCKER_USERNAME" --password-stdin

docker tag tete-db-img:latest puremunky/tete-db:build-$TRAVIS_BUILD_NUMBER
docker tag tete-web-img:latest puremunky/tete-web:build-$TRAVIS_BUILD_NUMBER

docker push puremunky/tete-db:build-$TRAVIS_BUILD_NUMBER
docker push puremunky/tete-web:build-$TRAVIS_BUILD_NUMBER
