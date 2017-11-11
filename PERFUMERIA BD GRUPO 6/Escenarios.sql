--crea orden
begin

ENVIOS_UTILS_PKG.crear_actualiza_orden('Libi Johana','6425648.8','residenciales azahares  casa 10 L SJP',1,5,0,0,null);

end;


select * from orders;

--crea orden detalle 

begin 

ENVIOS_UTILS_PKG.crea_actualiza_detalle_order(45,2,300,4,2);

end;



--Crea el envio 
begin

ENVIOS_UTILS_PKG.crea_envio(0,1,0,0);

end;



