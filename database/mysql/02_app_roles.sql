create table app_roles (
  id varchar(32) not null comment 'app user id',
  description varchar(256) null,
  constraint pk_app_roles primary key (id),
  constraint fk_app_roles_aspnet_roles foreign key (id)
    references aspnet_roles (id)
    on update cascade
    on delete cascade
) comment 'App roles table.';
