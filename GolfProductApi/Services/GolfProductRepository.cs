using System.Collections.Generic;
using System.Linq;
using GolfProductApi.Entities;
using Microsoft.AspNet.OData.Query;

namespace GolfProductApi.Services
{
    public class GolfProductRepository:IGolfProductRepository
    {
        private GolfProductDbContext _context;

        public GolfProductRepository(GolfProductDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Catalog> GetCatalogs(ODataQueryOptions<Catalog> options)
        {

            return options.ApplyTo(_context.Catalogs).Cast<Catalog>().ToArray();
        }

        public Catalog GetCatalogById(short catalogId, ODataQueryOptions<Catalog> options)
        {
            return options.ApplyTo(_context.Catalogs.Where(c=>c.CatalogId == catalogId)).Cast<Catalog>().FirstOrDefault();
        }

        public IEnumerable<Category> GetCategories(ODataQueryOptions<Category> options)
        {
            return options.ApplyTo(_context.Categories).Cast<Category>().ToArray();
        }

        public IEnumerable<Family> GetFamilies(ODataQueryOptions<Family> options)
        {
            return options.ApplyTo(_context.Families).Cast<Family>().ToArray();
        }

        public IEnumerable<Product> GetProducts(ODataQueryOptions<Product> options)
        {
            return options.ApplyTo(_context.Products).Cast<Product>().ToArray();
        }

        IEnumerable<Catalog> IGolfProductRepository.GetCatalogs(ODataQueryOptions<Catalog> options)
        {
            throw new System.NotImplementedException();
        }
    }
}