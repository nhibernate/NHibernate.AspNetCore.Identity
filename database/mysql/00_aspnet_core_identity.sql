create database net_core_app character set utf8 collate utf8_general_ci;

use nhibernate;

create table aspnet_roles (
  id varchar(32) not null comment 'id of user',
  name varchar(64) null comment 'name of user',
  normalized_name varchar(64) null comment 'normalized name of user',
  concurrency_stamp varchar(36) null comment 'concurrency stamp',
  constraint pk_aspnet_roles primary key (id)
)
comment 'aspnet core ideitity roles';

create table aspnet_role_claims (
  id int auto_increment comment 'role claim id',
  claim_type varchar(1024) not null comment 'claim type',
  claim_value varchar(1024) not null comment 'claim value',
  role_id varchar(32) not null comment 'role id',
  constraint pk_aspnet_role_claims primary key (id),
  constraint fk_aspnet_role_claims_roles
    foreign key (role_id) references aspnet_roles (id)
    on update cascade
    on delete cascade
)
comment 'aspnet core identity role claims';

create table aspnet_users (
  id varchar(32) not null comment 'user id',
  user_name varchar(64) not null comment 'user name',
  normalized_user_name varchar(64) not null comment 'normalized user name',
  email varchar(256) not null comment 'email',
  normalized_email varchar(256) null comment 'normalized email',
  email_confirmed tinyint(1) default 0 not null comment 'email confirmed',
  password_hash varchar(256) null comment 'password hash',
  security_stamp varchar(64) null comment 'security stamp',
  concurrency_stamp varchar(36) null comment 'concurrency stamp',
  phone_number varchar(128) null comment 'phone number',
  phone_number_confirmed tinyint(1) default 0 not null comment 'phone number confirmed',
  two_factor_enabled tinyint(1) default 0 not null comment 'two factor enabled',
  lockout_end_unix_time_seconds bigint null comment 'lockout end unix time in seconds',
  lockout_enabled tinyint(1) default 0 not null comment 'lockout enabled',
  access_failed_count int null comment 'access failed count',
  constraint pk_aspnet_users primary key (id),
  constraint ui_aspnet_users_email unique (email),
  constraint ui_aspnet_users_normalized_user_name unique (normalized_user_name),
  constraint ui_aspnet_users_user_name unique (user_name)
)
comment 'aspnet identity users';

create table aspnet_user_claims (
  id int auto_increment comment 'user claim id',
  claim_type varchar(1024) not null comment 'claim type',
  claim_value varchar(1024) not null comment 'claim value',
  user_id varchar(32) not null comment 'user id',
  constraint pk_aspnet_user_claims primary key(id),
  constraint fk_aspnet_user_claims_users
    foreign key (user_id) references aspnet_users (id)
    on update cascade
    on delete cascade
)
comment 'aspnet core identity user claims';

create table aspnet_user_logins (
  login_provider varchar(32) not null comment 'login provider',
  provider_key varchar(256) not null comment 'login provider key',
  provider_display_name varchar(256) not null comment 'login provider display name',
  user_id varchar(32) not null comment 'user id',
  constraint pk_aspnet_user_logins primary key (login_provider, provider_key),
  constraint fk_aspnet_user_logins_user
    foreign key (user_id) references aspnet_users (id)
    on update cascade
    on delete cascade
)
comment 'aspnet user logins';

create table aspnet_user_roles (
  user_id varchar(32) not null comment 'user id',
  role_id varchar(32) not null comment 'role id',
  constraint pk_aspnet_user_roles primary key (user_id, role_id),
  constraint fk_aspnet_user_roles_roles
    foreign key (role_id) references aspnet_roles (id)
    on update cascade
    on delete cascade,
  constraint fk_aspnet_user_roles_users
    foreign key (user_id) references aspnet_users (id)
    on update cascade
    on delete cascade
)
comment 'aspnet user roles relation table.';

create table aspnet_user_tokens(
  user_id varchar(32) not null comment 'user id',
  login_provider varchar(32) not null comment 'login provider for this token',
  name varchar(32) not null comment 'token name',
  value varchar(256) not null comment 'token value',
  constraint pk_aspnet_user_tokens primary key (user_id, login_provider, name),
  constraint fk_aspnet_user_tokens_users
    foreign key (user_id) references aspnet_users (id)
    on update cascade
    on delete cascade
)
comment 'aspnet user tokens';
