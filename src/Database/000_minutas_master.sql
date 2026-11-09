/*
MNTA - MINUTAS DE TRABAJO
MNTAS - MINUTA ASISTENTES
MNTTE - MINUTA TEMAS
MNTAC - MINUTA ACUERDOS
MNTTA - MINUTA TAREAS
MNTAR - MINUTA AUDITORIA
*/

CREATE TABLE `MNTA` (
	`MNTA_ID` BIGINT(20) NOT NULL AUTO_INCREMENT COMMENT 'indice',
	`MNTA_FOLIO` VARCHAR(30) NOT NULL COMMENT 'folio de minuta' COLLATE 'utf8mb4_unicode_ci',
	`MNTA_TTLO` VARCHAR(200) NOT NULL COMMENT 'titulo de la reunion' COLLATE 'utf8mb4_unicode_ci',
	`MNTA_CTTP_ID` VARCHAR(30) NOT NULL COMMENT 'catalogo de tipos de reuniones' COLLATE 'utf8mb4_unicode_ci',
	`MNTA_FCREU` DATE NOT NULL COMMENT 'fecha de reunion',
	`MNTA_HRINI` TIME NOT NULL COMMENT 'hora de inicio',
	`MNTA_HRFIN` TIME NULL DEFAULT NULL COMMENT 'hora de finalizacion',
	`MNTA_UBICA` VARCHAR(250) NULL DEFAULT NULL COMMENT 'ubicacion' COLLATE 'utf8mb4_unicode_ci',
	`MNTA_PLTFRMA` VARCHAR(100) NULL DEFAULT NULL COMMENT 'plataforma' COLLATE 'utf8mb4_unicode_ci',
	`MNTA_OBJTVO` TEXT NOT NULL COMMENT 'objetivo de la reunion' COLLATE 'utf8mb4_unicode_ci',
	`MNTA_CTDPTO_ID` BIGINT(20) NULL DEFAULT NULL COMMENT 'indice del catalogo de departamento',
	`MNTA_PRYCT_ID` BIGINT(20) NULL DEFAULT NULL COMMENT 'indice del proyecto ',
	`MNTA_DRCTR_ID` BIGINT(20) NULL DEFAULT NULL COMMENT 'indice del catalogo de clientes',
	`MNTA_STTS_ID` VARCHAR(30) NOT NULL DEFAULT 'Borrador' COMMENT 'estado de la minuta' COLLATE 'utf8mb4_unicode_ci',
	`MNTA_FCPRO` DATETIME NULL DEFAULT NULL COMMENT 'proxima fecha',
	`MNTA_UBICAP` VARCHAR(250) NULL DEFAULT NULL COMMENT 'proxima ubicacion' COLLATE 'utf8mb4_unicode_ci',
	`MNTA_USR_ID` BIGINT(20) NOT NULL COMMENT 'creador por',
	`MNTA_FN` DATETIME NOT NULL DEFAULT current_timestamp() COMMENT 'fecha de creacion',
	`MNTA_FCACT` DATETIME NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp() COMMENT 'fecha de actulizacion',
	`MNTA_VERFIL` INT(11) NOT NULL DEFAULT '1' COMMENT 'numero de version de la fila',
	`MNTA_A` TINYINT(1) NOT NULL DEFAULT '0' COMMENT 'registro activo',
	PRIMARY KEY (`MNTA_ID`) USING BTREE,
	UNIQUE INDEX `UK_MINUTAS_FOLIO` (`MNTA_FOLIO`) USING BTREE,
	INDEX `IX_MINUTAS_FECHA` (`MNTA_FCREU`) USING BTREE,
	INDEX `IX_MINUTAS_ESTADO_FECHA` (`MNTA_STTS_ID`, `MNTA_FCREU`) USING BTREE,
	INDEX `IX_MINUTAS_PROYECTO` (`MNTA_PRYCT_ID`) USING BTREE,
	INDEX `IX_MINUTAS_CLIENTE` (`MNTA_DRCTR_ID`) USING BTREE,
	INDEX `IX_MINUTAS_CREADO_POR` (`MNTA_USR_ID`) USING BTREE
)
COMMENT='tabla de minutas'
COLLATE='utf8mb4_unicode_ci'
ENGINE=InnoDB
;

