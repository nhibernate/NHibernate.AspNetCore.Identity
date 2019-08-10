-- table: public.app_users

-- drop table public.app_users;

create table public.app_users
(
    id character varying(32) collate pg_catalog."default" not null,
    create_time timestamp without time zone not null default now(),
    last_login timestamp without time zone,
    login_count integer default 0,
    constraint pk_app_users primary key (id),
    constraint fk_aspnet_users_id foreign key (id)
        references public.aspnet_users (id) match simple
        on update cascade
        on delete cascade
)
with (
    oids = false
)
tablespace pg_default;

alter table public.app_users
    owner to postgres;
comment on table public.app_users
    is 'application users table.';
