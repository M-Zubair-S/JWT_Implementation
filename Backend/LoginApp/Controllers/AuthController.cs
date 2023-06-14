using LoginApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace LoginApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [Authorize]
        [HttpGet]
        [Route("getdata")]
        public string GetData()
        {
            return "Authenticated with jwt";
        }

        [HttpGet]
        [Route("details")]
        public string Details()
        {
            return "Authenticated without jwt";
        }

        [Authorize]
        [HttpGet]
        [Route("getproduct")]
        public IActionResult GetProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("conn").ToString()))
            {
                string query = "SELECT Id, ProductName, Description FROM Product";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                Id = (int)reader["Id"],
                                Name = reader["ProductName"].ToString(),
                                Description = reader["Description"].ToString()

                            };

                            products.Add(product);
                        }
                    }
                }
            }
            string json = JsonConvert.SerializeObject(products);

            return Content(json, "application/json");
        }
    }
}