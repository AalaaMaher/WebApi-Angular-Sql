using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using DemoFullApi1.Models;

namespace DemoFullApi1.Controllers
{
    public class EmployeeController :ApiController
    {
        // GET: Employee
        public HttpResponseMessage Get()
        {
            string query = @"
                    select EmployeeID,EmployeeName ,Department, HirringDate, PersonalImageName from
                    dbo.Employee
                    ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["DemoDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);


        }
        public string Post(Employee emp)
        {
            try
            {
                string query = @"
                    insert into dbo.Employee values
                    ('" + emp.EmployeeName + @"'
                    ,'" + emp.Department + @"'
                    ,'" + emp.HirringDate + @"'
                    ,'" +emp.PersonalImageName+ @"'
                    )
                    ";

                DataTable table = new DataTable();
                using (SqlConnection con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["DemoDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Added Successfully!!";
            }
            catch (Exception)
            {

                return "Failed to Add!!";
            }
        }
        public string Put(Employee emp)
        {
            try
            {
                string query = @"
                    update dbo.Employee set 
                    EmployeeName='" + emp.EmployeeName + @"'
                    ,Department='" + emp.Department + @"'
                    ,HirringDate='" + emp.HirringDate + @"'
                    ,PersonalImageName='" + emp.PersonalImageName + @"'
                    where EmployeeID=" + emp.EmployeeID + @"
                    ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["DemoDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Updated Successfully!!";
            }
            catch (Exception)
            {

                return "Failed to Update!!";
            }
        }


        public string Delete(int id)
        {
            try
            {
                string query = @"
                    delete from dbo.Employee 
                    where EmployeeID=" + id + @"
                    ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["DemoDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Deleted Successfully!!";
            }
            catch (Exception)
            {

                return "Failed to Delete!!";
            }
        }
        [Route("api/Employee/GetAllDepartmentNames")]
        [HttpGet]
        public HttpResponseMessage GetAllDepartmentNames()
        {
            string query = @"
                    select DepartmentName from dbo.Department";

            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["DemoDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("api/Employee/savefie")]
       public string savefile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var PostedFile = httpRequest.Files[0];
                string FileName = PostedFile.FileName;
                var path = HttpContext.Current.Server.MapPath("~/Photos/" + FileName);
                PostedFile.SaveAs(path);
                return FileName;

            }
            catch
            {
                return "user.png";
            }
        }

    }
}
