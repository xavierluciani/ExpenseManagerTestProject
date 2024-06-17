/*==============================================================*/
/* Nom de SGBD :  Microsoft SQL Server 2022                     */
/*                                                              */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('EXPENSES') and o.name = 'FK_EXPENSES_REL_EXP_N_NATURES')
alter table EXPENSES
   drop constraint FK_EXPENSES_REL_EXP_N_NATURES
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('EXPENSES') and o.name = 'FK_EXPENSES_REL_USR_E_USERS')
alter table EXPENSES
   drop constraint FK_EXPENSES_REL_USR_E_USERS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('USERS') and o.name = 'FK_USERS_REL_USR_C_CURRENCI')
alter table USERS
   drop constraint FK_USERS_REL_USR_C_CURRENCI
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CURRENCIES')
            and   type = 'U')
   drop table CURRENCIES
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('EXPENSES')
            and   name  = 'REL_EXP_NAT_FK'
            and   indid > 0
            and   indid < 255)
   drop index EXPENSES.REL_EXP_NAT_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('EXPENSES')
            and   name  = 'REL_USR_EXP_FK'
            and   indid > 0
            and   indid < 255)
   drop index EXPENSES.REL_USR_EXP_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EXPENSES')
            and   type = 'U')
   drop table EXPENSES
go

if exists (select 1
            from  sysobjects
           where  id = object_id('NATURES')
            and   type = 'U')
   drop table NATURES
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('USERS')
            and   name  = 'REL_USR_CUR_FK'
            and   indid > 0
            and   indid < 255)
   drop index USERS.REL_USR_CUR_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('USERS')
            and   type = 'U')
   drop table USERS
go

/*==============================================================*/
/* Table : CURRENCIES                                           */
/*==============================================================*/
create table CURRENCIES (
   ID_CUR               int                  not null	identity(1,1),
   CUR_CODE             varchar(10)          null,
   CUR_NAME             varchar(100)         null,
   constraint PK_CURRENCIES primary key (ID_CUR)
)
go

/*==============================================================*/
/* Table : EXPENSES                                             */
/*==============================================================*/
create table EXPENSES (
   ID_EXP               int                  not null	identity(1,1),
   ID_NAT               int                  not null,
   ID_USR               int                  not null,
   EXP_DATE             datetime             null,
   EXP_AMOUNT           decimal(10,2)        null,
   EXP_COMMENTARY       varchar(300)         null,
   constraint PK_EXPENSES primary key (ID_EXP)
)
go

/*==============================================================*/
/* Index : REL_USR_EXP_FK                                       */
/*==============================================================*/




create nonclustered index REL_USR_EXP_FK on EXPENSES (ID_USR ASC)
go

/*==============================================================*/
/* Index : REL_EXP_NAT_FK                                       */
/*==============================================================*/




create nonclustered index REL_EXP_NAT_FK on EXPENSES (ID_NAT ASC)
go

/*==============================================================*/
/* Table : NATURES                                              */
/*==============================================================*/
create table NATURES (
   ID_NAT               int                  not null	identity(1,1),
   NAT_CODE             varchar(50)          null,
   NAT_NAME             varchar(50)          null,
   constraint PK_NATURES primary key (ID_NAT)
)
go

/*==============================================================*/
/* Table : USERS                                                */
/*==============================================================*/
create table USERS (
   ID_USR               int                  not null	identity(1,1),
   ID_CUR               int                  not null,
   NAME                 varchar(200)         null,
   SURNAME              varchar(200)         null,
   constraint PK_USERS primary key (ID_USR)
)
go

/*==============================================================*/
/* Index : REL_USR_CUR_FK                                       */
/*==============================================================*/




create nonclustered index REL_USR_CUR_FK on USERS (ID_CUR ASC)
go

alter table EXPENSES
   add constraint FK_EXPENSES_REL_EXP_N_NATURES foreign key (ID_NAT)
      references NATURES (ID_NAT)
go

alter table EXPENSES
   add constraint FK_EXPENSES_REL_USR_E_USERS foreign key (ID_USR)
      references USERS (ID_USR)
go

alter table USERS
   add constraint FK_USERS_REL_USR_C_CURRENCI foreign key (ID_CUR)
      references CURRENCIES (ID_CUR)
go

