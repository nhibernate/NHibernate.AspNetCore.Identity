drop table if exists app_users;

create table app_users (
    id text not null,
    create_time datetime not null,
    last_login datetime,
    login_count int not null,
    city_id bigint,
    primary key (id),
    constraint fk_ec9767ed foreign key (id) references aspnet_users
);
