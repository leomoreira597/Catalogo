using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catalog.Context;
using catalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products = _context.Products.ToList();
            if (products is null)
            {
                return NotFound("Produtos não encontrados....");
            }
            return products;
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Product> GetProducts(int id)
        {
            var product = _context.Products.FirstOrDefault(product => product.ProductId == id);
            if (product is null)
            {
                return NotFound("Produto não encontrado....");
            }
            return product;
        }

        [HttpPost]
        public ActionResult Post(Product product)
        {
            if (product is null)
            {
                return BadRequest();
            }
            _context.Products.Add(product);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", new { id = product.ProductId }, product);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product){
            if (id != product.ProductId)
            {
                return BadRequest("Os ids não batem.....");
            }
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(product);
        }
        
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id){
            var product = _context.Products.FirstOrDefault(product => product.ProductId == id);
            if (product is null)
            {
                return NotFound("Produto não localizado...");
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            
            return Ok(product);
        }
    }
}