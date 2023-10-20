using FlowerSaleAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowerSaleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductsController : ControllerBase
    {
        private readonly ShopContext _shopContext; //Automatically get the ShopContext injected

        public ProductsController(ShopContext shopContext)
        {
            _shopContext = shopContext;
            _shopContext.Database.EnsureCreated();
        }

        [HttpGet]
        //public IEnumerable<Product> GetAllProducts()
        //{
        //    return _shopContext.Products.ToArray();
        //}
        public async Task <ActionResult> GetAllProducts([FromQuery] ProductParametersQuery queryParameters)
        {
            //return Ok(await _shopContext.Products.ToArrayAsync());
            //when finished Product Controller then:
            IQueryable<Product> products = _shopContext.Products;
            if (queryParameters.MinPrice != null)
            {
                products = products.Where(
                    p => p.Price >= queryParameters.MinPrice.Value);
            }
            if (queryParameters.MaxPrice != null)
            {
                products = products.Where(
                    p => p.Price <= queryParameters.MaxPrice.Value);
            }
            if (!string.IsNullOrEmpty(queryParameters.SearchTerm))
            {
                products = products.Where(
                    //p => p.Sku.ToLower().CompareTo(queryParameters.SearchTerm.ToLower()) ||
                    p => p.Name.ToLower().Contains(queryParameters.SearchTerm.ToLower()));
            }
            if (!string.IsNullOrEmpty(queryParameters.Name))
            {
                products = products.Where(
                    p => p.Name.ToLower().Contains(queryParameters.Name.ToLower()));    
            }
            //Sort
            if (!string.IsNullOrEmpty(queryParameters.sortBy))
            {
                if(typeof(Product).GetProperty(queryParameters.sortBy) != null)
                {
                    products = products.OrderByCustom(queryParameters.sortBy, queryParameters.SortOrder);
                }
            }

            products = products.Skip(queryParameters.Size * (queryParameters.Page - 1)).Take(queryParameters.Size);
            return Ok(await products.ToArrayAsync());

        }
        [Route("api/[controller]")]
        [HttpGet]

        public async Task<ActionResult> GetProduct(int id)
        {
            var product = await _shopContext.Products.FindAsync(id);
            //here use Ok product
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>>PostProduct(Product product)
        {
            _shopContext.Products.Add(product);
            await _shopContext.SaveChangesAsync();
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult>PutProduct(int id, Product product)
        {
            if(id != product.Id)
            {
                return BadRequest();
            }
            //here update our data store
            _shopContext.Entry(product).State = EntityState.Modified;
            try
            {
                await _shopContext.SaveChangesAsync();
            }
            //maybe product have been modified already
            catch(DbUpdateConcurrencyException ex)
            {
                if (!_shopContext.Products.Any(p => p.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }                
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>>DeleteProcudt(int id)
        {
            var product = await _shopContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _shopContext.Products.Remove(product);
            await _shopContext.SaveChangesAsync();

            return product;
        }

        //public ActionResult GetAllProducts()
        //{
        //    return Ok(_shopContext.Products.ToArray());
        //}
        //[Route("api/[controller]")]
        //[HttpGet]

        //public ActionResult GetProduct(int id)
        //{
        //    var product = _shopContext.Products.Find(id);
        //    //here use Ok product
        //    if(product == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(product);
        //}
    }
}
