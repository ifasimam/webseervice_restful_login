using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Central.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/
        public IEnumerable<string> Get()
        {
            return new string[] { "Nilai1", "Nilai2" };
        }

        // GET: Api
        public string Get(int id)
        {
            return "value";
        }



        

    }
}
