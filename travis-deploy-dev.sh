#!/bin/bash
# TODO: Push to dev k8s cluster.
# https://gist.github.com/theraaz/933ab38f1dcbd985f448439728bfef6e
echo "$DOCKER_TOKEN" | docker login -u "$DOCKER_USERNAME" --password-stdin
echo "$KUBERNETES_CLUSTER_CERTIFICATE" | base64 --decode > cert.crt
docker tag tete-web-img:latest puremunky/tete-web:build-$TRAVIS_BUILD_NUMBER

docker push puremunky/tete-web:build-$TRAVIS_BUILD_NUMBER

envsubst <./deployments/do-dev.yml >./deployments/do-dev.yml.out
mv ./deployments/do-dev.yml.out ./deployments/do-dev.yml
kubectl --kubeconfig=/dev/null --token=$KUBERNETES_TOKEN --server=$KUBERNETES_SERVER --certificate-authority=cert.crt apply -f ./deployments/do-dev.yml
