using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DepartmentController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
                        select * from employee.Department
                    ";
            // this is not correct, the would be better use stored procedures beucase of sql injection
            // with sp we can use place holders
            DataTable table = new DataTable();
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString))
                using(var cmd = new SqlCommand(query, connection))
                using(var da = new SqlDataAdapter(cmd))
            {
                // sql data adpter to fill the table(DataTable)
                //cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Department dep)
        {
            try
            {
                string query = @"
                        insert into employee.Department (DepartmentName) values
                        (N'"+dep.DepartmentName + @"')
                    ";
                DataTable table = new DataTable();
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, connection))
                using (var da = new SqlDataAdapter(cmd))
                {
                    //cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Added Successfully";
            }
            catch(Exception)
            {
                return "Error, Failed to add";
            }
        }

        public string Put(Department dep)
        {
            try
            {
                string query = @"
                        update employee.Department
                        set DepartmentName = '" + dep.DepartmentName + @"'
                        where DepartmentId= "+ dep.DepartmentId + @"";
                DataTable table = new DataTable();
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, connection))
                using (var da = new SqlDataAdapter(cmd))
                {
                    //cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Updated Successfully";
            }
            catch (Exception)
            {
                return "Error, Failed to update";
            }
        }

        public string Delete(int id)
        {
            try
            {
                string query = @"
                        delete from employee.Department
                        where DepartmentId= " + id + @"";
                DataTable table = new DataTable();
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, connection))
                using (var da = new SqlDataAdapter(cmd))
                {
                    //cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Deleted Successfully";
            }
            catch (Exception)
            {
                return "Error, Failed to delete";
            }
        }
    }
}
