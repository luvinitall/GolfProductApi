using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GolfProductApi.Entities;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GolfProductApi.Controllers
{
    public class CatalogsController : ODataController
    {
        private ILogger<CatalogsController> _logger;
        private GolfProductDbContext _context;

        public CatalogsController(GolfProductDbContext context, ILogger<CatalogsController> logger)
        {
            _context = context;
            _logger = logger;
            //HttpContext.RequestServices.GetService(typeof(ILogger<CatalogsController>));
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult Get(ODataQueryOptions<Catalog> options)
        {
            try
            {
                return Ok(_context.Catalogs);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e,"An Exception has occured");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [HttpGet]
        [EnableQuery]
        [ODataRoute("Catalogs({key})")]
        public IActionResult Get([FromODataUri] short key, ODataQueryOptions<Catalog> options)
        {
            try
            {
                throw new Exception();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "An Exception has occured");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}