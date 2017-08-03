using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    public class ProductController : ApiController
    {
        [HttpGet()]
        public IHttpActionResult Get()
        {
            IHttpActionResult ret = null;
            List<Product> list = new List<Product>();
            list = CreateMockData();
            ret = Ok(list);
            return ret;
        }

        [HttpGet()]
        public IHttpActionResult Get(int id)
        {
            IHttpActionResult ret;
            List<Product> list = new List<Product>();
            Product prod = new Product();

            list = CreateMockData();
            prod = list.Find(p => p.ProductId == id);
            if (prod == null)
            {
                ret = NotFound();
            }
            else {
                ret = Ok(prod);
            }

            return ret;
        }

        [HttpPost()]
        public IHttpActionResult Post(Product product)
        {
            IHttpActionResult ret = null;
            if (Add(product))
            {
                ret = Created<Product>(Request.RequestUri +
                     product.ProductId.ToString(), product);
            }
            else
            {
                ret = NotFound();
            }
            return ret;
        }

        private bool Add(Product product)
        {
            int newId = 0;
            List<Product> list = new List<Product>();
            list = CreateMockData();

            newId = list.Max(p => p.ProductId);
            newId++;
            product.ProductId = newId;
            list.Add(product);
            // TODO: Change to ‘ false ’ to test NotFound()
            return true;
        }

        private List<Product> CreateMockData()
        {
            List<Product> ret = new List<Product>();
            ret.Add(new Product()
            {
                ProductId = 1,
                ProductName = "Extending Bootstrap with CSS, JavaScript and jQuery",
              IntroductionDate = Convert.ToDateTime("11-Jun-2015"),
                Url = "http://bit.ly/1SNzc0i"
            });

            ret.Add(new Product()
            {
                ProductId = 2,
                ProductName = "Build your own Bootstrap Business Application Template in MVC",
              IntroductionDate = Convert.ToDateTime("29-Jan-2015"),
                Url = "http://bit.ly/1I8ZqZg"
            });

            ret.Add(new Product()
            {
                ProductId = 3,
                ProductName = "Building Mobile Web Sites Using Web Forms, Bootstrap, and HTML5",
              IntroductionDate = Convert.ToDateTime("28-Aug-2014"),
                Url = "http://bit.ly/1J2dcrj"
            });

            return ret;
        }
    }
}
