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
        
        private readonly IProductRepository _productRepository;

        public ProdutosController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products = _productRepository.GetAll();
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
            var product = _productRepository.Get(p => p.ProductId == id);
            if (product is null)
            {
                return NotFound("Produto não encontrado....");
            }
            return Ok(product);
        }
        [HttpGet("products/{id}")]
        public ActionResult<IEnumerable<Product>> GetCategoryProduct(int id)
        {
            var products = _productRepository.GetProductsForCategory(id);
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
            var newProduct = _productRepository.Create(product);

            return new CreatedAtRouteResult("ObterProduto", new { id = newProduct.ProductId }, newProduct);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest("Os ids não batem.....");
            }
            _productRepository.Updated(product);
            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var product = _productRepository.Get(p => p.ProductId == id);
            if (product is null)
            {
                return BadRequest("Produto não encontrado");
            }
            _productRepository.Delete(product);
            return Ok($"Produto deletado com sucesso: {product}");
        }
    }
}