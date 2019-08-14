create table TodoItems (
  Id bigint identity,
  Title nvarchar(32) not null,
  Description nvarchar(128),
  Completed bit default 0 not null,
  UserId nvarchar(32) not null,
  constraint pk_TodoItems primary key (Id),
  constraint fk_TodoItems_AppUsers
      foreign key (UserId) references AppUsers (Id)
      on update cascade
      on delete cascade
)
go

