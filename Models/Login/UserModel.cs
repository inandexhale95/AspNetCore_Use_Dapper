using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Myweb.Second.Models.Login
{
    public class UserModel
    {
        public int User_seq { get; set; }
        public string User_name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // 암호화
        public void ConvertPassword()
        {
            var sha = new HMACSHA512();
            sha.Key = Encoding.UTF8.GetBytes(this.Password.Length.ToString());

            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(this.Password));

            this.Password = Convert.ToBase64String(hash);
        }

        public int Register(IConfiguration _configuration)
        {
            string sql = @"
INSERT INTO t_user (
    user_name,
    email,
    password
)
SELECT 
    @user_name,
    @email,
    @password
";
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conn.Open();
                // this는 객체 자신이 만들어져서 사용할 수 있다. 
                return Dapper.SqlMapper.Execute(conn, sql, this);
            }
        }


    }
}
