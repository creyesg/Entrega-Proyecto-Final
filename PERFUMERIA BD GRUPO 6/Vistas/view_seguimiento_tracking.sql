
CREATE OR REPLACE VIEW view_seguimiento_tracking
 AS 
SELECT trk.ORDER_ID, trk.FEC_TRACK, trk.EMISOR_ALIAS AS emisor, trk.RESEPTOR_ALIAS AS receptor, 
trk.TXT_OBSERVACIONES, trk.ID_STATUS, trk.coordenadas
FROM TRACKING_T trk
ORDER BY trk.FEC_TRACK;


 
