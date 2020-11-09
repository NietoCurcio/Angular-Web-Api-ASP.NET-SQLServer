using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;

namespace WebApplication1.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
                        select EmployeeId, EmployeeName, DepartmentId,
                        convert(varchar(10), DateOfJoining, 102) as DateOfJoining,
                        PhotoFileName
                        from employee.Employee
                    ";
            // this is not correct, the would be better use stored procedures beucase of sql injection
            // with sp we can use place holders
            DataTable table = new DataTable();
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                // sql data adpter to fill the table(DataTable)
                //cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Employee emp)
        {
            try
            {
                string query = @"
                        insert into employee.Employee values
                        (
                            N'" + emp.EmployeeName + @"',
                            '" + emp.DepartmentId + @"',
                            '" + emp.DateOfJoining + @"',
                            N'" + emp.PhotoFileName + @"'
                        )                        
                    ";
                DataTable table = new DataTable();
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, connection))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Added Successfully";
            }
            catch (Exception)
            {
                return "Error, Failed to add";
            }
        }

        public string Put(Employee emp)
        {
            try
            {
                string query = @"
                        UPDATE employee.Employee SET
                        EmployeeName = '" + emp.EmployeeName + @"'
                        ,DepartmentId = '" + emp.DepartmentId + @"'
                        ,DateOfJoining = '" + emp.DateOfJoining + @"'
                        ,PhotoFileName = '" + emp.PhotoFileName + @"'
                        where EmployeeId = " + emp.EmployeeId + @"";
                DataTable table = new DataTable();
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, connection))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
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
                        delete from employee.Employee
                        where EmployeeId= " + id + @"";
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

        // custom method (is not Get, Post, Put, Delete)
        // we have the add its route
        [Route("api/Employee/GetAllDepartmentNames")]
        [HttpGet]
        public HttpResponseMessage GetAllDepartmentNames()
        {

            string query = @"
                        select DepartmentName from employee.Department";

            DataTable table = new DataTable();
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, connection))
            using (var da = new SqlDataAdapter(cmd))
            {
                //cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("api/Employee/SaveFile")]
        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + filename);

                postedFile.SaveAs(physicalPath);

                return filename;

            }catch(Exception)
            {
                return "anonymous.png";
            }
        }
    }
}
