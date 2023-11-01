using FarmersMarketApp_RestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using REST_API.Models;

namespace FarmersMarketApp_RestAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetAllProducts")]

        public Response GetAllProducts()
        {
            Response response = new Response();

            NpgsqlConnection con = new NpgsqlConnection(_configuration.GetConnectionString("productConnection"));

            DBApplication dbA = new DBApplication();

            response = dbA.GetAllProducts(con);

            return response;
        }

        [HttpGet]
        [Route("GetProductById/{id}")]
        public Response GetProductById(int id)
        {
            Response response = new Response();

            NpgsqlConnection con = new NpgsqlConnection(_configuration.GetConnectionString("productConnection"));

            DBApplication dbA = new DBApplication();
            response = dbA.GetProductById(con, id);
            return response;
        }

        [HttpGet]
        [Route("GetProductByName/{name}")]
        public Response GetProductByName(string name)
        {
            Response response = new Response();

            NpgsqlConnection con = new NpgsqlConnection(_configuration.GetConnectionString("productConnection"));

            DBApplication dbA = new DBApplication();
            response = dbA.GetProductByName(con, name);
            return response;
        }

        [HttpPost]
        [Route("AddProduct")]

        public Response AddProduct(Product product)
        {
            Response response = new Response();

            NpgsqlConnection con = new NpgsqlConnection(_configuration.GetConnectionString("productConnection"));

            DBApplication dbA = new DBApplication();
            response = dbA.AddProduct(con, product);
            return response;
        }

        [HttpPut]
        [Route("UpdateProduct")]
        
        public Response PutProduct(Product product)
        {
            Response response = new Response();

            NpgsqlConnection con = new NpgsqlConnection(_configuration.GetConnectionString("productConnection"));

            DBApplication dbA = new DBApplication();
            response = dbA.UpdateProduct(con, product);
            return response;
        }

        [HttpPut]
        [Route("UpdateProductByName")]

        public Response PutProductByName(Product product)
        {
            Response response = new Response();

            NpgsqlConnection con = new NpgsqlConnection(_configuration.GetConnectionString("productConnection"));

            DBApplication dbA = new DBApplication();
            response = dbA.UpdateProductByName(con, product);
            return response;
        }

        [HttpDelete]
        [Route("DeleteProductById/{id}")]

        public Response DeleteProductById(int id)
        {
            Response response = new Response();
            NpgsqlConnection con = new NpgsqlConnection(_configuration.GetConnectionString("productConnection"));
            DBApplication dbA = new DBApplication();
            response = dbA.DeleteProductbyId(con, id);
            return response;
        }
    }
}
