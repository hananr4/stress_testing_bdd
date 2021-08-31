use StressTesting
--exec spCuentaCrear 1
--exec spCuentaCrear 2

--exec spCuentaTransaccion 1, 10.00  ,   1
--exec spCuentaTransaccion 2, 41.00  ,   1
--exec spCuentaTransaccion 1,  2.50  ,  -1
--exec spCuentaTransaccion 2,  5.50  ,  -1
--exec spCuentaTransaccion 1,  1.20  ,  -1
--exec spCuentaTransaccion 2,  6.10  ,   1
--exec spCuentaTransaccion 1,  0.75  ,  -1
--exec spCuentaTransaccion 2,  1.10  ,   1

select * from cuentas
select * from transacciones


select count(*) from cuentas
select count(*) from transacciones

sp_who

select * from cuentas

select cuentas.cuentaId, SUM ( valor * signo ), cuentas.saldo, COUNT(*), cuentas.cantidad
from cuentas
join transacciones 
on cuentas.cuentaId = transacciones.cuentaId
group by cuentas.cuentaId, cuentas.saldo, cuentas.cantidad
having cuentas.saldo <> SUM ( valor * signo )


select cuentas.cuentaId, SUM ( valor * signo ), cuentas.saldo, COUNT(*), cuentas.cantidad
from cuentas
join transacciones 
on cuentas.cuentaId = transacciones.cuentaId
group by cuentas.cuentaId, cuentas.saldo, cuentas.cantidad
order by cuentas.cuentaId

select * from cuentas where cuentaId = 1
select * from transacciones where cuentaId = 1
select SUM (valor * signo ) from transacciones where cuentaId = 1
