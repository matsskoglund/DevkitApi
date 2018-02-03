# Welcome to DevkitApi

[![Build status](https://ci.appveyor.com/api/projects/status/1al4h956xqs27ta6/branch/master?svg=true)](https://ci.appveyor.com/project/matsskoglund58956/devkitapi/branch/master)

[![CodeBuild status](https://codebuild.eu-west-1.amazonaws.com/badges?uuid=eyJlbmNyeXB0ZWREYXRhIjoib1RGL0E0Y0Nib2taRElyL1ZUTmNWaGVKRENFWkxSQ0x6Y1J3WG0wQys4REpUcmw4OHZ6SWRxbzlOWGxmMmgzVDdWSFo5T1ZzZHlDOXVCOHI2aVBKTWpBPSIsIml2UGFyYW1ldGVyU3BlYyI6Imcrb0FaV1ZiWThSeWdNZ0EiLCJtYXRlcmlhbFNldFNlcmlhbCI6MX0%3D&branch=master)

[![Docker status](https://dockerbuildbadges.quelltext.eu/status.svg?organization=matsskoglund&repository=devkitapiauto)](https://dockerbuildbadges.quelltext.eu/status.svg?organization=matsskoglund&repository=devkitapi)

## Starting a mariadb docker for testing
´´´docker run -p 3306:3306 --name devkit -e MYSQL_ROOT_PASSWORD=mypass -d mariadb´´´

## Building docker image
docker build -t matsskoglund/devkitapi:latest .

## Running in Staging mode (will create and use internal sqlite db). I use Staging mode for testing for the moment.
´´´docker run -e ASPNETCORE_ENVIRONMENT=Staging -it -p 5000:5000 matsskoglund/devkitapi:latest´´´

