# WebApiCast
Desafio - Cast Group

Comandos utilizados para criar a imagem:

docker build . -t dotnetapi1

Comando utilizado para criar o conteiner e executá-lo:

docker run -p 8081:5000 -e ASPNETCORE_ENVIRONMENT=Development dotnetapi1 --name dotnetapi2
