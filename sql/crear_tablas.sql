create database StressTesting
go
use StressTesting
go
drop table if exists transacciones 
drop table if exists cuentas
go
create table cuentas (
	cuentaId          Int      not null primary key,
	saldo             Money    not null,
	fecha             Datetime not null,
	cantidad          Int      not null,
	ultimaTransaccion Datetime null,
	bloqueada		 bit      not null
)
go
create table transacciones (
	transaccionId   int identity(1,1) primary key,
	cuentaId	    int      not null,
	fecha			datetime not null,
	valor			money    not null,
	signo			int      not null,
	saldo			money    not null
)
go
