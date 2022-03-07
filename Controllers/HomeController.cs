using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Myweb.Second.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Myweb.Second.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger,
                              IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TicketList()
        {
            string status = "In Progress";

            var ticketList = TicketModel.GetList(status, _configuration);

            return View(ticketList);
            /*var dt = new DataTable();
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    string status = "In Progress";
                    cmd.Connection = conn;
                    cmd.CommandText = @"
SELECT A.ticket_id, A.title, A.status
FROM t_ticket A
WHERE A.status = @status";

                    cmd.Parameters.AddWithValue("status", status);
                    // Select 할 때
                    var reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    // Create, Update, Delete 할 때
                    //cmd.ExecuteNonQuery();
                }
                ViewData["dt"] = dt;
            }
            return View();*/
        }

        // [FormForm] : Form에서 받을 데이터만 받을 수 있게 해주는 애트리뷰트
        public IActionResult TicketChange([FromForm] TicketModel model)/*int ticket_id, string title*/
        {
            model.Update(_configuration);

            return Redirect("TicketList");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
