using System.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;
using ConsoleApp3;

class Program
{
    // Cadena de conexión a la base de datos
    public static string connectionString = "Data Source=LAB1504-03\\SQLEXPRESS;Initial Catalog=Tecsup2023DB;User ID=userTecsup;Password=123456";

    static void Main()
    {

        
        SqlConnection miconexion = new SqlConnection(connectionString);
        miconexion.Open();

        SqlCommand micomando = new SqlCommand("SELECT * FROM Students", miconexion);
        SqlDataAdapter miAdapter = new SqlDataAdapter(micomando);

        DataTable miDatatable = new DataTable();
        miAdapter.Fill(miDatatable);

        miconexion.Close();

        foreach (DataRow row in miDatatable.Rows)
        {
            Console.WriteLine($"ID: {row["StudentID"]}, Nombre:{row["FirstName"]}, Cargo: {row["LastName"]}");
        }


    }
    private static DataTable ListarEstudianteDataTable()
    {
        // Crear un DataTable para almacenar los resultados
        DataTable dataTable = new DataTable();
        // Crear una conexión a la base de datos
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Abrir la conexión
            connection.Open();

            // Consulta SQL para seleccionar datos
            string query = "SELECT * FROM Estudiante";

            // Crear un adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);



            // Llenar el DataTable con los datos de la consulta
            adapter.Fill(dataTable);

            // Cerrar la conexión
            connection.Close();

        }
        return dataTable;
    }
    //De forma conectada
    private static List<Estudiante> ListarEstudianteListaObjetos()
    {
        List<Estudiante> estudiantes = new List<Estudiante>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Abrir la conexión
            connection.Open();

            // Consulta SQL para seleccionar datos
            string query = "SELECT StudentID,FirstName,LastName FROM Estudiante";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Verificar si hay filas
                    if (reader.HasRows)
                    {
                        Console.WriteLine("Lista de Empleados:");
                        while (reader.Read())
                        {
                            // Leer los datos de cada fila

                            estudiantes.Add(new Estudiante
                            {
                                StudentID = (int)reader["StudentID"],
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString()
                            });

                        }
                    }
                }
            }

            // Cerrar la conexión
            connection.Close();
        }
        return estudiantes;

    }

}