CREATE TABLE `MNTAS` (
	`MNTAS_ID` BIGINT(20) NOT NULL AUTO_INCREMENT COMMENT 'indice incremental',
	`MNTAS_MNTA_ID` BIGINT(20) NOT NULL COMMENT 'indice de la minuta',
	`MNTAS_USR_ID` BIGINT(20) NULL DEFAULT NULL COMMENT 'indice del usuario',
	`MNTAS_NOM` VARCHAR(200) NOT NULL COMMENT 'nombre del asistente' COLLATE 'utf8mb4_unicode_ci',
	`MNTAS_MAIL` VARCHAR(254) NULL DEFAULT NULL COMMENT 'correo electronico' COLLATE 'utf8mb4_unicode_ci',
	`MNTAS_ROL` VARCHAR(100) NOT NULL COMMENT 'rol de la reunion' COLLATE 'utf8mb4_unicode_ci',
	`MNTAS_STTS_ID` VARCHAR(20) NOT NULL DEFAULT 'Convocado' COMMENT 'estado de la asistencia' COLLATE 'utf8mb4_unicode_ci',
	PRIMARY KEY (MNTAS_ID),
    UNIQUE KEY UK_MINUTA_ASISTENTE_USUARIO (MNTAS_MNTA_ID, MNTAS_USR_ID),
    KEY IX_ASISTENTES_USUARIO (MNTAS_USR_ID),
    CONSTRAINT FK_ASISTENTES_MINUTA FOREIGN KEY (MNTAS_MNTA_ID) REFERENCES MNTA(MNTA_ID)
)
COMMENT='tabla de asistentes'
COLLATE='utf8mb4_unicode_ci'
ENGINE=InnoDB
;

CREATE TABLE `MNTTE` (
	`MNTTE_ID` BIGINT(20) NOT NULL AUTO_INCREMENT COMMENT 'indice incremental',
	`MNTTE_MNTA_ID` BIGINT(20) NOT NULL COMMENT 'indice de la minuta',
	`MNTTE_ORDEN` INT(11) NOT NULL COMMENT 'orden del tema',
	`MNTTE_TEMA` VARCHAR(250) NOT NULL COMMENT 'nombre del tema' COLLATE 'utf8mb4_unicode_ci',
	`MNTTE_DESC` TEXT NULL DEFAULT NULL COMMENT 'descripcion del tema' COLLATE 'utf8mb4_unicode_ci',
	`MNTTE_DRCTR_ID` BIGINT(20) NULL DEFAULT NULL COMMENT 'indice del responsable',
	`MNTTE_STTS_ID` VARCHAR(20) NOT NULL DEFAULT 'Pendiente' COMMENT 'estado del tema' COLLATE 'utf8mb4_unicode_ci',
	PRIMARY KEY (`MNTTE_ID`) USING BTREE,
	UNIQUE INDEX `UK_MINUTA_TEMA_ORDEN` (`MNTTE_MNTA_ID`, `MNTTE_ORDEN`) USING BTREE,
	CONSTRAINT `FK_TEMAS_MINUTA` FOREIGN KEY (`MNTTE_MNTA_ID`) REFERENCES `MNTA` (`MNTA_ID`) ON UPDATE RESTRICT ON DELETE RESTRICT
)
COLLATE='utf8mb4_unicode_ci'
ENGINE=InnoDB
;

CREATE TABLE `MNTAC` (
	`MNTAC_ID` BIGINT(20) NOT NULL AUTO_INCREMENT COMMENT 'indice incremental',
	`MNTAC_MNTA_ID` BIGINT(20) NOT NULL COMMENT 'indice de la minuta',
	`MNTAC_TEMA_ID` BIGINT(20) NULL DEFAULT NULL COMMENT 'indice del tema',
	`MNTAC_CTTPO` VARCHAR(20) NOT NULL COMMENT 'tipo' COLLATE 'utf8mb4_unicode_ci',
	`MNTAC_DESC` TEXT NOT NULL COMMENT 'descripcion del acuerdo' COLLATE 'utf8mb4_unicode_ci',
	`MNTAC_ORDEN` INT(11) NOT NULL DEFAULT '1',
	`MNTAC_USR_N` BIGINT(20) NOT NULL COMMENT 'clave del usuario que registra',
	`MNTAC_FN` DATETIME NOT NULL DEFAULT current_timestamp() COMMENT 'fecha de registro',
	PRIMARY KEY (`MNTAC_ID`) USING BTREE,
	INDEX `IX_ACUERDOS_MINUTA` (`MNTAC_MNTA_ID`, `MNTAC_ORDEN`) USING BTREE,
	INDEX `FK_ACUERDOS_TEMA` (`MNTAC_TEMA_ID`) USING BTREE,
	CONSTRAINT `FK_ACUERDOS_MINUTA` FOREIGN KEY (`MNTAC_MNTA_ID`) REFERENCES `MNTA` (`MNTA_ID`) ON UPDATE RESTRICT ON DELETE RESTRICT,
	CONSTRAINT `FK_ACUERDOS_TEMA` FOREIGN KEY (`MNTAC_TEMA_ID`) REFERENCES `MNTTE` (`MNTTE_ID`) ON UPDATE RESTRICT ON DELETE RESTRICT
)
COMMENT='tabla de acuerdos'
COLLATE='utf8mb4_unicode_ci'
ENGINE=InnoDB
;

