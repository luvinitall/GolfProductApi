using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GolfProductApi.Entities;
using Microsoft.AspNet.OData.Query;

namespace GolfProductApi.Services
{
    public interface IGolfProductRepository
    {
       IEnumerable<Catalog> GetCatalogs(ODataQueryOptions<Catalog> options);

        Catalog GetCatalogById(short catalogId, ODataQueryOptions<Catalog> options);

        IEnumerable<Category> GetCategories(ODataQueryOptions<Category> options);
        IEnumerable<Family> GetFamilies(ODataQueryOptions<Family> options);
        IEnumerable<Product> GetProducts(ODataQueryOptions<Product> options);
    }
}
