#!/bin/bash
# TODO: Push to prod k8s cluster.
echo "$DOCKER_TOKEN" | docker login -u "$DOCKER_USERNAME" --password-stdin

docker tag tete-db-img:latest puremunky/tete-db:prod-$TRAVIS_BUILD_NUMBER
docker tag tete-web-img:latest puremunky/tete-web:prod-$TRAVIS_BUILD_NUMBER

docker push puremunky/tete-db:prod-$TRAVIS_BUILD_NUMBER
docker push puremunky/tete-web:prod-$TRAVIS_BUILD_NUMBER
