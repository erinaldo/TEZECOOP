
SELECT *  FROM [dbSG2000].[dbo].[TB_MovimientosContables]
where nrCajaPuesto is not null

DELETE FROM [dbSG2000].[dbo].[TB_MovimientosContables]
where nrCajaPuesto is not null

SELECT * FROM  [TB_Cajas] WHERE flImputado=0

UPDATE [TB_Cajas] SET flImputado=0 WHERE nrcaja in(2000002233,2000002232)


select max(nrcaja) from [TB_Cajas] where nrPuesto = 2 
	

---  update tb_cajas set flImputado=0   where  nrcaja=2000002233

select * from  tb_cajas where  nrcaja=2000002233

select * from tb_movimientoscontables where nrcajapuesto is not null

--delete from tb_movimientoscontables where nrcajapuesto is not null

SELECT count(*) FROM TB_CUPONES where flCompensado = 0 and flanulado =0

-- Comensando todo para comenzar nuestras pruebas analiticas
update a set a.flCompensado=1, a.dtCompensado=getdate() FROM TB_CUPONES a where a.flCompensado = 0 and a.flanulado =0

--TU2
--generamos la caja puestos 2000002234
--grabamos viajes usamos la lic 124 


select * FROM TB_CUPONES a where a.flCompensado = 0 and a.flanulado =0
and nrCajaLicenciatario = 9000001101



update a set a.flCompensado=1, a.dtCompensado=getdate() FROM TB_CUPONES a where a.flCompensado = 0 and a.flanulado =0


select *  from  tb_cajas where nrcaja = 2000002234

update a set flImputado = 0  from  tb_cajas a where nrcaja = 2000002234

---imputamos la caja
---compense un ticket al contado de la lic 124

-- hay una grave problema a la hora de compensar comprobantes 
select *  from  tb_Cupones where nrCajaCliente = 2000002234

-- pendiente descompensar los cupones de la seleccion anterior

update  a
set  a.flCompensado=0, a.dtCompensado=null 
from tb_Cupones a 
where a.nrCajaCliente = 2000002234


select * from tb_movimientoscontables where nrcaja = 9000001101 


SELECT TB_MovimientosContables.nrCaja ,TB_MovimientosContables.IdMovimiento ,TB_MovimientosContables.nrCajaPuesto ,TB_MovimientosContables.dsConcepto ,TB_MovimientosContables.dtMovimiento ,TB_MovimientosContables.tpConcepto ,TB_MovimientosContables.tpCajaImputacion ,TB_MovimientosContables.dsUsuarioCajaPuesto ,TB_MovimientosContables.dsMovimiento ,TB_MovimientosContables.tpOperacion ,TB_MovimientosContables.nrRecibo ,TB_MovimientosContables.nrFactura ,TB_MovimientosContables.dsUsuario ,TB_MovimientosContables.vlPesos ,TB_MovimientosContables.vlDolares ,TB_MovimientosContables.vlEuros ,TB_MovimientosContables.dsProveedor ,TB_MovimientosContables.dsObservacion  FROM TB_MovimientosContables  WHERE    TB_MovimientosContables.nrCaja = '9000001101'   Order by TB_MovimientosContables.nrCajaPuesto ASC 



UPDATE [TB_Cajas] SET flImputado=0 WHERE nrcaja in(2000002235)
