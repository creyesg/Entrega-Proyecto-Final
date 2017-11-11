CREATE OR REPLACE VIEW view_paq_proc
 AS 
 SELECT ord.ORDER_ID, ord.NOMBRE_FACTURA, ord.CLIENT_ID, ord.DIRECCION, ord.FEC_ORDER AS fecha_orden,
 env.FEC_ENVIO AS fecha_envio, trk.FEC_TRACK AS Fecha_entrega
 FROM ORDERS ord
 INNER JOIN ENVIOS env ON env.ORDER_ID = ord.ORDER_ID
 AND env.FEC_ENVIO =(SELECT MAX(env2.FEC_ENVIO) FROM ENVIOS env2 WHERE env2.ORDER_ID = env.ORDER_ID)
 INNER JOIN TRACKING_T trk ON trk.ORDER_ID = ord.ORDER_ID
 AND trk.FEC_TRACK = (select max(trk2.fec_track) from TRACKING_T trk2 WHERE trk2.ORDER_ID = trk.ORDER_ID)
 WHERE ord.ID_STATUS <> 5
 ORDER BY ord.ORDER_ID;




 
 
 
