# WebApiCast
Desafio - Cast Group

Comandos utilizados para criar a imagem:

docker build . -t dotnetapi1

Comando utilizado para criar o conteiner e executá-lo:

docker run -p 8081:5000 -e ASPNETCORE_ENVIRONMENT=Development dotnetapi1 --name dotnetapi2


# Utilizando em Kubernetes

Garanta que você tenha acesso a um cluster Kubernetes configurado e funcional.
Containerize seu Aplicativo:

Certifique-se de que seu aplicativo esteja containerizado, geralmente usando Docker. Crie um Dockerfile para definir como seu aplicativo será empacotado em um contêiner.
Implante e Gerencie Imagens de Contêiner:

Construa suas imagens de contêiner e envie-as para um registro de contêiner acessível pelo seu cluster, como o Docker Hub, o Google Container Registry ou um registro privado.
Crie Manifestos do Kubernetes:

Crie arquivos YAML que descrevam como seu aplicativo deve ser implantado no Kubernetes. Isso pode incluir deployments, services, configmaps, secrets, persistent volume claims, etc.
Implantação no Kubernetes:

Use o utilitário kubectl ou ferramentas de automação como Helm para implantar os recursos do Kubernetes no cluster. Por exemplo:
kubectl apply -f seu-arquivo-de-implantação.yaml
