-- Cambios de DDL version 4.9.72
use dbSG2000
go

-- sp_helptext 'dbo.f_sepuedecompensar_2_0' 

IF exists (SELECT * FROM INFORMATION_SCHEMA.ROUTINES where SPECIFIC_NAME ='f_sepuedecompensar_2_1'  )
	DROP FUNCTION  [dbo].f_sepuedecompensar_2_1  

GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE FUNCTION  dbo.f_sepuedecompensar_2_1(@dtcupon DATETIME, @restriccion_dias INT, 
										@restriccion_Hora VARCHAR(8), @fecha_de_hoy DATETIME, @tpCupon CHAR(20),
										@flCobradoCliente as BIT, @dtCobradoCliente as DATE)  
RETURNS INT AS  
BEGIN 

--set dateformat dmy 
--declare @dtcupon datetime
--declare @restriccion_dias int 
--declare @restriccion_Hora varchar(9)
--declare @fecha_de_hoy	  datetime
--set @dtCupon='25/06/2008 05:59:59'
--set @fecha_de_hoy='26/06/2008 11:00:00'
--set @restriccion_dias=2
--set @restriccion_Hora='06:00:00'

DECLARE @dtcupon_hora_corte    DATETIME
DECLARE @sepuedecompensar      INT

	IF (@tpCupon = 'Tarjeta de Cr�dito' Or @tpCupon = 'Tarjeta de D�bito' Or @tpCupon = 'Todo Pago' )
	BEGIN
		if @flCobradoCliente = 0 
		BEGIN
			SET @sepuedecompensar=0
			return @sepuedecompensar; 
		END
		If @dtCobradoCliente <= @fecha_de_hoy 
			SET @sepuedeCompensar = 1
        ELSE  
			SET @sepuedeCompensar = 0
		RETURN @sepuedecompensar; 
	END


	If dateDiff(day, @dtcupon ,@fecha_de_hoy-@restriccion_dias) = 0 
	begin
		--Print dateDiff(day, @dtcupon ,@fecha_de_hoy - @restriccion_dias )
	        -- si estoy en el mismo dia, analizo la hora
        	set @dtcupon_hora_corte = convert(varchar,@fecha_de_hoy - @restriccion_dias,103) + ' ' + @restriccion_Hora
		--print @dtcupon_hora_corte
		--print DateDiff(minute, @dtcupon, @dtcupon_hora_corte)
	        If DateDiff(minute, @dtcupon, @dtcupon_hora_corte)>0 
		    set @sepuedecompensar=1;
		else
	            set @sepuedecompensar=0;
	end
	else
	begin

		--Print dateDiff(day, @dtcupon ,@fecha_de_hoy - @restriccion_dias )
		If dateDiff(day, @dtcupon ,@fecha_de_hoy - @restriccion_dias )>0
	            set @sepuedecompensar=1;
		else
	            set @sepuedecompensar=0;
	end

	--select @sepuedecompensar 'RESULTADO'

	------------------------------
	return @sepuedecompensar; 
END


/*

If (ptpCupon = "Tarjeta de Cr�dito" Or ptpCupon = "Tarjeta de D�bito" Or ptpCupon = "Todo Pago") Then
            
            ' Aqui tambien se debe Analizar la dtflCobradoalCliente
            ' MsgBox "Aqu� tambien se debe Analizar la Fecha de Cobro del viaje en tarjeta ", vbCritical
            
            If pflCobradoalCliente = "SI" Then
                Dim fecha_del_dia As Date
                pdtCobradoCliente = Left(pdtCobradoCliente, 10)
                fecha_del_dia = CDate(Left(Now(), 10))
            
                If CDate(pdtCobradoCliente) <= fecha_del_dia Then
                    sepuedeCompensar = True
                Else
                    sepuedeCompensar = False
                End If
            Else
                sepuedeCompensar = False
            End If
        End If

*/




-- sp_helptext 'sp_obtiene_falta_compensar_v4_2' 


GO



