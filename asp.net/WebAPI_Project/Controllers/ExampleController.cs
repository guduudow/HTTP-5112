using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI_Project.Controllers
{
    public class ExampleController : ApiController
    {
        /// <summary>
        /// This method returns three strings when receiving a POST request.
        /// <example>POST api/example</example>
        /// </summary>
        /// <returns>["example1", "example2", "example3"]</returns>
        public IEnumerable<string> Post()
        {
            return new string[] { "example1", "example2", "example3" };
        }

        // GET api/example/{id}
        public IEnumerable<string> Get(int id)
        {
            return new string[] { "example4", "example5", "example6" };
        }
    }
}
