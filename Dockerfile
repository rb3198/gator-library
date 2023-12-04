FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /gatorLibrary
COPY . .
RUN mkdir -p inputs
RUN mkdir -p outputs
RUN dotnet build
ENTRYPOINT [ "dotnet", "bin/Debug/net7.0/GatorLibrary.dll" ]