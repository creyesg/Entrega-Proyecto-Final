CREATE OR REPLACE PACKAGE ENVIOS_UTILS_PKG AS
/***************************************************************************\
* CREYES 13/10/2017
\***************************************************************************/
TYPE t_cursor IS REF CURSOR;

PROCEDURE crea_actualiza_track(p_order_id NUMBER,
                              p_emisor_alias VARCHAR2 DEFAULT NULL, 
                              p_receptor_alias VARCHAR2 DEFAULT NULL,
                              p_observaciones VARCHAR2 DEFAULT NULL, 
                              p_id_statud_track NUMBER,
                              p_coordenadas VARCHAR2 DEFAULT NULL, 
                              p_track_id NUMBER DEFAULT NULL
                              );


PROCEDURE crear_actualiza_orden(p_NOMBRE_FACTURA VARCHAR2 DEFAULT NULL,--1
                      p_nit VARCHAR2 DEFAULT NULL,
                      p_direccion VARCHAR2 DEFAULT NULL, 
                      p_client_id NUMBER DEFAULT NULL, 
                      p_status NUMBER DEFAULT NULL, 
                      p_peso_total NUMBER DEFAULT NULL, 
                      p_tamanio_total NUMBER DEFAULT NULL, 
                      p_order_id NUMBER DEFAULT NULL
                      );
                      
PROCEDURE crea_actualiza_detalle_order(p_COD_PRODUCTO NUMBER DEFAULT NULL, --2
                             p_PESO NUMBER DEFAULT NULL, 
                             p_tamanio NUMBER DEFAULT NULL, 
                             p_order_id NUMBER, 
                             p_det_id NUMBER DEFAULT NULL);
                             
                             
PROCEDURE crea_envio(--3
p_ID_EMPRESA NUMBER DEFAULT 0, 
p_SN_INTERNO NUMBER, 
p_COD_EMP_INTERNO NUMBER DEFAULT 0, 
p_ORDER_ID NUMBER
);


END ENVIOS_UTILS_PKG;

CREATE OR REPLACE PACKAGE BODY ENVIOS_UTILS_PKG AS

/* body */


PROCEDURE crea_actualiza_track(p_order_id NUMBER,
                              p_emisor_alias VARCHAR2 DEFAULT NULL, 
                              p_receptor_alias VARCHAR2 DEFAULT NULL,
                              p_observaciones VARCHAR2 DEFAULT NULL, 
                              p_id_statud_track NUMBER,
                              p_coordenadas VARCHAR2 DEFAULT NULL, 
                              p_track_id NUMBER DEFAULT NULL
                              )
AS 

v_track_id NUMBER(18);
v_status_curr NUMBER(18);

BEGIN 

IF p_track_id IS NULL THEN 
  SELECT track_secuence.NEXTVAL into v_track_id FROM dual;
  
  INSERT INTO TRACKING_T (TRACK_ID, FEC_TRACK, EMISOR_ALIAS, RESEPTOR_ALIAS, TXT_OBSERVACIONES, ID_STATUS, ORDER_ID, coordenadas)
  VALUES(v_track_id, SYSDATE, p_emisor_alias, p_receptor_alias, p_observaciones, p_id_statud_track, p_order_id, p_coordenadas);

ELSE
v_track_id := p_track_id;

UPDATE TRACKING_T SET EMISOR_ALIAS = nvl(p_emisor_alias,EMISOR_ALIAS), 
                      RESEPTOR_ALIAS = nvl(p_receptor_alias, RESEPTOR_ALIAS), 
                      TXT_OBSERVACIONES = nvl(p_observaciones,TXT_OBSERVACIONES), 
                      ID_STATUS = p_id_statud_track, 
                      ORDER_ID = p_order_id, 
                      coordenadas = nvl(p_coordenadas, coordenadas)
WHERE TRACK_ID = v_track_id;

END IF;


/*Actualiza la orden con respecto al estado del track*/

IF p_id_statud_track = 4 THEN --Entregado ok
    BEGIN --Cambia el estado de la orden
    v_status_curr := 5; --Entregado OK
    ENVIOS_UTILS_PKG.crear_actualiza_orden(NULL, NULL, NULL, NULL, v_status_curr, NULL, NULL, p_order_id);
    END;
ELSIF p_id_statud_track = 5 THEN --Cancelado
    IF p_emisor_alias = 'SISTEMA' THEN 
        v_status_curr := 4; --Cerrada por la empresa
    ELSE
        v_status_curr := 3; --Cerrada por el Cliente
    END IF;

    BEGIN --Cambia el estado de la orden
        ENVIOS_UTILS_PKG.crear_actualiza_orden(NULL, NULL, NULL, NULL, v_status_curr, NULL, NULL, p_order_id);
    END;
END IF;

END crea_actualiza_track;




PROCEDURE crear_actualiza_orden(p_NOMBRE_FACTURA VARCHAR2 DEFAULT NULL,
                      p_nit VARCHAR2 DEFAULT NULL,
                      p_direccion VARCHAR2 DEFAULT NULL, 
                      p_client_id NUMBER DEFAULT NULL, 
                      p_status NUMBER DEFAULT NULL, 
                      p_peso_total NUMBER DEFAULT NULL, 
                      p_tamanio_total NUMBER DEFAULT NULL, 
                      p_order_id NUMBER DEFAULT NULL
                      )
AS

v_order_id NUMBER(18);
BEGIN 

