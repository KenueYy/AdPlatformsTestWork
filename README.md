[![Build and Deploy Service](https://github.com/KenueYy/AdPlatformsTestWork/actions/workflows/test-build-and-deploy.yml/badge.svg)](https://github.com/KenueYy/AdPlatformsTestWork/actions/workflows/test-build-and-deploy.yml)


Есть несколько вариантов запуска: 
 1. Выполнить docker-compose.yml
 2. Открыть в IDE и в .csproj файле убрать строку <DefineConstants>CACHE_ENABLED</DefineConstants>


Проверить работу API также можно по адресу:

POST http://45.89.65.103:777/api/load для загрузки

GET http://45.89.65.103:777/api/search?location= для поиска
