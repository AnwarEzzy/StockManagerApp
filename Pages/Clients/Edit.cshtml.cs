using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Stock_Manager.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            string id = Request.Query["id"];
            try
            {
                string connectionString = "Data Source=localhost;Initial Catalog=stock_manager;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Client WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientInfo.id = reader.GetInt32(0);
                                clientInfo.nom = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.telephone = reader.GetString(3);
                                clientInfo.adresse = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            clientInfo.id = Convert.ToInt32(Request.Form["id"]);
            clientInfo.nom = Request.Form["nom"];
            clientInfo.email = Request.Form["email"];
            clientInfo.telephone = Request.Form["telephone"];
            clientInfo.adresse = Request.Form["adresse"];

            try
            {
                string connectionString = "Data Source=localhost;Initial Catalog=stock_manager;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Client SET nom=@nom, email=@email, telephone=@telephone, adresse=@adresse WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", clientInfo.id);
                        command.Parameters.AddWithValue("@nom", clientInfo.nom);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@telephone", clientInfo.telephone);
                        command.Parameters.AddWithValue("@adresse", clientInfo.adresse);
                        command.ExecuteNonQuery();
                    }
                }
                Response.Redirect("/Clients/Clients");
            }
            catch (Exception ex)
            {
                errorMessage = "Erreur : " + ex.Message;
            }
        }
    }
}
