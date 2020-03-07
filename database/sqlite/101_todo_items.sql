drop table if exists todo_items;

create table todo_items (
    id bigint not null,
    title text not null unique,
    description text,
    completed bool not null,
    user_id text,
    primary key (id)
);
