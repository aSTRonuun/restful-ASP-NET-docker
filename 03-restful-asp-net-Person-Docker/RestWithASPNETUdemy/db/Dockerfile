FROM mysql:5.7.22
EXPOSE 3306
COPY ./RestWithASPNETUdemy/DB/migrations/ /home/database/
COPY ./RestWithASPNETUdemy/DB/dataset/ /home/database/
COPY ./RestWithASPNETUdemy/CI/init_database.sh/ /docker-entrypoint-initdb.d/init_database.sh