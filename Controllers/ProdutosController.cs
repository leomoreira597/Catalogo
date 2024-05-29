using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catalog.Context;
using catalog.Models;
using catalog.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        
        private readonly IUnitOfWork _uof;

        public ProdutosController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products = _uof.ProductRepository.GetAll();
            if (products is null)
            {
                return NotFound("Produtos não encontrados....");
            }
            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Product> GetProducts(int id, [BindRequired] string name)
        {
            var productName = name;
            var product = _uof.ProductRepository.Get(p => p.ProductId == id);
            if (product is null)
            {
                return NotFound("Produto não encontrado....");
            }
            return Ok(product);
        }
        [HttpGet("products/{id}")]
        public ActionResult<IEnumerable<Product>> GetCategoryProduct(int id)
        {
            var products = _uof.ProductRepository.GetProductsForCategory(id);
            if (products is null)
            {
                return NotFound();
            }
            return Ok(products);
        }
        [HttpPost]
        public ActionResult Post(Product product)
        {
            if (product is null)
            {
                return BadRequest();
            }
            var newProduct = _uof.ProductRepository.Create(product);
            _uof.Commit();

            return new CreatedAtRouteResult("ObterProduto", new { id = newProduct.ProductId }, newProduct);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest("Os ids não batem.....");
            }
            _uof.ProductRepository.Updated(product);
            _uof.Commit();
            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var product = _uof.ProductRepository.Get(p => p.ProductId == id);
            if (product is null)
            {
                return BadRequest("Produto não encontrado");
            }
            _uof.ProductRepository.Delete(product);
            return Ok($"Produto deletado com sucesso: {product}");
        }
    }
}