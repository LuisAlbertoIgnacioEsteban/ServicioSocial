using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;

namespace Datos
{
    public class MetodosDatos
    {
        public MySqlDataAdapter mda = null;
        public MySqlCommand comando = null;
        public DataSet ds = null;

        /// <summary>
        /// Obtiene todos los alumnos, con sus numeros de entregas, 
        /// el nombre imagen de perfil y el numero de netregas
        /// <return>
        /// Devuelve un DataSet con los campos: matricula, imgPerfil y 
        /// cantidad de tareas entregadas por cada alumno
        /// </return>
        public DataSet tareasEntregasDeAlumnos() {
            List<tareasAlumnos> listAlumnos = new List<tareasAlumnos>();
            ds = null;
            try
            {
                ds = new DataSet();
                string str = "select distinct(a.matricula), a.imgPerfil, (select count(*) " +
                    "from detalleTarea dt where dt.estatus like 1 and dt.matricula like a.matricula)" +
                    " as 'NoEntregas' from tarea t join alumno a ; ";
                comando = new MySqlCommand(str, Conexion.ObtenerConexion());
                mda = new MySqlDataAdapter(comando);
                mda.Fill(ds, "entregas");
            }
            catch (Exception)
            {

                return ds = null;
            }
            finally {
                Conexion.ObtenerConexion().Close();
                Conexion.ObtenerConexion().Dispose();
            }
            return ds;
        }

        /// <summary>
        /// Obtiene la cantidad de tareas agregadas en la BD
        /// <return>
        /// Devuelve un DataSet con la cantidad de tareas agregadas
        /// </return>
        /***
         * Obtiene la cantidad de tareas de tareas 
         * @return DataSet
         * **/
        public DataSet noTareas()
        {
            List<tareasAlumnos> listAlumnos = new List<tareasAlumnos>();
            ds = null;
            try
            {
                ds = new DataSet();
                string str = "select count(*) from tarea;";
                comando = new MySqlCommand(str, Conexion.ObtenerConexion());
                mda = new MySqlDataAdapter(comando);
                mda.Fill(ds, "noTareas");
            }
            catch (Exception)
            {

                return ds = null;
            }
            finally
            {
                Conexion.ObtenerConexion().Close();
                Conexion.ObtenerConexion().Dispose();
            }
            return ds;
        }

        /// <summary>
        /// Agrega alumnos a la BD, utilizando archivos.csv
        /// <return>
        /// Devuelve un entero para indicar si fue exitoso la inserccion
        /// </return>
        /// <param name="dir">
        /// Ruta del archivo.csv
        /// </param>
        public int agregarAlumnos(string dir)
        {
            int resp = 0;
            try
            {
                string str = "LOAD DATA INFILE '" +dir+"' " +
                            " INTO TABLE db.alumno " +
                            " character set latin1 " +
                            " fields terminated by ';' " +
                            " lines terminated by '\n' " +
                            " IGNORE 0 LINES " +
                            " (matricula, nombres, apellidos, imgPerfil);";
                comando = new MySqlCommand(str, Conexion.ObtenerConexion());
                resp = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                Conexion.ObtenerConexion().Close();
                Conexion.ObtenerConexion().Dispose();
            }
            return resp;
        }


        /// <summary>
        /// Agrega tareas a la BD, utilizando archivos.csv
        /// <return>
        /// Devuelve un entero para indicar si fue exitoso la inserccion
        /// </return>
        /// <param name="dir">
        /// Ruta del archivo.csv
        /// </param>
        public int agregarTareas(string dir)
        {
            int resp = 0;
            try
            {
                string str = "LOAD DATA INFILE '" + dir + "' " +
                            " INTO TABLE db.tarea " +
                            " fields terminated by ';' " +
                            " lines terminated by '\n' " +
                            " IGNORE 1 LINES " +
                            " (nombre,idMateria);";
                comando = new MySqlCommand(str, Conexion.ObtenerConexion());
                resp = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                Conexion.ObtenerConexion().Close();
                Conexion.ObtenerConexion().Dispose();
            }
            return resp;
        }


        /// <summary>
        /// Agrega materias a la BD, utilizando archivos.csv
        /// <return>
        /// Devuelve un entero para indicar si fue exitoso la inserccion
        /// </return>
        /// <param name="dir">
        /// Ruta del archivo.csv
        /// </param>
        public int agregarMaterias(string dir)
        {
            int resp = 0;
            try
            {
                string str = "LOAD DATA INFILE '" + dir + "' " +
                            " INTO TABLE db.materia " +
                            " fields terminated by ';' " +
                            " lines terminated by '\n' " +
                            " (nombre);";
                comando = new MySqlCommand(str, Conexion.ObtenerConexion());
                resp = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                Conexion.ObtenerConexion().Close();
                Conexion.ObtenerConexion().Dispose();
            }
            return resp;
        }

        /// <summary>
        /// Agrega detalles de materias a la BD, utilizando archivos.csv
        /// <return>
        /// Devuelve un entero para indicar si fue exitoso la inserccion
        /// </return>
        /// <param name="dir">
        /// Ruta del archivo.csv
        /// </param>
        public int agregarDetalleMaterias(string dir)
        {
            int resp = 0;
            try
            {
                string str = "LOAD DATA INFILE '" + dir + "' " +
                            " INTO TABLE db.detalletarea " +
                            " fields terminated by ',' " +
                            " lines terminated by '\n' " +
                            " IGNORE 1 LINES "+
                            " (fecha,matricula,estatus,idTarea,calificacion);";
                comando = new MySqlCommand(str, Conexion.ObtenerConexion());
                resp = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                Conexion.ObtenerConexion().Close();
                Conexion.ObtenerConexion().Dispose();
            }
            return resp;
        }

    }
}


/*
 Clase conexion
 */
class Conexion
    {

    /// <summary>
    /// Obtiene una conexion con la BD
    /// <return>
    /// Devuelve la conexion a la BD
    /// </return>
    public static MySqlConnection ObtenerConexion()
        {
            MySqlConnection conectar = new MySqlConnection("server=localhost; database=DB; user=root; pwd='Lafina$123'");
            conectar.Open();
                return conectar;
        }
    }


