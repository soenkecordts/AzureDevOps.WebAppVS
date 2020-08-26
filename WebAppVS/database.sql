create database Lebensmittelretter
go
use Lebensmittelretter
go
create table [User]
(
    Email nvarchar(255) primary key,
    Password nvarchar(20)
)
go
insert into [user]
values ('muster@muster.de', 'muster')