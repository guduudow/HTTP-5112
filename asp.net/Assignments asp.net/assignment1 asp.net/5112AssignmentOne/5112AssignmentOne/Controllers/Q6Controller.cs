using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _5112AssignmentOne.Controllers
{
    public class Q6Controller : ApiController
    {
        //GET api/Q6/{id}
        public IEnumerable<string> Get(int id)
        {


            double hst = 0.13;
            double dailyRate = 5.50 * id;
            double rate = 5.50;
            double tax = rate * hst;

            //int fortNight = 14;
            double costWithTax = (dailyRate * hst) + dailyRate;
            double cost = Math.Round(costWithTax, 2);



            int numOfFortnights = id;
            int numOfDays = 14 * numOfFortnights;
            int totalCost = numOfDays;

            return new string[]
            {
                  (id + " " + "fortnights at $5.50/FN = " + " " + "$" + Math.Round(dailyRate, 2) + " " + "CAD" + " " + "HST = $0.13 CAD" + " " + "=" + " " + "$"+ tax +"CAD" + " " + "Total Cost =" + " " + "$"+ Math.Round(costWithTax, 2) +"CAD")
            };
           
           
        }
    };
        }
    
