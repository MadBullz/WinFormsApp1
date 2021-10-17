using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace WinFormsApp1
{
    class Employee
    {
        public int empID { get; set; }
        public string empName { get; set; }
        public bool gender { get; set; }
        public int salary { get; set; }
        public string phone { get; set; }
    }

    class EmployeeDAO
    {
        // đối tượng kết nối
        SqlConnection connection;

        // đối tượng thực thi các truy vấn
        SqlCommand command;

        string getConnectionString()
        {
            // khai báo và lấy chuỗi từ appsettings.json
            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();

            return config["ConnectionString:MyEmployeeDB"];
        }

        public List<Employee> getEmployee()
        {
            List<Employee> emp = new List<Employee>();
            connection = new SqlConnection(getConnectionString());
            string query = "select * from Employee";

            command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        emp.Add(new Employee
                        {
                            empID = reader.GetInt32("empID"),
                            empName = reader.GetString("empName"),
                            gender = reader.GetBoolean("gender"),
                            salary = reader.GetInt32("salary"),
                            phone = reader.GetString("phone")
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return emp;
        }



        public int InsertEmployee(Employee employee)
        {
            int result = 0;

            connection = new SqlConnection(getConnectionString());
            string query = "insert into Employee values (@empName, @Gender, @Salary, @Phone) ";
            command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@empName",employee.empName);
            command.Parameters.AddWithValue("@Gender", employee.gender==true? 1 : 0);
            command.Parameters.AddWithValue("@Salary", employee.salary);
            command.Parameters.AddWithValue("@Phone", employee.phone);

            try
            {
                connection.Open();
                result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public int UpdateEmployee(Employee emp)
        {
            int result = 0;

            connection = new SqlConnection(getConnectionString());
            string query = "UPDATE Employee SET empName = @Name, gender = @Gender, salary = @Salary, phone = @Phone WHERE empID = @ID";
            command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Name", emp.empName);
            command.Parameters.AddWithValue("@Gender", emp.gender == true ? 1 : 0);
            command.Parameters.AddWithValue("@Salary", emp.salary);
            command.Parameters.AddWithValue("@Phone", emp.phone);
            command.Parameters.AddWithValue("@ID", emp.empID);

            try
            {
                connection.Open();
                result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public int DeleteEmployee(int empID)
        {
            int result = 0;

            connection = new SqlConnection(getConnectionString());
            string query = "DELETE FROM Employee  WHERE empID = @ID";
            command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", empID);

            try
            {
                connection.Open();
                result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return result;
        }
    }
}
