#docker run --name tete-db -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=tetePassword!' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-CU8-ubuntu
ARG saPassword=blah

FROM mcr.microsoft.com/mssql/server:2017-CU8-ubuntu


ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=$saPassword

EXPOSE 1433

CMD /opt/mssql/bin/sqlservr