using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using ContactList.Data.Repositories;
using ContactList.Infrastructure;
using ContactList.Models;
using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Slapper;
using Swashbuckle.Swagger.Annotations;

namespace ContactList.Controllers
{
    public class DrinksController : ApiController
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = 
            new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()};

        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<Drink>))]
        [Route("~/drinks")]
        public JsonResult<IList<Drink>> Get()
        {
            var drinksRepository = new DrinksRepository();
            return Json(drinksRepository.ListDrinks(), JsonSerializerSettings);
        }

        [HttpGet]
        [Authorize]
        [Route("~/drinks/mine")]
        public JsonResult<List<int>> UserDrinks()
        {
            return Json(new List<int>(), JsonSerializerSettings);
            ;
        } 
    }

}