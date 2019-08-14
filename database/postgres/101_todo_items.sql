create table public.todo_items
(
    id bigint not null default public.snow_flake_id(),
    title character varying(32) not null,
    description character varying(128),
    completed boolean not null default false,
    user_id character varying(32) not null,
    constraint pk_todo_items primary key (id),
    constraint fk_todo_items_users foreign key (user_id)
        references public.app_users (id) match simple
        on update no action
        on delete no action
)
with (
    oids = false
);

alter table public.todo_items
    owner to postgres;
comment on table public.todo_items
    is 'todo items';

comment on column public.todo_items.id
    is 'todo item id';

comment on column public.todo_items.title
    is 'todo item title.';

comment on column public.todo_items.description
    is 'todo item description';

comment on column public.todo_items.completed
    is 'is completed?';
