using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoFullApi1.Models;

namespace DemoFullApi1.Controllers
{
    public class DepartmentController : ApiController
    {
        // GET: Department
        public HttpResponseMessage Get()
        {
            string query = @"
                    select DepartmentID,DepartmentName from
                    dbo.Department
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

            return Request.CreateResponse<DataTable>(HttpStatusCode.OK, table);


        }
        public string Post(Department dep)
        {
            try
            {
                string query = @"
                    insert into dbo.Department values
                    ('" + dep.DepartmentName + @"')
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
        public string Put(Department dep)
        {
            try
            {
                string query = @"
                    update dbo.Department set DepartmentName=
                    '" + dep.DepartmentName + @"'
                    where DepartmentID=" + dep.DepartmentID + @"
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
                    delete from dbo.Department 
                    where DepartmentID=" + id + @"
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

    }
}