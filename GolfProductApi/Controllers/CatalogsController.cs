using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GolfProductApi.Entities;
using GolfProductApi.Helpers;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Get()
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
        public IActionResult Get([FromODataUri] short key)
        {
            try
            {
                var catalogs = _context.Catalogs.Where(c => c.CatalogId == key);

                if (!catalogs.Any())
                    return NotFound();

                return Ok(SingleResult.Create(catalogs));
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "An Exception has occured");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        ////[HttpGet]
        ////[ODataRoute("Catalogs({key})/Description")]
        ////public virtual IActionResult GetCatalogProperty([FromODataUri] int key)
        ////{
        ////    var catalog = _context.Catalogs.FirstOrDefault(c => c.CatalogId == key);

        ////    if (catalog == null)
        ////        return NotFound();

        ////    var propertyToGet = Request.RequestUri.Segments.Last();

        ////    if (!catalog.HasProperty(propertyToGet))
        ////        return NotFound();

        ////    var propertyValue = catalog.GetValue(propertyToGet);

        ////    if (propertyValue == null)
        ////        return StatusCode(System.Net.HttpStatusCode.NoContent);


        ////    return this.CreateOKHttpActionResult(propertyValue);
        ////}

        [HttpGet]
        [ODataRoute("Catalogs({key})/CatalogCategories")]
        [EnableQuery]
        public IActionResult GetCatalogCollectionProperty([FromODataUri] int key)
        {
            var catalog = _context.Catalogs.Include("CatalogCategories").Where(c => c.CatalogId == key);

            if (!catalog.Any())
                return NotFound();


            return Ok(_context.Categories.Where(f => f.CatalogCategories.Any(c => c.CatalogId == key)));


        }

        //[HttpGet]
        //[ODataRoute("Catalogs({key})/Description/$value")]
        //public IActionResult GetCatalogPropertyRawValue([FromODataUri] int key)
        //{
        //    var catalog = _context.Catalogs.FirstOrDefault(c => c.CatalogId == key);

        //    if (catalog == null)
        //        return NotFound();



        //    var propertyToGet = Request.RequestUri.Segments[Request.RequestUri.Segments.Length - 2].TrimEnd('/');

        //    if (!catalog.HasProperty(propertyToGet))
        //        return NotFound();

        //    var propertyValue = catalog.GetValue(propertyToGet);

        //    if (propertyValue == null)
        //        return StatusCode(System.Net.HttpStatusCode.NoContent);


        //    return this.CreateOKHttpActionResult(propertyValue.ToString());
        //}
        [HttpPost]
        public IActionResult Post(Catalog catalog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Catalogs.Add(catalog);
            _context.SaveChanges();

            return Created(catalog);
        }

        public IActionResult Put([FromODataUri] int key, Catalog catalog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentCatalog = _context.Catalogs.FirstOrDefault(c => c.CatalogId == key);
            if (currentCatalog == null)
                return NotFound();

            catalog.CatalogId = currentCatalog.CatalogId;


            _context.Entry(currentCatalog).CurrentValues.SetValues(catalog);

            _context.SaveChanges();

            return StatusCode(StatusCodes.Status204NoContent);
        }

        public IActionResult Patch([FromODataUri] int key, Delta<Catalog> patch)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var currentCatalog = _context.Catalogs.FirstOrDefault(c => c.CatalogId == key);
            if (currentCatalog == null)
                return NotFound();

            patch.Patch(currentCatalog);
            _context.SaveChanges();

            return StatusCode(StatusCodes.Status204NoContent);
        }

        public IActionResult Delete([FromODataUri] int key)
        {
            var currentCatalog = _context.Catalogs.Include("CatalogCategories").FirstOrDefault(c => c.CatalogId == key);
            if (currentCatalog == null)
                return NotFound();

            //Can not delete if the catalog is linked to any families;
            if (currentCatalog.CatalogCategories.Any())
            {
                return new ContentResult()
                {
                    StatusCode = StatusCodes.Status409Conflict,
                    Content = "This catalog is being referenced by other categories."
                };

            }

            _context.Catalogs.Remove(currentCatalog);
            _context.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        //[HttpPost]
        //[ODataRoute("Catalogs({key})/CatalogCategories/$ref")]
        //public IActionResult CreateLinkToCatalog([FromODataUri] short key, [FromBody] Uri link)
        //{
        //    var currentCatalog = _context.Catalogs.Include("CatalogCategories").FirstOrDefault(c => c.CatalogId == key);
        //    if (currentCatalog == null)
        //        return NotFound();

        //    short keyOfCategoryToAdd = Request.GetKeyValue<short>(link);

        //    if (currentCatalog.CatalogCategories.Any(i => i.CatalogId == keyOfCategoryToAdd))
        //        return BadRequest($"The family with id {keyOfCategoryToAdd} is already linked to this catalog");


        //    var categoryLinkToAdd = _context.Categories.Include("CatalogCategories").FirstOrDefault(f => f.CategoryId == keyOfCategoryToAdd);

        //    if (categoryLinkToAdd == null)
        //        return NotFound();

        //    currentCatalog.CatalogCategories.Add(new CatalogCategory() { CategoryId = keyOfCategoryToAdd, CatalogId = key });
        //    // categoryLinkToAdd.CatalogCategories = currentCatalog;
        //    _context.SaveChanges();

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //[HttpPut]
        //[ODataRoute("Catalogs({key})/CatalogCategories({relatedKey})/$ref")]
        //public IActionResult UpdateLinkToFamily([FromODataUri] int key, [FromODataUri] int relatedKey, [FromBody] Uri link)
        //{
        //    //////var currentCatalog = _context.Categories.Include("Families").FirstOrDefault(c => c.CatalogId == key);
        //    //////if (currentCatalog == null)
        //    //////    return NotFound();

        //    //////var familyToRemove = currentCatalog.Families.FirstOrDefault(f => f.FamilyId == relatedKey);

        //    //////if (familyToRemove == null)
        //    //////    return NotFound();

        //    //////int keyOfFamilyToAdd = Request.GetKeyValue<int>(link);

        //    //////if (currentCatalog.Families.Any(i => i.FamilyId == keyOfFamilyToAdd))
        //    //////    return BadRequest($"The family with id {keyOfFamilyToAdd} is already linked to this catalog");


        //    //////var familyLinkToAdd = _context.Families.Include("Catalog").FirstOrDefault(f => f.FamilyId == keyOfFamilyToAdd);

        //    //////if (familyLinkToAdd == null)
        //    //////    return NotFound();


        //    //////currentCatalog.Families.Remove(familyToRemove);
        //    //////_context.Families.Remove(familyToRemove);


        //    //////currentCatalog.Families.Add(familyLinkToAdd);
        //    //////familyLinkToAdd.Catalog = currentCatalog;
        //    //////_context.SaveChanges();

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //[HttpDelete]
        //[ODataRoute("Catalogs({key})/CatalogCategories({relatedKey})/$ref")]
        //public IActionResult DeleteLinkToFamily([FromODataUri] int key, [FromODataUri] int relatedKey)
        //{
        //    var currentCatalog = _context.Catalogs.Include("CatalogCategories").FirstOrDefault(c => c.CatalogId == key);
        //    if (currentCatalog == null)
        //        return NotFound();

        //    //////var familyToRemove = currentCatalog.Families.FirstOrDefault(f => f.FamilyId == relatedKey);

        //    //////if (familyToRemove == null)
        //    //////    return NotFound();

        //    //////familyToRemove.Catalog = null;
        //    //////currentCatalog.Families.Remove(familyToRemove);
        //    //////_context.Families.Remove(familyToRemove);

        //    _context.SaveChanges();
        //    return StatusCode(HttpStatusCode.NoContent);

        //}


    }
}