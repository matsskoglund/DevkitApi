#!/bin/bash
# Stop all containers
docker stop $(docker ps -a -q)
# Remove all containers
docker rm $(docker ps -a -q)

# Remove all images
docker rmi $(docker images -q)