using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSQL
{
    internal class Program
    {
        public static SqlConnection con;
        public static SqlCommand cmd;
        public static SqlDataReader reader;
        public static string constr = "server=SOUMYA; database=ProductInventoryDB; trusted_connection=true;";
        static void Main(string[] args)
        {
            string mm = "";
            do
            {
                Console.WriteLine("Choose from the below options\n1.View all Products\n2.Add new Product\n3.Update a Product\n4.Delete a Product");
                int op = int.Parse(Console.ReadLine());
                switch (op)
                {
                    case 1:
                        {
                            ViewProd();
                            break;
                        }
                    case 2:
                        {
                            AddProd();
                            break;
                        }
                    case 3:
                        {
                            UpdateProd();
                            break;
                        }
                    case 4:
                        {
                            DeleteProd();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid operation Choice");
                            return;
                        }
                }
                Console.WriteLine("\n\nPress M to go back to Main Menu");
                mm = Console.ReadLine().ToLower();
            } while (mm == "m");

        }

        static void ViewProd()
        {
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand
                {
                    Connection = con,
                    CommandText = "select * from Products"
                };
                con.Open();
                reader = cmd.ExecuteReader();
                Console.WriteLine("Product ID\tProduct Name\t\tPrice\tQuantity\tMfg Date\tExp Date");
                while (reader.Read())
                {
                    Console.Write(reader["ProductId"] + "\t\t");
                    Console.Write(reader["ProductName"] + "\t\t");
                    Console.Write(reader["Price"] + "\t");
                    Console.Write(reader["Quantity"] + "\t");
                    Console.Write(reader["MfDate"] + "\t");
                    Console.Write(reader["ExpDate"]);
                    Console.WriteLine("\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error!!!" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        static void AddProd()
        {
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand
                {
                    CommandText = "insert into Products (ProductId, ProductName, Price, Quantity, MfDate, ExpDate) values (@id,@name,@price,@qty,@mfDate,@expDate)",
                    Connection = con
                };
                Console.WriteLine("Enter Product Id");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Product Name");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Product Price");
                float price = float.Parse(Console.ReadLine());
                Console.WriteLine("Enter Product Quantity");
                int qty = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Product Mfg Date");
                DateTime mfDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter Product Exp Date");
                DateTime expDate = DateTime.Parse(Console.ReadLine());
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@mfDate", mfDate);
                cmd.Parameters.AddWithValue("@expDate", expDate);
                con.Open();
                int noe = cmd.ExecuteNonQuery();
                if (noe > 0)
                {
                    Console.WriteLine("Record Inserted!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        static void UpdateProd()
        {
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand
                {
                    CommandText = "update Products set ProductName=@name,Price=@price, Quantity=@qty, MfDate=@mfDate, ExpDate=@expDate where ProductId=@id",
                    Connection = con
                };
                Console.WriteLine("Enter Product Id to be updated");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Product Name");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Product Price");
                float price = float.Parse(Console.ReadLine());
                Console.WriteLine("Enter Product Quantity");
                int qty = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Product Mfg Date");
                DateTime mfDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter Product Exp Date");
                DateTime expDate = DateTime.Parse(Console.ReadLine());
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@mfDate", mfDate);
                cmd.Parameters.AddWithValue("@expDate", expDate);
                con.Open();
                int noe = cmd.ExecuteNonQuery();
                if (noe > 0)
                {
                    Console.WriteLine("Record Updated!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        static void DeleteProd()
        {
            try
            {
                con = new SqlConnection(constr);
                cmd = new SqlCommand
                {
                    CommandText = "delete from Products where ProductId=@id",
                    Connection = con
                };
                Console.WriteLine("Enter Product Id to be deleted");
                int id = int.Parse(Console.ReadLine());
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                int noe = cmd.ExecuteNonQuery();
                if (noe > 0)
                {
                    Console.WriteLine("Record Updated!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
