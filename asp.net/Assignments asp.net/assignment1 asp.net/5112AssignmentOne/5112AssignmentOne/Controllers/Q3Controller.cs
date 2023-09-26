using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _5112AssignmentOne.Controllers
{
    public class Q3Controller : ApiController
    {
        //<summary>Returns the requested string in the POST method. Must use command prompt curl -d "" "url" to do it
        //</summary>
        //<example>POST api/Q3/</example>
        //<returns>The requested resource does not support http method 'GET'. When curl -d is used "Hello World!"</returns>
        public string Post()
        {
            return "Hello World!";
        }
    }
}
