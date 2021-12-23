using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpese
{
    public class ADONetDisconnected
    {
        static string connectionStringSQL = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GestioneSpese;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private static SqlDataAdapter InitializeAdapter(SqlConnection conn)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = new SqlCommand("SELECT * FROM Spesa JOIN Categoria ON CategoriaId = Categoria.Id", conn);
            adapter.DeleteCommand = DeleteCommand(conn);

            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            return adapter;
        }

        private static SqlCommand DeleteCommand(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM Spesa WHERE Id=@id";
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 0, "Id"));
            return cmd;
        }

        public static void PrintApproved()
        {
            DataSet spesaDS = new DataSet();
            using SqlConnection conn = new SqlConnection(connectionStringSQL);
            try
            {
                conn.Open();

                var spesaAdapter = InitializeAdapter(conn);
                spesaAdapter.Fill(spesaDS, "Spesa");

                conn.Close();

                EnumerableRowCollection<DataRow> filteredTable = from spesa in spesaDS.Tables["Spesa"].AsEnumerable()
                                                                 where spesa.Field<bool>("Approvato") == true
                                                                 select spesa;

                foreach (DataRow r in filteredTable)
                {
                    Console.WriteLine("[" + r["Id"] + "] Descrizione: "
                       + r["Descrizione"] + " Categoria: " + r["Categoria"] + " Utente: " + r["Utente"] + " Importo: " + r["Importo"] + " Approvato: " + r["approvato"]);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Errore SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore generico: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }


        public static void PrintAllSpese()
        {
            DataSet spesaDS = new DataSet();
            using SqlConnection conn = new SqlConnection(connectionStringSQL);
            try
            {
                conn.Open();

                var spesaAdapter = InitializeAdapter(conn);
                spesaAdapter.Fill(spesaDS, "Spesa");

                conn.Close();

                foreach (DataRow r in spesaDS.Tables["Spesa"].Rows)
                {
                    Console.WriteLine("[" + r["Id"] + "] Descrizione: "
                       + r["Descrizione"] + " Categoria: " + r["Categoria"] + " Utente: " + r["Utente"] + " Importo: " + r["Importo"] + " Approvato: " + r["approvato"]);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Errore SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore generico: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }


        public static void DeleteById()
        {
            DataSet spesaDS = new DataSet();
            using SqlConnection conn = new SqlConnection(connectionStringSQL);
            int id = 7;
            try
            {
                conn.Open();

                var spesaAdapter = InitializeAdapter(conn);
                spesaAdapter.Fill(spesaDS, "Spesa");

                conn.Close();

                Console.WriteLine("Lista di tutte le spese");
                PrintAllSpese();

                Console.WriteLine("Inserisci l'id della Spesa da eliminare");
                int.TryParse(Console.ReadLine(), out id);


                EnumerableRowCollection<DataRow> r = from spesa in spesaDS.Tables["Spesa"].AsEnumerable()
                            where spesa.Field<int>("Id") == id
                            select spesa;

                if (r.FirstOrDefault() != null)
                {
                    r.First().Delete();
                    spesaAdapter.Update(spesaDS, "Spesa");
                    Console.WriteLine("Database Aggiornato");
                }
                else
                    Console.WriteLine("Spesa non eliminata");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Errore SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore generico: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}