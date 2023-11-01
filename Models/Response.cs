using FarmersMarketApp_RestAPI.Models;

namespace REST_API.Models
{
    public class Response
    {
        // This is the response message we want our database server should provide us as response

        // Response starts with a code
        public int statusCode { get; set; }

        // Response has a message
        public string messageCode { get; set; }

        // Response can only have one student retrieved from the DB
        public Product product { get; set; }

        // Response can have a list of students from the DB
        public List<Product> products { get; set; }
    }
}
