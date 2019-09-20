using System;
using System.Linq;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/Products")]
    public class ProductsController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        [HttpGet]
        [Route("GetList")]
        public IHttpActionResult GetList()
        {
            try
            {
                var productCategoryList = (from category in db.Categories
                                           orderby category.CategoryName
                                           select new
                                           {
                                               category.CategoryId,
                                               category.CategoryName,
                                               CategoryPrice = db.Products.Where(x => x.CategoryId == category.CategoryId)
                                                .Sum(x => x.ProductPrice),
                                               Product = from product in db.Products
                                                         where product.CategoryId == category.CategoryId
                                                         select new
                                                         {
                                                             product.ProductId,
                                                             product.ProductName,
                                                             product.ProductPrice
                                                         }
                                           });


                var result = new { productCategoryList, Total = productCategoryList.ToList().Sum(x => x.CategoryPrice) };

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception)
            {
                //If any exception occurs Internal Server Error i.e. Status Code 500 will be returned  
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public IHttpActionResult GetAllCategories()
        {
            try
            {
                var result = db.Categories.ToList().Select(s => new
                {
                    s.CategoryId,
                    s.CategoryName
                }).ToList();


                if (result.Count == 0)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception)
            {
                //If any exception occurs Internal Server Error i.e. Status Code 500 will be returned  
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetProductById/{id}")]
        public IHttpActionResult GetProductById(int id)
        {
            try
            {
                if (id <= 0 || !ProductExists(id))
                {
                    return BadRequest("Please enter valid product Id"); ;
                }

                var result = db.Products.Where(x => x.ProductId == id).Select(s => new
                {
                    s.ProductId,
                    s.ProductName,
                    s.ProductPrice,
                    s.CategoryId,
                    s.Category.CategoryName
                });

                return Ok(result);

            }
            catch (Exception)
            {
                //If any exception occurs Internal Server Error i.e. Status Code 500 will be returned  
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Create(Product product)
        {
            if (product == null || string.IsNullOrEmpty(product.ProductName) || product.ProductPrice <= 0 || product.CategoryId <= 0)
            {
                return BadRequest("Please enter valid data");
            }

            try
            {
                var category = db.Categories.ToList().Where(x => x.CategoryId == product.CategoryId).FirstOrDefault();
                if (category == null)
                {
                    return BadRequest("Please enter valid Category Id");
                }

                db.Products.Add(new Product()
                {
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    CategoryId = product.CategoryId
                });

                db.SaveChanges();

                return Ok(product);
            }
            catch (Exception)
            {
                //If any exception occurs Internal Server Error i.e. Status Code 500 will be returned  
                return InternalServerError();               
            }
        }

        [HttpPut]
        [Route("Edit")]
        public IHttpActionResult Edit(Product product)
        {
            if (product == null || product.ProductId <= 0 || string.IsNullOrEmpty(product.ProductName) || product.ProductPrice <= 0 || product.CategoryId <= 0)
            {
                return BadRequest("Please enter valid data");
            }

            try
            {
               var existingProduct = db.Products.Where(s => s.ProductId == product.ProductId).FirstOrDefault();

                if (existingProduct == null)
                {
                   return BadRequest("Please enter valid product Id");
                }

                var category = db.Categories.ToList().Where(x => x.CategoryId == product.CategoryId).FirstOrDefault();
                if (category == null)
                {
                    return BadRequest("Please enter valid Category Id");
                }

                existingProduct.ProductName = product.ProductName;
                existingProduct.ProductPrice = product.ProductPrice;
                existingProduct.CategoryId = product.CategoryId;

                db.SaveChanges();
                return Ok(existingProduct);
            }
            catch (Exception)
            {
                //If any exception occurs Internal Server Error i.e. Status Code 500 will be returned  
                return InternalServerError();
            }
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.ProductId == id) > 0;
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Please enter valid product Id");
            }
            try
            {
                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return BadRequest("Please enter valid product Id");
                }

                db.Products.Remove(product);
                db.SaveChanges();

                return Ok(product);
            }
            catch (Exception)
            {
                //If any exception occurs Internal Server Error i.e. Status Code 500 will be returned  
                return InternalServerError();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
