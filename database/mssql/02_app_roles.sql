-- auto-generated definition
create table AppRoles (
  Id nvarchar(32) not null,
  Description nvarchar(256) not null,
  constraint PK_AppRoles primary key (Id),
  constraint FK_AppRoles_AspNetRoles
  foreign key (Id) references AspNetRoles (Id)
    on update cascade
    on delete cascade
)
go
