# Welcome to DevkitApi

[![Build status](https://ci.appveyor.com/api/projects/status/1al4h956xqs27ta6/branch/master?svg=true)](https://ci.appveyor.com/project/matsskoglund58956/devkitapi/branch/master)

![CodeBuild status](https://codebuild.eu-west-1.amazonaws.com/badges?uuid=eyJlbmNyeXB0ZWREYXRhIjoib1RGL0E0Y0Nib2taRElyL1ZUTmNWaGVKRENFWkxSQ0x6Y1J3WG0wQys4REpUcmw4OHZ6SWRxbzlOWGxmMmgzVDdWSFo5T1ZzZHlDOXVCOHI2aVBKTWpBPSIsIml2UGFyYW1ldGVyU3BlYyI6Imcrb0FaV1ZiWThSeWdNZ0EiLCJtYXRlcmlhbFNldFNlcmlhbCI6MX0%3D&branch=master)

[![Docker status](https://dockerbuildbadges.quelltext.eu/status.svg?organization=matsskoglund&repository=devkitapiauto)](https://dockerbuildbadges.quelltext.eu/status.svg?organization=matsskoglund&repository=devkitapi)

## Starting a mariadb docker for testing
`docker run -p 3306:3306 --name devkit -e MYSQL_ROOT_PASSWORD=mypass -d mariadb`

## Building docker image
`docker build -t matsskoglund/devkitapi:latest .`

## Running in Staging mode
Will create and use internal sqlite db. I use Development mode for testing for the moment.

`docker run -e ASPNETCORE_ENVIRONMENT=Staging --net=host -it -p 5000:5000 matsskoglund/devkitapi:latest`

## Run the Postman tests
`cd test`
`newman run DevkitApi.Test.postman_collection.json`

# Using Docker networking
`docker network create devkitapinet`
`docker network ls`
`docker run --net=devkitapinet --name devkitapidb -e MYSQL_ROOT_PASSWORD=mypass -d mariadb`
`docker build -t matsskoglund/devkitapi:latest .`
`docker run -e ASPNETCORE_ENVIRONMENT=Staging --net=devkitapinet --name devkitapi -d -p 5000:5000 matsskoglund/devkitapi:latest`
`newman run https://raw.githubusercontent.com/matsskoglund/DevkitApi/master/test/DevkitApi.Test.postman_collection.json`
`docker stop devkitapidb`
`docker network rm devkitapinet`
