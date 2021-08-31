
create or alter procedure spCuentaCrear
@cuentaId int
as
	Insert into cuentas(cuentaId, saldo, fecha, cantidad)
	values ( @cuentaId, 0, GETDATE(), 0)
go