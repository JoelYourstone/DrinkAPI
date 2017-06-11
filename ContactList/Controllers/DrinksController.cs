using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using DrinkAPI.Data.Repositories;
using DrinkAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.Swagger.Annotations;

namespace DrinkAPI.Controllers
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
        }

        [HttpPost]
        [Route("~/drinks/add")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(AddDrinkResult))]
        public JsonResult<AddDrinkResult> Add([FromBody] AddDrinkModel drink)
        {
            var drinksRepository = new DrinksRepository();
            if (drinksRepository.Add(drink))
            {
                return Json(new AddDrinkResult() {IsSuccess = true}, JsonSerializerSettings);
            }
            return Json(new AddDrinkResult() { IsSuccess = false , Message = "Drink already exists"}, JsonSerializerSettings);
        }
    }

    public class AddDrinkModel
    {
        public string Name { get; set; }
        public string Glass { get; set; }
        public string Instructions { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}