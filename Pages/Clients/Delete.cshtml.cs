using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Stock_Manager.Pages.Clients
{
    public class DeleteModel : PageModel
    {
        public void OnGet()
        {
            string id = Request.Query["id"];
            try
            {
                string connectionString = "Data Source=localhost;Initial Catalog=stock_manager;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "DELETE FROM Client WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }

            Response.Redirect("/Clients/Clients");
        }
    }
}
