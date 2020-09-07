#!/bin/bash
# TODO: Push to dev k8s cluster.
echo "$DOCKER_TOKEN" | docker login -u "$DOCKER_USERNAME" --password-stdin

docker tag tete-web-img:latest puremunky/tete-web:build-$TRAVIS_BUILD_NUMBER

docker push puremunky/tete-web:build-$TRAVIS_BUILD_NUMBER
