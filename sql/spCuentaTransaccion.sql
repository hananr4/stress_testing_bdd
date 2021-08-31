use StressTesting
GO
create or alter procedure spCuentaTransaccion
@cuentaId int,
@valor    money,
@signo    int
as
	declare  @saldo money

	if not exists ( select 1 from cuentas where cuentaId = @cuentaId )
	begin
		Insert into cuentas(cuentaId, saldo, fecha, cantidad, bloqueada)
		values ( @cuentaId, 0, GETDATE(), 0, 0)
	end

	Update cuentas
	set bloqueada = 1
	where cuentaId = @cuentaId
	

	select @saldo = saldo 
	from cuentas
	where cuentaId = @cuentaId
	
	set @saldo = @saldo + (@valor * case when @signo = 1 then 1 else -1 end)

	update cuentas
	set saldo							= @saldo,
			ultimaTransaccion = GETDATE(),
			cantidad					= cantidad + 1
	where cuentaId = @cuentaId

	Insert into transacciones 
		( cuentaId, fecha, valor, signo, saldo )
	values 
		( @cuentaId, GETDATE(), @valor, @signo, @saldo )


	Update cuentas
	set bloqueada = 0
	where cuentaId = @cuentaId
	
	--WAITFOR DELAY '00:00:01'
go