IF p_order_id IS NULL THEN 
  SELECT order_secuence.NEXTVAL into v_order_id FROM dual;
  INSERT INTO ORDERS (ORDER_ID,NOMBRE_FACTURA, NIT, DIRECCION, CLIENT_ID, ID_STATUS, FEC_ORDER, PESO_TOTAL, TAMANIO_TOTAL)
  VALUES (v_order_id, p_NOMBRE_FACTURA, p_nit, p_direccion, p_client_id, p_status, SYSDATE, p_peso_total, p_tamanio_total);
ELSE 

v_order_id := p_order_id;

UPDATE ORDERS SET NOMBRE_FACTURA = nvl(p_NOMBRE_FACTURA,NOMBRE_FACTURA), 
                  NIT = nvl(p_nit, NIT), 
                  DIRECCION = nvl(p_direccion, DIRECCION), 
                  CLIENT_ID = nvl(p_client_id, CLIENT_ID), 
                  ID_STATUS = p_status, 
                  PESO_TOTAL = nvl(p_peso_total, PESO_TOTAL), 
                  TAMANIO_TOTAL = nvl(p_tamanio_total, TAMANIO_TOTAL)
WHERE ORDER_ID = v_order_id;

END IF;

END crear_actualiza_orden; 


PROCEDURE crea_actualiza_detalle_order(p_COD_PRODUCTO NUMBER DEFAULT NULL, 
                             p_PESO NUMBER DEFAULT NULL, 
                             p_tamanio NUMBER DEFAULT NULL, 
                             p_order_id NUMBER, 
                             p_det_id NUMBER DEFAULT NULL)
AS

v_det_id NUMBER(18);
v_total_peso NUMBER(18,2);
v_total_tamanio NUMBER(18,2);
v_current_status NUMBER(18);

BEGIN


IF p_det_id IS NULL THEN 
  SELECT order_det_secuence.NEXTVAL into v_det_id FROM dual;
  
  INSERT INTO ORDERS_DET (DET_ID, COD_PRODUCTO, PESO, TAMAÑO, ORDER_ID)
  VALUES(v_det_id, p_COD_PRODUCTO, p_PESO, p_tamanio, p_order_id);

ELSE 

v_det_id := p_det_id;

UPDATE ORDERS_DET SET COD_PRODUCTO = nvl(p_COD_PRODUCTO,COD_PRODUCTO), 
                      PESO = nvl(p_PESO,PESO), 
                      TAMAÑO = nvl(p_tamanio,TAMAÑO)
WHERE  DET_ID = v_det_id;


END IF; 

COMMIT;

/* Actualiza los valores de peso total y tamanio total en orders */

SELECT SUM(PESO), SUM(TAMAÑO) INTO v_total_peso, v_total_tamanio 
FROM ORDERS_DET WHERE ORDER_ID = p_order_id;

 
SELECT ID_STATUS INTO v_current_status FROM ORDERS WHERE ORDER_ID = p_order_id;


BEGIN

ENVIOS_UTILS_PKG.crear_actualiza_orden(NULL, NULL, NULL, NULL, v_current_status, v_total_peso, v_total_tamanio, p_order_id);

END;

END crea_actualiza_detalle_order;




PROCEDURE crea_envio(
p_ID_EMPRESA NUMBER DEFAULT 0, 
p_SN_INTERNO NUMBER, 
p_COD_EMP_INTERNO NUMBER DEFAULT 0, 
p_ORDER_ID NUMBER
)
AS 

v_envio_id NUMBER(18);
v_current_status NUMBER(18);
v_receptor_alias VARCHAR2(150);
v_status_track NUMBER(18);

BEGIN 

  SELECT envio_secuence.NEXTVAL into v_envio_id FROM dual;
  
  INSERT INTO ENVIOS (ENVIO_ID, ID_EMPRESA, SN_INTERNO, ORDER_ID, COD_EMP_INTERNO, FEC_ENVIO)
  VALUES(v_envio_id, p_ID_EMPRESA, p_SN_INTERNO, p_ORDER_ID, p_COD_EMP_INTERNO, SYSDATE);
  
  COMMIT;
 
BEGIN --Cambia el estado de la orden
v_current_status := 2; --En proceso
ENVIOS_UTILS_PKG.crear_actualiza_orden(NULL, NULL, NULL, NULL, v_current_status, NULL, NULL, p_ORDER_ID);
END;

--Inicializa el Tracking de envío

IF p_SN_INTERNO = -1 THEN 
SELECT TRACK_ALIAS INTO v_receptor_alias FROM ENVIOS_INTERNOS WHERE COD_EMPLEADO = p_COD_EMP_INTERNO; 
ELSE 
SELECT TRACK_ALIAS INTO v_receptor_alias FROM EMPRESAS_ENVIO WHERE ID_EMPRESA = p_ID_EMPRESA;
END IF;


v_status_track := 1; --Iniciado

BEGIN 
ENVIOS_UTILS_PKG.crea_actualiza_track(p_ORDER_ID,
                              'SISTEMA', 
                              v_receptor_alias,
                              'Estimados por favor realizar el envío de la orden no. ' || p_ORDER_ID, 
                              v_status_track,
                              '', 
                              NULL
                              );
END;

END  crea_envio;

END ENVIOS_UTILS_PKG;


GRANT ALL ON ENVIOS_UTILS_PKG TO PUBLIC;

