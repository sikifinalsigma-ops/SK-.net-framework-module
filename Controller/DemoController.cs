using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Server.Controller
{
    public class DemoController : ApiController
    {
        [EnableCors("*", "*", "*")]
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [EnableCors("http://127.0.0.1", "*", "*",SupportsCredentials =true)]
        // GET api/<controller>
        public IEnumerable<string> Get2()
        {
            return new string[] { "value1", "value2" };
        }

        [DisableCors]
        [HttpGet]
        // GET api/<controller>
        public IEnumerable<string> Get3()
        {
            return new string[] { "value1", "value2" };
        }

    }
}