using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _5112AssignmentOne.Controllers
{
    public class Q1Controller : ApiController
    {
        //<summary>
        //This method adds ten to whatever number is requested
        //</summary>
        //<example>GET api/Q1/{id}</example>
        //<returns>id + 10</returns>
        public int Get(int id)
        {
            int product = id + 10;
            return product;
        }
    }
}
