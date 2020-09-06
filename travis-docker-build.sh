#!/bin/bash
docker build -f Db.Dockerfile -t tete-db-img .
docker build -f Web.Dockerfile -t tete-web-img .