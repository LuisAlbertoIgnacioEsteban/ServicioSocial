-- Referencia https://phenobarbital.wordpress.com/2007/08/03/cargar-datos-en-mysql-con-load-data-infile/
-- Referencia https://blog.unixpad.com/2017/03/08/error-al-carga-archivo-de-datos-en-tabla-de-mysql-the-mysql-server-is-running-with-the-secure-file-priv-option/

use db;

-- SET GLOBAL local_infile=1;-- Habilitar el local_infile
-- SHOW VARIABLES LIKE "secure_file_priv";-- Mostrar la ruta de infile
show variables;
SET GLOBAL local_infile=0;

-- Cargar datos de alumnos
LOAD DATA INFILE 'C:\\ProgramData\\MySQL\\MySQL Server 8.0\\Uploads\\Datos\\alumnos.csv'
INTO TABLE db.alumno 
character set latin1
fields terminated by ';' 
lines terminated by '\n' 
IGNORE 0 LINES
(matricula, nombres,apellidos,imgPerfil);

-- Cargar datos de materias
LOAD DATA INFILE 'C:\\ProgramData\\MySQL\\MySQL Server 8.0\\Uploads\\Datos\\Materias.csv'
INTO TABLE db.materia
FIELDS TERMINATED BY ','
LINES TERMINATED BY '\n' 
(nombre);

-- Cargar datos de Tareas
LOAD DATA INFILE 'Arhicos/Tareas.csv'
INTO TABLE db.tarea
FIELDS TERMINATED BY ';'
LINES TERMINATED BY '\n' 
IGNORE 1 LINES
(nombre,idMateria);

-- Cargar datos detalleTareas
LOAD DATA INFILE 'C:\\ProgramData\\MySQL\\MySQL Server 8.0\\Uploads\\Datos\\detalleTareas.csv'
INTO TABLE db.detalletarea
FIELDS TERMINATED BY ','
LINES TERMINATED BY '\n' 
IGNORE 1 LINES
(fecha,matricula,estatus,idTarea,calificacion)
-- SET fecha = str_to_date(STR_TO_DATE(@fecha,'%d/%m/%Y'),'%Y-%m-%d');
;

select * from alumno;
select * from materia;
select * from tarea;
select * from detalleTarea;


