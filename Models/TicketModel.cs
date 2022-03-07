using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Myweb.Second.Models
{
    public class TicketModel
    {
        public int Ticket_Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }

        public static List<TicketModel> GetList(string status, IConfiguration configuration)
        {
            using (var conn = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                conn.Open();
                string sql = @"
SELECT A.ticket_id, A.title, A.status
FROM t_ticket A
WHERE A.status = @status";
                return Dapper.SqlMapper.Query<TicketModel>(conn, sql, new { status = status }).ToList();
            }
        }

        public int Update(IConfiguration _configuration)
        {
            string sql = @"
UPDATE t_ticket SET title = @title
WHERE ticket_id = @ticket_id";
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conn.Open();
                // this는 객체 자신이 만들어져서 사용할 수 있다. 
                return Dapper.SqlMapper.Execute(conn, sql, this);
            }
        }
    }
}
