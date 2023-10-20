using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _5112AssignmentTwo.Controllers
{
    //2010 CCC - J1 "What's n, Daddy?"
    ///<summary>Writeshow many times a number (between 1 and 10) can be represented with fingers.</summary>
    ///<name> param="n"></name>
    ///<example>The number 5 can be represented 3 times (5 fingers and zero) (4 fingers and 1 finger) and (3 fingers and 2 fingers)</example>
    ///

    public class WhatNumberDaddyController : ApiController
    {
        [Route("J1/WhatNumberDaddy/{id}")]
        [HttpGet]
        public string WhatNumberDaddy(int id)
     
        {
            if (id == 1) {
                return "1";
            } else if (id == 2)
            {
                return "2";
            } else if (id == 3)
            {
                return "2";
            } else if (id == 4)
            {
                return "3";
            } else if (id == 5)
            {
                return "3";
            } else if (id == 6)
            {
                return "3";
            } else if (id == 7)
            {
                return "2";
            } else if (id == 8) {
                return "2";
            } else if (id == 9)
            {
                return "1";
            } else if (id == 10)
            {
                return "1";
            } else
            {
               return (id + " is not a number that can be represented with hands. please enter a number between 1 and 10");
            }
            
        }
    }
}
