using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Stock_Manager.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            clientInfo.nom = Request.Form["nom"];
            clientInfo.email = Request.Form["email"];
            clientInfo.telephone = Request.Form["telephone"];
            clientInfo.adresse = Request.Form["adresse"];

            if (clientInfo.nom.Length == 0 || clientInfo.email.Length == 0 ||
                clientInfo.telephone.Length == 0 || clientInfo.adresse.Length == 0)
            {
                errorMessage = "Tous les champs sont obligatoires.";
                return;
            }

            try
            {
                string connectionString = "Data Source=localhost;Initial Catalog=stock_manager;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Client (nom, email, telephone, adresse) VALUES (@nom, @email, @telephone, @adresse)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nom", clientInfo.nom);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@telephone", clientInfo.telephone);
                        command.Parameters.AddWithValue("@adresse", clientInfo.adresse);
                        command.ExecuteNonQuery();
                    }
                }
                successMessage = "Client ajouté avec succès !";
                Response.Redirect("/Clients/Clients");
            }
            catch (Exception ex)
            {
                errorMessage = "Erreur : " + ex.Message;
            }
        }
    }
}
