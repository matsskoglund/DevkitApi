docker network create devkitapinet
docker run --net=devkitapinet --name devkitapidb -e MYSQL_ROOT_PASSWORD=mypass -d mariadb
docker build -t matsskoglund/devkitapi:latest .
set DBCONNECTION=server=devkitapidb;userid=root;password=mypass;database=devkit;
docker run -e ASPNETCORE_ENVIRONMENT=Staging -e DBCONNECTION=%DBCONNECTION% --net=devkitapinet --name devkitapi -d -p 5000:5000 matsskoglund/devkitapi:latest
timeout 10
newman run https://raw.githubusercontent.com/matsskoglund/DevkitApi/master/test/DevkitApi.Test_Nondestructive.postman_collection.json
docker stop devkitapidb
docker stop devkitapi
docker network rm devkitapinet
