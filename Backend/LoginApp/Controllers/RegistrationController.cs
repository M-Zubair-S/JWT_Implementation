using Google.Authenticator;
using LoginApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LoginApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost]
        [Route("Registration")]
        public string registration(Registration registration)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("conn").ToString());
            SqlCommand cmd = new SqlCommand("INSERT INTO Registration (Username, Password) VALUES (@Username, @Password)", con);
            cmd.Parameters.AddWithValue("@Username", registration.Username);
            cmd.Parameters.AddWithValue("@Password", registration.Password);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i > 0)
            {
                return "Data Inserted";
            }
            else
            {
                return "Error";
            }
        }



        [HttpPost]
        public Registration login(Registration registration)
        {
            if (string.IsNullOrEmpty(registration.Username) || string.IsNullOrEmpty(registration.Password))
            {
                return null;
            }

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("conn").ToString());
            SqlCommand cmd = new SqlCommand("SELECT * FROM Registration WHERE Username = @Username AND Password = @Password", con);
            cmd.Parameters.AddWithValue("@Username", registration.Username);
            cmd.Parameters.AddWithValue("@Password", registration.Password);

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                return registration;
     
            }
            else
            {
                return null;
            }
        }

        private string GenerateToken(Registration registration)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials= new SigningCredentials(securitykey,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],null,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("signin")]
        public IActionResult SignIn(Registration registration)
        {
            IActionResult response = Unauthorized();
            var User_ = login(registration);

            if(User_ != null)
            {
                var token=GenerateToken(registration);
                response =Ok(new { token=token});
            }
            return response;
        }



    }
}
