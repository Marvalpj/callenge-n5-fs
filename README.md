# 1- ejecutar los servicios

docker-compose up -d 
ver que los servicio sqlserver y kafka esten corriendo

# 2- crear archivo .env

en la carpta frontend-permission crear archivo .env para declarar la variable de entorno del api
VITE_API_URL=http://example.com/api/

# 3- instalar dependencias en el front 

ejecutar el comando "npm i" en el proyecto frontend-permission para instalar las depencias

# 4- conectar al servicio sqlserver y crear una tabla
crear base de datos para que ef cree y mapee las entidades en ella

# 5- configurar kafka
crear topico permission-topic
acceder a la terminal del contenedor donde se corre el servicio de kafka
ejecutar el siguiente comando
formato del comando:  "kafka-topics --bootstrap-server ipServicio:port --create --topic nombre-topico"
comando kafka-topics --bootstrap-server kafka:9092 --create --topic permission-topic





