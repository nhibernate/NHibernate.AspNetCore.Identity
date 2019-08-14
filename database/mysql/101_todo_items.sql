-- auto-generated definition
create table todo_items
(
  id bigint auto_increment comment 'todo item id',
  title varchar(32)not null comment 'todo item title',
  description varchar(128) null comment 'todo item description',
  completed tinyint(1) default 0 not null comment 'todo item is completed.',
  user_id varchar(32)not null comment 'todo item user id',
  constraint pk_todo_items primary key (id),
  constraint fk_todo_items_users
    foreign key (user_id) references app_users (id)
      on update cascade
      on delete cascade
)
comment 'todo items table';
