drop table if exists aspnet_roles;

create table aspnet_roles (
  id varchar(32) not null constraint pk_aspnet_roles primary key,
  name varchar(64) not null,
  normalized_name   varchar(64) not null,
  concurrency_stamp varchar(36)
);

drop table if exists aspnet_role_claims;

create table aspnet_role_claims (
  id integer
    constraint pk_aspnet_role_claims
      primary key autoincrement,
  claim_type varchar(1024) not null,
  claim_value varchar(1024) not null,
  role_id varchar(32) not null
    constraint fk_aspnet_role_claims_role_id
      references aspnet_roles(id)
      on update cascade
      on delete cascade
);

drop table if exists aspnet_users;

create table aspnet_users (
    id text not null,
    user_name text not null unique,
    normalized_user_name text not null unique,
    email text not null,
    normalized_email text not null,
    email_confirmed bool not null,
    password_hash text,
    security_stamp text,
    concurrency_stamp text,
    phone_number text,
    phone_number_confirmed bool not null,
    two_factor_enabled bool not null,
    lockout_end_unix_time_seconds bigint,
    lockout_enabled bool not null,
    access_failed_count int not null,
    primary key (id)
);

drop table if exists aspnet_user_claims;

create table aspnet_user_claims (
    id  integer primary key autoincrement,
    claim_type text not null,
    claim_value text not null,
    user_id text not null
);

drop table if exists aspnet_user_logins;

create table aspnet_user_logins (
    login_provider text not null,
    provider_key text not null,
    provider_display_name text not null,
    user_id text not null,
    primary key (login_provider, provider_key)
);

drop table if exists aspnet_user_roles;

create table aspnet_user_roles (
    user_id text not null,
    role_id text not null,
    primary key (user_id, role_id)
);

drop table if exists aspnet_user_tokens;

create table aspnet_user_tokens (
    user_id text not null,
    login_provider text not null,
    name text not null,
    value text not null,
    primary key (user_id, login_provider, name)
);
