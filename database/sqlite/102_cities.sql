drop table if exists cities;

create table cities (
    id bigint not null,
    name text not null unique,
    population int,
    primary key (id)
);
