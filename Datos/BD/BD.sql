drop database DB;
create database DB;
use DB;

create table alumno(
matricula char(9) primary key,
nombres varchar(50) not null,
apellidos varchar(60) not null,
imgPerfil longtext not null
)engine=InnoDB;

create table materia(
idMateria int auto_increment primary key,
nombre varchar(30) not null
);

create table tarea(
idTarea int auto_increment primary key,
nombre varchar(50) not null,
idMateria int not null,
foreign key(idMateria) references materia(idMateria)
)engine=InnoDB;

create table detalleTarea(
idDetalle int auto_increment primary key,
fecha date not null,
estatus bool not null,
matricula varchar(9) not null,
calificacion tinyint not null,
idTarea int not null,
foreign key(matricula) references alumno(matricula),
foreign key(idTarea) references tarea(idTarea)
)engine=InnoDB;

/*
select distinct(a.matricula), a.imgPerfil,
(select count(*) from detalleTarea dt where  dt.estatus like 1 and dt.matricula like a.matricula) as 'No. Entregas'
from tarea t join alumno a ;

select distinct(a.idAlumno), a.imgPerfil,
(select count(*) from detalleTarea dt where  dt.estatus like 1 and dt.idAlumno like a.idAlumno) as 'No. Entregas'
from tarea t join alumno a ;

select distinct(a.idAlumno),
(select count(*) from detalleTarea dt where  dt.estatus like 1 and dt.idAlumno like a.idAlumno) as 'NoEntregas'
from tarea t join alumno a ;
*/

