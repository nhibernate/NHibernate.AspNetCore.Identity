create table app_users (
  id varchar(32) not null comment 'app user id',
  create_time datetime null default now(),
  last_login datetime null,
  login_count int default 0,
  constraint pk_app_users primary key (id),
  constraint fk_app_users_aspnet_users foreign key (id)
    references aspnet_users (id)
    on update cascade
    on delete cascade
) comment 'App users table.';