CREATE TABLE `MNTTA` (
	`MNTTA_ID` BIGINT(20) NOT NULL AUTO_INCREMENT COMMENT 'indice incremental',
	`MNTTA_MNTA_ID` BIGINT(20) NOT NULL COMMENT 'indice de la minuta',
	`MNTTA_MNTAC_ID` BIGINT(20) NULL DEFAULT NULL COMMENT 'indice del acuerdo',
	`MNTTA_DESC` TEXT NOT NULL COMMENT 'descripcion' COLLATE 'utf8mb4_unicode_ci',
	`MNTTA_DRCTR_ID` BIGINT(20) NOT NULL COMMENT 'indice del usuario responsable',
	`MNTTA_FCLIM` DATE NOT NULL COMMENT 'fecha limite de entrega',
	`MNTTA_ESTADO` VARCHAR(20) NOT NULL DEFAULT 'Pendiente' COMMENT 'estado de la tarea' COLLATE 'utf8mb4_unicode_ci',
	`MNTTA_FECFIN` DATETIME NULL DEFAULT NULL COMMENT 'fecha de finalizacion',
	`MNTTA_EVDCIA` TEXT NULL DEFAULT NULL COMMENT 'evidencia' COLLATE 'utf8mb4_unicode_ci',
	`MNTTA_USR_N` BIGINT(20) NOT NULL COMMENT 'creado por',
	`MNTTA_FN` DATETIME NOT NULL DEFAULT current_timestamp() COMMENT 'fecha de creacion',
	`MNTTA_FECACT` DATETIME NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp() COMMENT 'fecha de actualizacion o modificacion',
	PRIMARY KEY (`MNTTA_ID`) USING BTREE,
	INDEX `IX_TAREAS_RESPONSABLE_ESTADO` (`MNTTA_DRCTR_ID`, `MNTTA_ESTADO`, `MNTTA_FCLIM`) USING BTREE,
	INDEX `IX_TAREAS_MINUTA` (`MNTTA_MNTA_ID`) USING BTREE,
	INDEX `FK_TAREAS_ACUERDO` (`MNTTA_MNTAC_ID`) USING BTREE,
	CONSTRAINT `FK_TAREAS_ACUERDO` FOREIGN KEY (`MNTTA_MNTAC_ID`) REFERENCES `MNTAC` (`MNTAC_ID`) ON UPDATE RESTRICT ON DELETE RESTRICT,
	CONSTRAINT `FK_TAREAS_MINUTA` FOREIGN KEY (`MNTTA_MNTA_ID`) REFERENCES `MNTA` (`MNTA_ID`) ON UPDATE RESTRICT ON DELETE RESTRICT
)
COLLATE='utf8mb4_unicode_ci'
ENGINE=InnoDB
;

CREATE TABLE `MNTAR` (
	`MNTAR_ID` BIGINT(20) NOT NULL AUTO_INCREMENT,
	`MNTAR_MNTA_ID` BIGINT(20) NOT NULL,
	`MNTAR_DRCTR_ID` INT(11) NOT NULL,
	`MNTAR_ACCION` VARCHAR(40) NOT NULL COLLATE 'utf8mb4_unicode_ci',
	`MNTAR_FM` DATETIME NOT NULL DEFAULT current_timestamp(),
	`MNTAR_DANT` LONGTEXT NULL DEFAULT NULL COLLATE 'utf8mb4_unicode_ci',
	`MNTAR_DNUE` LONGTEXT NULL DEFAULT NULL COLLATE 'utf8mb4_unicode_ci',
	PRIMARY KEY (`MNTAR_ID`) USING BTREE,
	INDEX `IX_MINUTA_AUDITORIA_MINUTA_FECHA` (`MNTAR_MNTA_ID`, `MNTAR_FM`) USING BTREE,
	INDEX `IX_MINUTA_AUDITORIA_USUARIO_FECHA` (`MNTAR_DRCTR_ID`, `MNTAR_FM`) USING BTREE,
	CONSTRAINT `FK_MINUTA_AUDITORIA_MINUTA` FOREIGN KEY (`MNTAR_MNTA_ID`) REFERENCES `MNTA` (`MNTA_ID`) ON UPDATE RESTRICT ON DELETE RESTRICT
)
COLLATE='utf8mb4_unicode_ci'
ENGINE=InnoDB
;

