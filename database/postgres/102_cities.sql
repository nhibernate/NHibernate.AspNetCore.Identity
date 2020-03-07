-- table: public.cities

-- drop table public.cities;
-- create cities table;
create table public.cities
(
    id bigint not null default public.snow_flake_id(),
    name character varying(64) collate pg_catalog."default",
    population integer,
    constraint pk_cities primary key (id)
)
with (
    oids = false
)
tablespace pg_default;

alter table public.cities
    owner to postgres;
-- insert sample city data
insert into cities(name, population) values('bratislava', 432000);
insert into cities(name, population) values('budapest', 1759000);
insert into cities(name, population) values('prague', 1280000);
insert into cities(name, population) values('warsaw', 1748000);
insert into cities(name, population) values('los angeles', 3971000);
insert into cities(name, population) values('new york', 8550000);
insert into cities(name, population) values('edinburgh', 464000);
insert into cities(name, population) values('berlin', 3671000);

-- add city_id to app_users, constraint as foreign key;
alter table public.app_users
    add column city_id bigint;
alter table public.app_users
    add constraint fk_app_users_city_id foreign key (city_id)
    references public.cities (id) match simple
    on update no action
    on delete no action
    not valid;

