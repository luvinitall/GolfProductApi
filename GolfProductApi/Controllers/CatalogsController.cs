using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GolfProductModel;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GolfProductApi.Controllers
{
    public class CatalogsController : ODataController
    {
        private ILogger<CatalogsController> _logger;
        public CatalogsController(ILogger<CatalogsController> logger)
        {
            _logger = logger;
            //HttpContext.RequestServices.GetService(typeof(ILogger<CatalogsController>));
        }

        [EnableQuery]
        public IActionResult Get()
        {
            try
            {
                ICollection<Catalog> catalogs = new List<Catalog>()
                {
                    new Catalog()
                    {
                        Id = 1,
                        Description = "Hello"
                    }
                };
                return Ok(catalogs);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e,"An Exception has occured");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }
    }
}