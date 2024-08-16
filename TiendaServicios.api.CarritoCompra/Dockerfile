# Establecer la imagen base para el SDK de .NET
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Establecer el directorio de trabajo
WORKDIR /app

# Copiar el archivo .csproj y restaurar las dependencias
COPY TiendaServicios.api.CarritoCompra/*.csproj ./TiendaServicios.api.CarritoCompra/
RUN dotnet restore TiendaServicios.api.CarritoCompra/TiendaServicios.api.CarritoCompra.csproj

# Copiar el resto de los archivos y construir la aplicación
COPY TiendaServicios.api.CarritoCompra/ ./TiendaServicios.api.CarritoCompra/
WORKDIR /app/TiendaServicios.api.CarritoCompra
RUN dotnet build -c Release -o /app/build

# Establecer la imagen base para el runtime de .NET
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# Establecer el directorio de trabajo
WORKDIR /app

# Copiar los archivos de la compilación desde el contenedor anterior
COPY --from=build-env /app/build .

# Exponer el puerto y ejecutar la aplicación
EXPOSE 80
ENTRYPOINT ["dotnet", "TiendaServicios.api.CarritoCompra.dll"]

