using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Stock_Manager.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo client = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnPost()
        {
            client.nom = Request.Form["nom"];
            client.email = Request.Form["email"];
            client.telephone = Request.Form["telephone"];
            client.adresse = Request.Form["adresse"];

            try
            {
                string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=stock_manager;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "INSERT INTO Client (nom, Email, Adresse, Telephone) VALUES (@nom, @email, @adresse, @telephone)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@nom", client.nom);
                        cmd.Parameters.AddWithValue("@email", client.email);
                        cmd.Parameters.AddWithValue("@telephone", client.telephone);
                        cmd.Parameters.AddWithValue("@adresse", client.adresse);
                        cmd.ExecuteNonQuery();
                    }
                }

                successMessage = "Client ajouté avec succès.";
                Response.Redirect("/Clients");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public class ClientInfo
        {
            public string nom, email, telephone, adresse;
        }
    }
}