CREATE TABLE `_user` (
	`_user_id` INT(11) NOT NULL AUTO_INCREMENT COMMENT 'id principal de la tabla',
	`_user_idroll` INT(11) NOT NULL DEFAULT '0' COMMENT 'ID de roll',
	`_user_iddir` INT(11) NOT NULL DEFAULT '0' COMMENT 'ID de directorio',
	`_user_a` SMALLINT(1) NOT NULL DEFAULT '1' COMMENT 'registro activo',
	`_user_clv` VARCHAR(14) NULL DEFAULT NULL COMMENT 'clave unica de registro en sistema' COLLATE 'utf8mb3_general_ci',
	`_user_apikey` VARCHAR(32) NULL DEFAULT NULL COMMENT 'clave de acceso al API de forma externa' COLLATE 'utf8mb3_general_ci',
	`_user_psw` VARCHAR(64) NOT NULL COMMENT 'password (md5 y base64)' COLLATE 'utf8mb3_general_ci',
	`_user_mail` VARCHAR(128) NOT NULL COMMENT 'correo electronic, separados por (;)' COLLATE 'utf8mb3_general_ci',
	`_user_nom` VARCHAR(128) NOT NULL COMMENT 'nombre' COLLATE 'utf8mb3_general_ci',
	`_user_fn` TIMESTAMP NULL DEFAULT current_timestamp() COMMENT 'fec. sist.',
	`_user_usr_n` VARCHAR(10) NULL DEFAULT NULL COMMENT 'usuario creo' COLLATE 'utf8mb3_general_ci',
	`_user_fm` DATETIME NULL DEFAULT NULL COMMENT 'Fecha de modificacion',
	`_user_usr_m` VARCHAR(10) NULL DEFAULT NULL COMMENT 'Usuario modifica' COLLATE 'utf8mb3_general_ci',
	`_user_tkn` VARCHAR(64) NULL DEFAULT NULL COMMENT 'Token' COLLATE 'utf8mb3_general_ci',
	PRIMARY KEY (`_user_id`) USING BTREE
)
COMMENT='catalogo de usuarios'
COLLATE='utf8mb3_general_ci'
ENGINE=InnoDB
AUTO_INCREMENT=29
;

INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (1, 1, 0, 1, 'ANHE1', '8969CC9DAFE1DFFC9A55CA7325D121D6', '$2y$10$rJIZADB4Fb6o4CRxpdKZXeV6qBFokrJUBh9bwtLp9evAoqV8XRybe', 'sistemas.anhe@ipo.com.mx', 'Antonio Hernández', '2018-02-23 14:37:49', 'ANHE1', '2025-05-27 14:51:08', 'EDITA', NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (2, 2, 0, 1, 'ANGU1', NULL, '$2a$10$1WsaQEPTXqEOtxVq2d17Iuhv5VuRwLaCnhzURYRaS/wF7zt26f0vm', 'adm.cobranza@ipo.com.mx', 'Ana Gurrola', '2018-03-22 12:50:13', 'ANHE1', NULL, NULL, NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (3, 3, 0, 1, 'NRCL1', NULL, '$2y$10$QUXelL0ZnkKyCvNKnCCS8u/l4g5gcS1grZze11uQVP9plSRxhxzRy', 'adm.audi@ipo.com.mx', 'Claudia Ramírez', '2018-03-22 12:58:10', 'ANHE1', NULL, NULL, NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (4, 3, 0, 1, 'RUHE1', NULL, '$2a$10$3o8k.KnqU0T.QXaWZ0FNV.4ozwtFFeYfaIKrjJ5uMVv5dSVr2Lxia', 'adm.pagos@ipo.com.mx', 'Ruth Y. Hernández Gómez', '2019-10-31 18:22:43', 'ANHE1', NULL, NULL, NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (5, 1, 0, 1, 'EDNA1', NULL, '$2y$10$rJIZADB4Fb6o4CRxpdKZXeV6qBFokrJUBh9bwtLp9evAoqV8XRybe', 'adm.dir@ipo.com.mx', 'Eduardo Nájera Águilar', '2019-10-31 18:28:08', 'ANHE1', NULL, NULL, NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (6, 2, 0, 1, 'JOCA1', NULL, '$2a$10$SukguoDiwMRywQvJHnFmBeiVCeLlbX.ncA.iPFHnaquo3L7giWAIa', 'adm.proyectos@ipo.com.mx', 'José Carlos Najera', '2019-12-03 02:04:48', 'ANHE1', NULL, NULL, NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (7, 1, 0, 1, 'FAUR1', NULL, '$2y$10$S/x5siTbIdYDvo1Z8MWu9O1qvfRVRfTrXyQpjzDjmR20sNv7ShtYe', 'ventas.dir@ipo.com.mx', 'Fabricio Urbina', '2019-12-24 09:46:59', 'ANHE1', NULL, NULL, NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (8, 1, 0, 1, 'LUNA1', NULL, '$2a$10$1WsaQEPTXqEOtxVq2d17Iuhv5VuRwLaCnhzURYRaS/wF7zt26f0vm', 'prod.dir@ipo.com.mx', 'José Luis Nájera Águilar', '2020-05-20 21:53:34', 'ANHE1', NULL, NULL, NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (9, 5, 0, 1, 'VIGL1', NULL, '$2y$10$6QKWjMYen.6Ydlp0IfMYa.Fp42a0QuBrf9BMRGmqx3j/9wI9Nidg.', 'vigilancia@ipo.com.mx', 'Vigilancia', '2020-05-20 21:53:34', 'ANHE1', NULL, NULL, NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (10, 7, 0, 1, 'OMSO1', NULL, '$2a$10$1WsaQEPTXqEOtxVq2d17Iuhv5VuRwLaCnhzURYRaS/wF7zt26f0vm', 'adm.almacen@ipo.com.mx', 'Omar Soto', '2020-05-21 11:19:04', 'ANHE1', NULL, NULL, NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (11, 5, 0, 1, 'MAJO1', NULL, '$2y$10$OVjw0y0oxXtSXXumBGr0HuG93uxFAmSO4eEVXwg75yOisqqlp5mAy', 'prod.audi@ipo.com.mx', 'Maria Jose Najera', '2020-05-21 12:19:24', 'ANHE1', NULL, NULL, NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (12, 5, 0, 1, 'RACA1', NULL, '$2y$10$OVjw0y0oxXtSXXumBGr0HuG93uxFAmSO4eEVXwg75yOisqqlp5mAy', 'envios1@ipo.com.mx', 'Rafael Cano Mercado', '2020-05-21 12:19:53', 'ANHE1', NULL, NULL, NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (13, 4, 0, 1, 'JUMO1', NULL, '$2y$10$OVjw0y0oxXtSXXumBGr0HuG93uxFAmSO4eEVXwg75yOisqqlp5mAy', 'design.dir@ipo.com.mx', 'Juan Carlos Montiel', '2020-05-21 12:20:56', 'ANHE1', NULL, NULL, NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (14, 4, 0, 1, 'ARCH1', NULL, '$2y$10$OVjw0y0oxXtSXXumBGr0HuG93uxFAmSO4eEVXwg75yOisqqlp5mAy', 'prod.posprensa@ipo.com.mx', 'Arturo Chavarria', '2020-05-21 12:21:41', 'ANHE1', NULL, NULL, NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (15, 8, 0, 1, 'MORO1', NULL, '$2a$10$1WsaQEPTXqEOtxVq2d17Iuhv5VuRwLaCnhzURYRaS/wF7zt26f0vm', 'adm.ccalidad@ipo.com.mx', 'Monica Romero', '2020-05-21 12:24:18', 'ANHE1', NULL, NULL, NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (16, 6, 0, 1, 'JOHE1', NULL, '$2a$10$1WsaQEPTXqEOtxVq2d17Iuhv5VuRwLaCnhzURYRaS/wF7zt26f0vm', 'ventas.luhe@ipo.com.mx', 'José Luis Hernández R.', '2021-04-08 14:32:50', 'ANHE1', NULL, NULL, NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (17, 5, 0, 1, 'ANMU1', NULL, '$2y$10$35Ly7G9BecomNhB91hs5Gu/9MXc/O9h6qw1mkc1migQ7qUvGkNuMu', 'regalos.ventas@ipo.com.mx', 'Antonia Muñiz', '2021-10-08 13:15:29', 'ANHE1', NULL, NULL, NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (18, 4, 0, 1, 'ARPE1', NULL, '$2a$10$1WsaQEPTXqEOtxVq2d17Iuhv5VuRwLaCnhzURYRaS/wF7zt26f0vm', 'prod.logistica@ipo.com.mx', 'Armando Perea', '2021-12-30 12:48:00', 'ANHE1', NULL, NULL, NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (19, 7, 0, 1, 'MAGO1', NULL, '$2a$10$1WsaQEPTXqEOtxVq2d17Iuhv5VuRwLaCnhzURYRaS/wF7zt26f0vm', '', 'María Isabel Gómez Júarez', '2023-06-27 16:47:33', 'ANHE1', NULL, NULL, NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (20, 7, 0, 1, 'MARTIN', NULL, '$2a$10$1WsaQEPTXqEOtxVq2d17Iuhv5VuRwLaCnhzURYRaS/wF7zt26f0vm', '', 'Martin Garcia Ruiz', '2023-06-27 16:53:59', 'ANHE1', NULL, NULL, NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (21, 8, 0, 1, 'FETO1', NULL, '$2a$10$1WsaQEPTXqEOtxVq2d17Iuhv5VuRwLaCnhzURYRaS/wF7zt26f0vm', 'adm.acalidad@ipo.com.mx', 'Fermín Torres Ríos', '2023-10-03 17:06:12', 'ANHE1', NULL, NULL, NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (22, 0, 0, 1, 'SEJI1', NULL, '$2y$10$BwIDBQFIWhBuMqeTYvQGxugql1/PcNhePTonRxYoOoH7kUq3ZS.KK', ' ', 'Sergio Jiménez', '2025-08-01 05:31:05', 'EDITA', NULL, NULL, NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (23, 0, 0, 1, 'FAJI1', NULL, '$2y$10$BwIDBQFIWhBuMqeTYvQGxugql1/PcNhePTonRxYoOoH7kUq3ZS.KK', ' ', 'Fabian Jiménez', '2025-08-27 08:43:37', 'EDITA', NULL, NULL, NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (24, 10, 0, 1, 'ALMA1', NULL, '$2a$10$1WsaQEPTXqEOtxVq2d17Iuhv5VuRwLaCnhzURYRaS/wF7zt26f0vm', '', 'Alberto Maturano', '2025-10-21 14:21:41', 'ANHE1', NULL, NULL, NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (25, 1, 0, 1, 'EDME1', NULL, '$2y$10$rJIZADB4Fb6o4CRxpdKZXeV6qBFokrJUBh9bwtLp9evAoqV8XRybe', 'sistemas.emendoza@ipo.com.mx', 'Eduardo Mendoza', '2026-01-29 12:06:22', 'EDITA', '2026-01-29 14:07:17', 'EDITA', '');
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (26, 4, 0, 1, 'JUCA1', NULL, '$2y$10$OVjw0y0oxXtSXXumBGr0HuG93uxFAmSO4eEVXwg75yOisqqlp5mAy', 'design.dir@ipo.com.mx', 'JOSE JUAN CARMONA GARIBAY', '2026-02-24 21:01:05', 'ANHE1', NULL, NULL, NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (27, 0, 1323, 1, 'ALBA1', NULL, '$2y$10$35Ly7G9BecomNhB91hs5Gu/9MXc/O9h6qw1mkc1migQ7qUvGkNuMu', 'adm.sistemas@ipo.com.mx', 'Alfonso Basan García', '2026-06-09 13:22:38', NULL, NULL, NULL, NULL);
INSERT INTO `_user` (`_user_id`, `_user_idroll`, `_user_iddir`, `_user_a`, `_user_clv`, `_user_apikey`, `_user_psw`, `_user_mail`, `_user_nom`, `_user_fn`, `_user_usr_n`, `_user_fm`, `_user_usr_m`, `_user_tkn`) VALUES (28, 0, 0, 1, 'SAAC1', NULL, '$2y$10$35Ly7G9BecomNhB91hs5Gu/9MXc/O9h6qw1mkc1migQ7qUvGkNuMu', 'mantenimiento@ipo.com.mx', 'Salomé Acevedo', '2026-07-01 12:12:18', 'ANHE1', NULL, NULL, NULL);
