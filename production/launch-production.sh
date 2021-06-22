cat ./github-token.txt | docker login ghcr.io --username lidiapb --password-stdin
docker-compose pull
docker-compose up