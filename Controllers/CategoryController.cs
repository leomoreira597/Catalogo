using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catalog.Context;
using catalog.Filters;
using catalog.Models;
using catalog.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            var category = _categoryRepository.GetAll();
            return Ok(category);
        }

        [HttpGet("Products")]
        public ActionResult<IEnumerable<Category>> GetCategoryWithProducts()
        {
           var category = _categoryRepository.GetCategoriesWithProducts();
           return Ok(category);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Category> GetCategory(int id)
        {
            var category = _categoryRepository.Get(C => C.CategoryId == id);
            if (category is null)
            {
                return NotFound("categoria não encontrado....");
            }
            return Ok(category);
        }

        [HttpPost]
        public ActionResult PostCategory(Category category)
        {
            if (category is null)
            {
                return BadRequest("Categoria está nula ");
            }
            var categoryCreated = _categoryRepository.Create(category);
            return new CreatedAtRouteResult("ObterCategoria", new { id = categoryCreated.CategoryId }, categoryCreated);
        }
        [HttpPut]
        public ActionResult Put(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest("Dados invalidos");
            }
            _categoryRepository.Updated(category);
            return Ok(category);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var category = _categoryRepository.Get(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound("O id de categoria passado não existe");
            }
            var categoryDeleted = _categoryRepository.Delete(category);
            return Ok(categoryDeleted);
        }
    }
}