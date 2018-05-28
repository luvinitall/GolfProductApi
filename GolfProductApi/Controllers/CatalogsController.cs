using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GolfProductModel;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace GolfProductApi.Controllers
{
    public class CatalogsController : ODataController
    {
        public CatalogsController()
        {
        }

        [EnableQuery]
        public IActionResult Get()
        {
            ICollection<Catalog> catalogs = new List<Catalog>()
            {
                new Catalog()
                {
                    CatalogId = 1,
                    Description = "Hello"
                }
            };
            return Ok(catalogs);
        }
    }
}