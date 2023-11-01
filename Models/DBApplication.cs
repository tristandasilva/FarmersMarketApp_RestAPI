using Microsoft.Extensions.Configuration;
using Npgsql;
using REST_API.Models;
using System.Data;

namespace FarmersMarketApp_RestAPI.Models
{
    public class DBApplication
    {
        public Response GetAllProducts(NpgsqlConnection con)
        {
            string Query = "SELECT * FROM public.products";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            Response response = new Response();

            List<Product> products = new List<Product>();

            if (dt.Rows.Count > 0) // If table is not empty
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Product product = new Product();

                    product.prod_name = (string) dt.Rows[i]["prod_name"];
                    product.id = (int) dt.Rows[i]["id"];
                    product.prod_amt = (double) dt.Rows[i]["prod_amt"];
                    product.prod_price = (double) dt.Rows[i]["prod_price"];
                    product.prod_price = (double) dt.Rows[i]["prod_price"];
                    products.Add(product);
                }
            }

            if (products.Count > 0)
            {
                response.statusCode = 200;
                response.messageCode = "Data retrieved successfully";
                response.product = null;
                response.products = products;
            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "Data failed to retrieve, or table is empty";
                response.product = null;
                response.products = null;
            }
            return response;
        }

        public Response GetProductById(NpgsqlConnection con, int id)
        {
            Response response = new Response();

            string Query = "SELECT * FROM public.products WHERE id='" + id + "'";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0) // If table is not empty
            {
                Product product = new Product();

                product.prod_name = (string)dt.Rows[0]["prod_name"];
                product.id = (int)dt.Rows[0]["id"];
                product.prod_amt = (double)dt.Rows[0]["prod_amt"];
                product.prod_price = (double)dt.Rows[0]["prod_price"];
                product.prod_price = (double)dt.Rows[0]["prod_price"];

                response.statusCode = 200;
                response.messageCode = "Data retrieved successfully";
                response.product = product;
                response.products = null;
            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "Data not found, check the ID";
                response.product = null;
                response.products = null;
            }

            return response;
        }

        public Response AddProduct(NpgsqlConnection con, Product product)
        {
            con.Open();
            Response response = new Response();

            string Query = "INSERT INTO public.products VALUES(@prod_name, @id, @prod_amt, @prod_price)";

            NpgsqlCommand cmd = new NpgsqlCommand(Query, con);

            cmd.Parameters.AddWithValue("@prod_name", product.prod_name);
            cmd.Parameters.AddWithValue("@id", product.id);
            cmd.Parameters.AddWithValue("@prod_amt", product.prod_amt);
            cmd.Parameters.AddWithValue("@prod_price", product.prod_price);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                response.statusCode = 200;
                response.messageCode = "Successfully inserted";
                response.product = product;
                response.products = null;
            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "Insertion not successful";
                response.product = null;
                response.products = null;
            }
            con.Close();
            return response;
        }

        public Response UpdateProduct(NpgsqlConnection con, Product product)
        {
            con.Open();
            Response response = new Response();

            string Query = "UPDATE public.products " +
                           "SET prod_name=@name, prod_amt=@amt, prod_price=@price " +
                           "WHERE id=@id";

            NpgsqlCommand cmd = new NpgsqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@name", product.prod_name);
            cmd.Parameters.AddWithValue("@amt", product.prod_amt);
            cmd.Parameters.AddWithValue("@price", product.prod_price);
            cmd.Parameters.AddWithValue("@id", product.id);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                response.statusCode = 200;
                response.messageCode = "Successfully updated";
                response.product = product;
                response.products = null;
            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "Update not successful";
                response.product = null;
                response.products = null;
            }
            con.Close();
            return response;
        }

        public Response DeleteProductbyId(NpgsqlConnection con, int id)
        {
            con.Open();
            Response response = new Response();

            string Query = "DELETE FROM public.products WHERE id='" + id + "'";

            NpgsqlCommand cmd = new NpgsqlCommand(Query, con);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                response.statusCode = 200;
                response.messageCode = "Product deleted successfully";
            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "Product not found";
            }
            con.Close();
            return response;
        }
    }
}
