using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpese
{
    public static class ADONetConnected
    {
        static string connectionStringSQL = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GestioneSpese;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //Inserire nuova Spesa
        public static void InsertSpesa()
        {
            using SqlConnection conn = new SqlConnection(connectionStringSQL);

            try
            {
                conn.Open();

                DateTime data = DateTime.Now;
                string descrizione, utente;
                int categoria;
                decimal importo;

                Console.WriteLine("Scegli una categoria tra:" +
                    "\n[ 1 ] Elettronica" +
                    "\n[ 2 ] Carne" +
                    "\n[ 3 ] Frutta e Verdura" +
                    "\n[ 4 ] Casa");
                do
                {
                    int.TryParse(Console.ReadLine(), out categoria);
                    if (categoria != 1 && categoria != 2 && categoria != 3 && categoria != 4)
                        Console.WriteLine("Scegli una categoria valida");

                } while (categoria != 1 && categoria != 2 && categoria != 3 && categoria != 4);

                Console.WriteLine("Inserisci la descrizione della spesa");
                descrizione = Console.ReadLine();

                Console.WriteLine("Inserisci l'utente");
                utente = Console.ReadLine();

                Console.WriteLine("Inserisci l'importo della spesa");
                decimal.TryParse(Console.ReadLine(), out importo);

                string insertSql = "INSERT INTO SPESA VALUES(@data, @categoria, @descrizione, @utente, @importo,0) ";
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = insertSql;

                cmd.Parameters.AddWithValue("@data", data);
                cmd.Parameters.AddWithValue("@categoria", categoria);
                cmd.Parameters.AddWithValue("@descrizione", descrizione);
                cmd.Parameters.AddWithValue("@utente", utente);
                cmd.Parameters.AddWithValue("@importo", importo);

                int righe = cmd.ExecuteNonQuery();

                if (righe > 0)
                    Console.WriteLine("Spesa inserita correttamente");
                else
                    Console.WriteLine("Spesa non inserita");
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

        //Approvare una Spesa esistente (impostare il campo Approvato)
        public static void Approve()
        {
            using SqlConnection conn = new SqlConnection(connectionStringSQL);
            int id;
            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "UPDATE Spesa SET Approvato = 1 WHERE Id = @id";

                Console.WriteLine("Lista delle spese non approvate");
                PrintNotApproved();
                Console.WriteLine("Inserisci l'id della spesa che vuoi approvare");
                int.TryParse(Console.ReadLine(), out id);
                cmd.Parameters.AddWithValue("@id", id);

                int righe = cmd.ExecuteNonQuery();

                if (righe > 0)
                    Console.WriteLine("Spesa approvata correttamente");
                else
                    Console.WriteLine("Spesa non approvata o non esistente");
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

        public static void PrintNotApproved()
        {
            using SqlConnection conn = new SqlConnection(connectionStringSQL);

            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM Spesa JOIN Categoria ON CategoriaId = Categoria.Id WHERE Approvato = 0";
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    Console.WriteLine("[" + reader["Id"] + "] Descrizione: "
                        + reader["Descrizione"] + " Categoria: " + reader["Categoria"] +  " Utente: " + reader["Utente"] + " Importo: " + reader["Importo"] + " Approvato: " + reader["approvato"]);
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

        //Mostrare l'elenco delle Spese di uno specifico Utente
        public static void PrintByUser()
        {
            using SqlConnection conn = new SqlConnection(connectionStringSQL);
            string utente;
            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM Spesa JOIN Categoria ON CategoriaId = Categoria.Id WHERE Utente = @utente";

                PrintUtenti();
                Console.WriteLine("Inserisci l'utente di cui vuoi mostrare le spese");
                utente = Console.ReadLine();
                cmd.Parameters.AddWithValue("@utente", utente);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine("[" + reader["Id"] + "] Descrizione: "
                        + reader["Descrizione"] + " Categoria: " + reader["Categoria"] + " Utente: " + reader["Utente"] + " Importo: " + reader["Importo"] + " Approvato: " + reader["approvato"]);
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

        public static void PrintUtenti()
        {
            using SqlConnection conn = new SqlConnection(connectionStringSQL);
            string utente;
            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT DISTINCT Utente FROM Spesa";
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("Questi sono gli utenti che hanno acquistato da noi\n");
                while (reader.Read())
                {
                    Console.WriteLine(reader["Utente"]);
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

        //Mostrare il totale delle Spese per Categoria
        public static void PrintTotByCategory()
        {
            using SqlConnection conn = new SqlConnection(connectionStringSQL);
            string utente;
            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT Categoria, SUM(Importo) AS Totale FROM Spesa JOIN Categoria ON Spesa.CategoriaId = Categoria.Id GROUP BY Categoria";
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("Questi sono gli utenti che hanno acquistato da noi\n");
                while (reader.Read())
                {
                    Console.WriteLine(reader["Categoria"] + ": " +reader["Totale"]);
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
    }
} 
