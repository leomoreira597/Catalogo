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
        private readonly IProductRepository _repository;

        public ProdutosController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products = _repository.GetProducts().ToList();
            if (products is null)
            {
                return NotFound("Produtos não encontrados....");
            }
            return products;
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Product> GetProducts(int id, [BindRequired] string name)
        {
            var productName = name;
            var product = _repository.GetProduct(id);
            if (product is null)
            {
                return NotFound("Produto não encontrado....");
            }
            return Ok(product);
        }

        [HttpPost]
        public ActionResult Post(Product product)
        {
            if (product is null)
            {
                return BadRequest();
            }
            var newProduct = _repository.Create(product);

            return new CreatedAtRouteResult("ObterProduto", new { id = newProduct.ProductId }, newProduct);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest("Os ids não batem.....");
            }
            bool refresh = _repository.Update(product);
            if (refresh)
            {
                return Ok(product);
            }

            return StatusCode(500, $"Falha ao atualizar o produto de id = {id}");
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            bool deleted = _repository.Delete(id);
            if (deleted)
            {
                return Ok("O produto foi excluido");
            }
            else
            {
                return StatusCode(500, $"Falha ao excluir produto de id = {id}");
            }
        }
    }
}