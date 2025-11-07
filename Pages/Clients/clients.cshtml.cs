using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Stock_Manager.Pages.Clients
{
    public class ClientsModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=localhost;Initial Catalog=stock_manager;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Client";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo client = new ClientInfo();
                                client.id = reader.GetInt32(0);
                                client.nom = reader.GetString(1);
                                client.email = reader.GetString(2);
                                client.telephone = reader.GetString(3);
                                client.adresse = reader.GetString(4);
                                listClients.Add(client);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }
    }

    public class ClientInfo
    {
        public int id;
        public string nom;
        public string email;
        public string telephone;
        public string adresse;
    }
}
