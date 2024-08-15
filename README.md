# config project
# 1- ejecutar los servicios
docker-compose up -d 
ver que los servicio sqlserver y kafka esten corriendo
# 2- conectar a sqlserver y crear una tabla
crear base de datos para que ef cree y mapee las entidades en ella

# 3- configurar el appseting.json del api
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "database": "Server=localhost;Database=permissions;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;"
  },
  "kafka": {
    "bootstrapServer": "localhost:29092"
  },
  "AllowedUrls": [
    "http://localhost:5173"
  ]
}
ConnectionStrings = cadena de conexion a la base de datos 
kafka:bootstrapServer= url de conexion a kafka
AllowedUrls: lista de url que haran peticion al api(url de el client, frontend)
# 4 config front-end 
ejecutar comando npm i para instalar las dependencias del proyecto
# 5 crear arvhico .env con sus variables 
VITE_API_URL=http://example.com/api/
VITE_API_URL: url del api
