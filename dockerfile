FROM mysql:latest

COPY init.sql /docker-entrypoint-initdb.d/

RUN chown -R mysql: /docker-entrypoint-initdb.d

EXPOSE 3306

CMD ["mysqld"]