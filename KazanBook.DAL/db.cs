using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace KazanBook.DAL
{
    public class db
    {
        static string connectionString = "Data Source=localhost;Initial Catalog=kazanbook;Integrated Security=SSPI;";
        static SqlConnection connection = new SqlConnection(connectionString);
        public static async Task Initialize()
        {
            try
            {
                await connection.OpenAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("Couldn't connect to db");
                throw e;
            }
            Console.WriteLine("Connected");
            string authorsCounter = await SqlQueryRead("SELECT COUNT(*) FROM Authors");
            string booksCounter = await SqlQueryRead("SELECT COUNT(*) FROM Books");
            Console.WriteLine($"Authors: {authorsCounter}");
            Console.WriteLine($"Books: {booksCounter}");
        }
        public static async Task<string> SqlQueryRead(string query)
        {
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            //SqlDataReader reader = sqlCommand.ExecuteReader();
            //object result = reader.GetValue(0);
            object result = await sqlCommand.ExecuteScalarAsync();
            //if (result is string == false) throw new Exception("SqlQueryRead error");
            //return (string)reader.GetValue(0);
            return result.ToString();
        }
        public static async Task<SqlDataReader> SqlQueryReader(string query)
        {
            SqlCommand command = new SqlCommand(query, connection);
            return await command.ExecuteReaderAsync();
        }
    }
}
