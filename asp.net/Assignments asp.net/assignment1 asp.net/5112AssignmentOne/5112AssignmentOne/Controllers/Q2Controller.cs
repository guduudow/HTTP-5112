using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _5112AssignmentOne.Controllers
{
    public class Q2Controller : ApiController
    {
        //<summary>This method returns the square of the integer input</summary>
        //<example>GET api/Q2/{id}</example>
        //<returns>id^2</retuns>
        public int Get(int id)
        {
            int product = id;
            product = (int)Math.Pow(product, 2); 
            return product;
        }
    }
}
