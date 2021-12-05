using PaddleSport.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace PaddleSport.Controllers
{
    public class PaymentController : Controller
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
        // GET: Payment
        public ActionResult PaymentDetails()
        {
            PaymentInfo p = new PaymentInfo();
            var ctx = Request.GetOwinContext();
            ClaimsPrincipal user = ctx.Authentication.User;
            IEnumerable<Claim> claims = user.Claims;
            p.Email = user.Identity.Name;

            p.total = float.Parse(Session["total"].ToString());
            return View(p);
        }
        [HttpPost]
        public ActionResult PaymentDetails(PaymentInfo paymentInfo)
        {
            paymentInfo.total = float.Parse(Session["total"].ToString());
            SqlCommand cmd = new SqlCommand("insert into paymentinfoes(Name,Email,Address,City,State,Zip, total,TransactionId) values ('" + paymentInfo.Name + "','" + paymentInfo.Email + "','" + paymentInfo.Address + "','" + paymentInfo.City + "','" + paymentInfo.State + "','" + paymentInfo.Zip + "','" + paymentInfo.total + "','" + paymentInfo.TransactionId + "')", con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToAction("Index","Home");
            }
            catch (Exception ex)
            {
                return View();
            }



        }
        
    }
}