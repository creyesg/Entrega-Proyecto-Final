
CREATE TABLE STATUS_TRACK(
ID_STATUS NUMBER(18),
DESCRIPTION_STATUS VARCHAR2(150),
PRIMARY KEY(ID_STATUS)
);
GRANT ALL ON STATUS_TRACK TO PUBLIC;

CREATE TABLE STATUS_ORDER(
ID_STATUS NUMBER(18),
DESCRIPTION_STATUS VARCHAR2(150),
PRIMARY KEY(ID_STATUS)
);
GRANT ALL ON STATUS_ORDER TO PUBLIC;



CREATE TABLE ORDERS (
ORDER_ID NUMBER(18),
NOMBRE_FACTURA VARCHAR2(300), 
NIT VARCHAR2(10), 
DIRECCION VARCHAR2(250), 
CLIENT_ID NUMBER(18),
ID_STATUS NUMBER(18), 
FEC_ORDER DATE, 
PESO_TOTAL NUMBER(18,2),
TAMANIO_TOTAL NUMBER(18,2),
PRIMARY KEY(ORDER_ID), 
CONSTRAINT fk_ORDERSTATUS
    FOREIGN KEY (ID_STATUS)
    REFERENCES STATUS_ORDER(ID_STATUS)
);
GRANT ALL ON ORDERS TO PUBLIC;


CREATE TABLE EMPRESAS_ENVIO(
ID_EMPRESA NUMBER(18), 
NOMBRE_EMPRESA VARCHAR2(200),
TARIFA_COBRO NUMBER(18,2),
TRACK_ALIAS VARCHAR2(150), 
PRIMARY KEY(ID_EMPRESA)
);

GRANT ALL ON EMPRESAS_ENVIO TO PUBLIC;


CREATE TABLE ENVIOS_INTERNOS(
 COD_EMPLEADO NUMBER(18), 
 TRACK_ALIAS VARCHAR2(150),
 PRIMARY KEY(COD_EMPLEADO)
);

GRANT ALL ON ENVIOS_INTERNOS TO PUBLIC;



--DROP TABLE ENVIOS


CREATE TABLE ENVIOS(
ENVIO_ID NUMBER(18), 
ID_EMPRESA NUMBER(18), 
SN_INTERNO NUMBER(5), 
ORDER_ID NUMBER(18), 
COD_EMP_INTERNO NUMBER(18),
FEC_ENVIO DATE,  
PRIMARY KEY(ENVIO_ID), 
CONSTRAINT fk_ENVIOEMPRESA
    FOREIGN KEY (ID_EMPRESA)
    REFERENCES EMPRESAS_ENVIO(ID_EMPRESA), 
CONSTRAINT fk_ENVIOEMP
    FOREIGN KEY (COD_EMP_INTERNO)
    REFERENCES ENVIOS_INTERNOS(COD_EMPLEADO),
 CONSTRAINT fk_ENVIOORDEN
    FOREIGN KEY (ORDER_ID)
    REFERENCES ORDERS(ORDER_ID)
);

GRANT ALL ON ENVIOS TO PUBLIC;


--DROP TABLE TRACKING_T;

CREATE TABLE TRACKING_T(
TRACK_ID NUMBER(18), 
FEC_TRACK DATE,
EMISOR_ALIAS VARCHAR2(150), 
RESEPTOR_ALIAS VARCHAR2(150), 
TXT_OBSERVACIONES VARCHAR2(500), 
ID_STATUS NUMBER(18), 
ORDER_ID NUMBER(18),
coordenadas VARCHAR2(200),
PRIMARY KEY(TRACK_ID),
CONSTRAINT fk_TRACKSTATUS
    FOREIGN KEY (ID_STATUS)
    REFERENCES STATUS_TRACK(ID_STATUS), 
CONSTRAINT fk_ORDERTRACK
    FOREIGN KEY (ORDER_ID)
    REFERENCES ORDERS(ORDER_ID)
);

GRANT ALL ON TRACKING_T TO PUBLIC;


CREATE TABLE ORDERS_DET(
DET_ID NUMBER(18),
COD_PRODUCTO VARCHAR2(150), 
PESO NUMBER(18,2), 
TAMA�O NUMBER(18,2), 
ORDER_ID NUMBER(18), 
PRIMARY KEY(DET_ID), 
CONSTRAINT fk_ORDERDET
    FOREIGN KEY (ORDER_ID)
    REFERENCES ORDERS(ORDER_ID)
);


GRANT ALL ON ORDERS_DET TO PUBLIC;
