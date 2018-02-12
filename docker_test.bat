docker network create devkitapinet
docker run --net=devkitapinet --name devkitapidb -e MYSQL_ROOT_PASSWORD=mypass -d mariadb
docker build -t matsskoglund/devkitapi:latest .
docker run -e ASPNETCORE_ENVIRONMENT=Staging --net=devkitapinet --name devkitapi -d -p 5000:5000 matsskoglund/devkitapi:latest
newman run https://raw.githubusercontent.com/matsskoglund/DevkitApi/master/test/DevkitApi.Test.postman_collection.json
docker stop devkitapidb
docker stop devkitapi
docker network rm devkitapinet
