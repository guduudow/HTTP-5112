using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _5112AssignmentOne.Controllers
{
    public class HostingCostController : ApiController
    {
        //GET api/HostingCost/{id}
        /// <summary>
        /// return the amount of fortnights that the id(num of days) is equal to, giving the total cost required
        /// </summary>
        /// <param name="id"></param>
        /// <returns>api/HostingCost{id} => num of Fortnights at 5.50 per fortnight + plus tax = total cost</returns>
        public IEnumerable<string> Get(int id)
        {
            int fortNight = 14;
            int addOne = id + 1;
            double numOfFortnights = Math.Ceiling((double)addOne / fortNight);

            double hst = 0.13;
            double dailyRate = Math.Round((5.50 * numOfFortnights), 2);
            double rate = 5.50;
            double tax = rate * hst;


            double costWithTax = (dailyRate * hst) + dailyRate;
            double cost = Math.Round(costWithTax, 2);





            //int numOfDays = 14 * numOfFortnights;
            //int totalCost = numOfDays;



            return new string[]
            {
                  (numOfFortnights + " " + "fortnights at $5.50/FN = " + " " + "$" + dailyRate + " " + "CAD"),
                  ("HST = $0.13 CAD" + " " + "=" + " " + "$"+ Math.Round(tax,2) +"CAD"),
                  ("Total Cost =" + " " + "$"+ Math.Round(costWithTax, 2) +"CAD")
            };


        }
    };
        }
    
