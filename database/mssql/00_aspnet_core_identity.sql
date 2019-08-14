-- auto-generated definition
create table AspNetRoles (
  Id nvarchar(32) not null,
  Name nvarchar(256),
  NormalizedName nvarchar(256),
  ConcurrencyStamp nvarchar(1024),
  constraint pk_AspNetRoles primary key (id)
)
go

-- auto-generated definition
create table AspNetRoleClaims (
  Id int identity,
  RoleId nvarchar(32) not null,
  ClaimType nvarchar(1024),
  ClaimValue nvarchar(1024),
  constraint pk_AspNetRoleClaims primary key (Id),
  constraint fk_AspNetRoleClaims_AspNetRoles
    foreign key (RoleId) references AspNetRoles (Id)
    on delete cascade
    on update cascade
)
go

-- auto-generated definition
create table AspNetUsers (
  Id nvarchar(32) not null,
  UserName nvarchar(256),
  NormalizedUserName nvarchar(256),
  Email nvarchar(256),
  NormalizedEmail nvarchar(256),
  EmailConfirmed bit not null,
  PasswordHash nvarchar(1024),
  SecurityStamp nvarchar(1024),
  ConcurrencyStamp nvarchar(1024),
  PhoneNumber nvarchar(1024),
  PhoneNumberConfirmed bit not null,
  TwoFactorEnabled bit not null,
  LockoutEnd datetimeoffset,
  LockoutEnabled bit not null,
  AccessFailedCount int not null,
  constraint pk_AspNetUsers primary key (Id)
)
go

-- auto-generated definition
create table AspNetUserClaims (
  Id int identity,
  UserId nvarchar(32) not null,
  ClaimType nvarchar(1024),
  ClaimValue nvarchar(1024),
  constraint pk_AspNetUserClaims primary key (Id),
  constraint fk_AspNetUserClaims_AspNetUsers
    foreign key (UserId) references AspNetUsers (Id)
    on delete cascade
    on update cascade
)
go

-- auto-generated definition
create table AspNetUserLogins (
  LoginProvider nvarchar(128) not null,
  ProviderKey nvarchar(128) not null,
  ProviderDisplayName nvarchar(1024),
  UserId nvarchar(32) not null,
  constraint pk_AspNetUserLogins primary key (LoginProvider, ProviderKey),
  constraint fk_AspNetUserLogins_AspNetUsers
    foreign key (UserId) references AspNetUsers (Id)
    on delete cascade
    on update cascade
)
go

-- auto-generated definition
create table AspNetUserRoles (
  UserId nvarchar(32) not null,
  RoleId nvarchar(32) not null,
  constraint pk_AspNetUserRoles primary key (UserId, RoleId),
  constraint fk_AspNetUserRoles_AspNetUsers
    foreign key (UserId) references AspNetUsers (Id)
    on delete cascade
    on update cascade,
  constraint fk_AspNetUserRoles_AspNetRoles
    foreign key (RoleId) references AspNetRoles (Id)
    on delete cascade
    on update cascade
)
go

-- auto-generated definition
create table AspNetUserTokens (
  UserId nvarchar(32) not null,
  LoginProvider nvarchar(128) not null,
  Name nvarchar(128) not null,
  Value nvarchar(1024),
  constraint pk_AspNetUserTokens primary key (UserId, LoginProvider, Name),
  constraint fk_AspNetUserTokens_AspNetUsers
    foreign key (UserId) references AspNetUsers
    on delete cascade
    on update cascade
)
go

