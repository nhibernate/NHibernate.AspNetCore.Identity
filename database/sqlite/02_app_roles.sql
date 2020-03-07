drop table if exists app_roles;

create table app_roles (
  id text not null,
  description text,
  primary key (id),
  constraint fk_98b95005 foreign key (id) references aspnet_roles
);