IF exists (SELECT * FROM INFORMATION_SCHEMA.ROUTINES where SPECIFIC_NAME ='sp_obtiene_falta_compensar_v4_9_72'  )
	DROP PROCEDURE  [dbo].sp_obtiene_falta_compensar_v4_9_72  

GO
	

/*
select * from tb_cupones where tpCupon = 'Todo Pago' and ( vlPagoEuros = 0 OR vlPagoDolares = 0 or vlPagoReales = 0  )  


Exec sp_obtiene_falta_compensar_v4_9_72
Exec sp_obtiene_falta_compensar_v4_2

*/
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

--- incluir logica de reales
CREATE  procedure sp_obtiene_falta_compensar_v4_9_72  
as  
begin  
declare @fecha_corte datetime  
declare @RESTRICCION_COMPENSACION_ACTIVADA    varchar(50)  
declare @RESTRICCION_COMPENSACION_HORA_PAGO   varchar(50)  
declare @RESTRICCION_COMPENSACION_DIAS_PAGO   int  
  
  
 select @RESTRICCION_COMPENSACION_ACTIVADA=vlParametro from TB_Parametros where dsParametro='RESTRICCION_COMPENSACION_ACTIVADA'  
 select @RESTRICCION_COMPENSACION_HORA_PAGO=vlParametro from TB_Parametros where  dsParametro='RESTRICCION_COMPENSACION_HORA_PAGO'  
 select @RESTRICCION_COMPENSACION_DIAS_PAGO=convert(int,vlParametro) from TB_Parametros where  dsParametro='RESTRICCION_COMPENSACION_DIAS_PAGO'  
  
 --print @RESTRICCION_COMPENSACION_ACTIVADA  
 --print @RESTRICCION_COMPENSACION_HORA_PAGO  
 --print @RESTRICCION_COMPENSACION_DIAS_PAGO  
   
  
 if @RESTRICCION_COMPENSACION_ACTIVADA='SI'  
 begin  
  select @RESTRICCION_COMPENSACION_HORA_PAGO=vlParametro from TB_Parametros where  dsParametro='RESTRICCION_COMPENSACION_HORA_PAGO'  
  select @RESTRICCION_COMPENSACION_DIAS_PAGO=convert(int,vlParametro) from TB_Parametros where  dsParametro='RESTRICCION_COMPENSACION_DIAS_PAGO'  
  
   
  ---SET @fecha_corte=Convert(datetime,CONVERT(varchar, getdate()-@RESTRICCION_COMPENSACION_DIAS_PAGO, 103)   
           ---+ ' '+@RESTRICCION_COMPENSACION_HORA_PAGO)  
   
  --PRINT @fecha_corte  
   
  SELECT --dtCupon, nrLicencia,  
         tpCupon,suma_vlPagoPesos = CASE tpCupon  
		   WHEN 'Contado' THEN  isnull(SUM(vlPagoPesos)- sum(vlComision+isnull(vlIVA,0)),0)  
           WHEN 'Cuenta Corriente' THEN isnull(SUM(vlMontoCupon)- sum(vlComision+isnull(vlIVA,0)),0)  
           WHEN 'Retorno' THEN isnull(SUM(0)- sum(vlComision+isnull(vlIVA,0)),0)  
           WHEN 'Cobro en Destino'   THEN isnull(SUM(0)- sum(vlComision+isnull(vlIVA,0)),0)  
		   WHEN 'Tarjeta de Cr�dito' THEN isnull(SUM(vlMontoCupon)- sum( vlComision+isnull(vlIVA,0) +  vlRecargoTarjeta  ),0)  
		   WHEN 'Tarjeta de D�bito'  THEN isnull(SUM(vlMontoCupon)- sum( vlComision+isnull(vlIVA,0) +  vlRecargoTarjeta  ),0)  
		   WHEN 'Todo Pago'			 THEN isnull(SUM(vlMontoCupon)- sum( vlComision+isnull(vlIVA,0) + vlRecargoTarjeta ),0)  
         END,  
         isnull(SUM(vlPagoEuros),0)   AS   suma_vlPagoEuros,   
         isnull(SUM(vlPagoDolares),0) AS   suma_vlPagoDolares, 
         isnull(SUM(vlPagoReales),0)  AS   suma_vlPagoReales
         INTO #tb_falta_compensar_agrupado  
  FROM TB_Cupones  
  WHERE (flCompensado = 0) AND (flAnulado = 0) AND   
      tpCupon IN ('Contado', 'Cuenta Corriente', 'Retorno','Cobro en Destino', 'Tarjeta de Cr�dito' ,  'Tarjeta de D�bito' , 'Todo Pago') AND   
      nrLicencia NOT IN (999, 998, 990)   
      and dbo.f_sepuedecompensar_2_0(dtCupon,@RESTRICCION_COMPENSACION_DIAS_PAGO,@RESTRICCION_COMPENSACION_HORA_PAGO,getdate())=1  
  Group by tpCupon --, dtCupon  
  
  select isnull(sum(suma_vlPagoPesos),0)   as suma_vlPagoPesos,  
         isnull(sum(suma_vlPagoEuros),0)   as suma_vlPagoEuros,  
         isnull(sum(suma_vlPagoDolares),0) as suma_vlPagoDolares,  
         isnull(sum(suma_vlPagoReales),0)  as suma_vlPagoReales, 
         (convert(varchar,getdate() - @RESTRICCION_COMPENSACION_DIAS_PAGO,103) + ' ' + @RESTRICCION_COMPENSACION_HORA_PAGO) as dtcupon_hora_corte  
  from #tb_falta_compensar_agrupado  
  
 end  
 else  
 begin   
  SELECT --dtCupon, nrLicencia,  
         tpCupon,suma_vlPagoPesos = CASE tpCupon  
		   WHEN 'Contado' THEN  isnull(SUM(vlPagoPesos)- sum(vlComision+isnull(vlIVA,0)),0)  
           WHEN 'Cuenta Corriente' THEN isnull(SUM(vlMontoCupon)- sum(vlComision+isnull(vlIVA,0)),0)  
           WHEN 'Retorno' THEN isnull(SUM(0)- sum(vlComision+isnull(vlIVA,0)),0)  
           WHEN 'Cobro en Destino'   THEN isnull(SUM(0)- sum(vlComision+isnull(vlIVA,0)),0)  
		   WHEN 'Tarjeta de Cr�dito' THEN isnull(SUM(vlMontoCupon)- sum( vlComision+isnull(vlIVA,0) +  vlRecargoTarjeta  ),0)  
		   WHEN 'Tarjeta de D�bito'  THEN isnull(SUM(vlMontoCupon)- sum( vlComision+isnull(vlIVA,0) +  vlRecargoTarjeta  ),0)  
		   WHEN 'Todo Pago'			 THEN isnull(SUM(vlMontoCupon)- sum( vlComision+isnull(vlIVA,0) + vlRecargoTarjeta ),0) 
         END,  
         isnull(SUM(vlPagoEuros),0)   AS   suma_vlPagoEuros,   
         isnull(SUM(vlPagoDolares),0) AS   suma_vlPagoDolares, 
         isnull(SUM(vlPagoReales),0)  AS   suma_vlPagoReales 
         INTO #tb_falta_compensar_agrupado_sin_restri  
  FROM TB_Cupones  
  WHERE (flCompensado = 0) AND (flAnulado = 0) AND   
      tpCupon IN ('Contado', 'Cuenta Corriente', 'Retorno','Cobro en Destino') AND   
      nrLicencia NOT IN (999, 998, 990)   
  Group by tpCupon  
  
  select isnull(sum(suma_vlPagoPesos),0)   as suma_vlPagoPesos,  
         isnull(sum(suma_vlPagoEuros),0)   as suma_vlPagoEuros,  
         isnull(sum(suma_vlPagoDolares),0) as suma_vlPagoDolares,  
         isnull(sum(suma_vlPagoReales),0)  as suma_vlPagoReales, 
         'Sin restricci�n'				   as dtcupon_hora_corte  
  from #tb_falta_compensar_agrupado_sin_restri  
  
   
 end   
  
end -- fin de procedure  
  


GO





