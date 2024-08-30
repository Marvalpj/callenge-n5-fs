# Utilizar una imagen base de Node.js 20.16
FROM node:20-alpine as BUILD_IMAGE

# Establecer el directorio de trabajo en el contenedor
WORKDIR /app/react-app

# Copiar los archivos necesarios
COPY package.json .

# Instalar las dependencias, incluyendo las de desarrollo
RUN npm install

# Copiar el resto de los archivos
COPY . .

RUN npm run build

FROM node:20-alpine as PRODUCTION_IMAGE

WORKDIR /app/react-app

COPY --from=BUILD_IMAGE /app/react-app/dist/ /app/react-app/dist/ 

RUN npm install -g vite

COPY package.json .
COPY vite.config.js ./dist/

EXPOSE 8080

CMD ["vite", "preview", "--host", "--port", "8080"]
