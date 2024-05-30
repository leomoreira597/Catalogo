using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using catalog.Context;
using catalog.DTO;
using catalog.Models;
using catalog.Repositories;
using Microsoft.AspNetCore.JsonPatch;
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
        private readonly IMapper _mapper;
        public ProdutosController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> GetProducts()
        {
            var products = _uof.ProductRepository.GetAll();
            if (products is null)
            {
                return NotFound("Produtos não encontrados....");
            }
            var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(productsDto);
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<ProductDTO> GetProducts(int id, [BindRequired] string name)
        {
            var productName = name;
            var product = _uof.ProductRepository.Get(p => p.ProductId == id);
            if (product is null)
            {
                return NotFound("Produto não encontrado....");
            }
            var productDto = _mapper.Map<ProductDTO>(product);
            return Ok(productDto);
        }
        [HttpGet("products/{id}")]
        public ActionResult<IEnumerable<ProductDTO>> GetCategoryProduct(int id)
        {
            var products = _uof.ProductRepository.GetProductsForCategory(id);
            if (products is null)
            {
                return NotFound();
            }
            var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(productsDto);
        }
        [HttpPost]
        public ActionResult<ProductDTO> Post(ProductDTO productDto)
        {
            if (productDto is null)
            {
                return BadRequest();
            }
            var product = _mapper.Map<Product>(productDto);
            var newProduct = _uof.ProductRepository.Create(product);
            _uof.Commit();
            var newProductDto = _mapper.Map<ProductDTO>(newProduct);
            return new CreatedAtRouteResult("ObterProduto", new { id = newProduct.ProductId }, newProduct);
        }

        [HttpPatch("{id}/UpdatePartial")]
        public ActionResult<ProductDtoUpdateResponse> Patch(int id, JsonPatchDocument<ProductDTOUpadateRequest> patchProductDTO)
        {
            if (patchProductDTO is null || id <= 0)
            {
                return BadRequest();
            }
            var product = _uof.ProductRepository.Get(c=> c.ProductId == id);
            if (product is null)
            {
                return NotFound();
            }
            var productUpdateRequest = _mapper.Map<ProductDTOUpadateRequest>(product);
            patchProductDTO.ApplyTo(productUpdateRequest,ModelState);
            if (!ModelState.IsValid || TryValidateModel(productUpdateRequest))
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(productUpdateRequest, product);
            _uof.ProductRepository.Updated(product);
            _uof.Commit();
            return Ok(_mapper.Map<ProductDtoUpdateResponse>(product));
        }

        [HttpPut("{id:int}")]
        public ActionResult<ProductDTO> Put(int id, ProductDTO productDto)
        {
            if (id != productDto.ProductId)
            {
                return BadRequest("Os ids não batem.....");
            }
            var product = _mapper.Map<Product>(productDto);
            var productUpdated = _uof.ProductRepository.Updated(product);
            _uof.Commit();
            var productUpdatedDto = _mapper.Map<ProductDTO>(productUpdated);
            return Ok(productUpdatedDto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ProductDTO> Delete(int id)
        {
            var product = _uof.ProductRepository.Get(p => p.ProductId == id);
            if (product is null)
            {
                return BadRequest("Produto não encontrado");
            }
            var productDeleted = _uof.ProductRepository.Delete(product);
            var productDeletedDto = _mapper.Map<ProductDTO>(productDeleted);
            _uof.Commit();
            return Ok($"Produto deletado com sucesso: {productDeletedDto}");
        }
    }
}