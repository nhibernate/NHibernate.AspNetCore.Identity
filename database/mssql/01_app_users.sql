-- auto-generated definition
create table AppUsers (
  Id nvarchar(32) not null,
  CreateTime datetime,
  LastLogin datetime,
  LoginCount int not null,
  constraint PK_AppUsers primary key (Id),
  constraint FK_AppUsers_AspNetUsers
    foreign key (Id) references AspNetUsers (Id)
    on update cascade
    on delete cascade
)
go
