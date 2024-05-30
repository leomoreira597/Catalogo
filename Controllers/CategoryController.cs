using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catalog.Context;
using catalog.DTO;
using catalog.DTO.Mappings;
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
        private readonly IUnitOfWork _uow;
        public CategoryController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<CategoryDTO>> GetCategories()
        {
            var categories = _uow.CategoryRepository.GetAll();
            var categoriesDto = new List<CategoryDTO>();
            foreach(var category in categories)
            {
                var categoryDto = new CategoryDTO
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name,
                    ImageUrl = category.ImageUrl
                };
                categoriesDto.Add(categoryDto);
            }
            return Ok(categoriesDto);
        }

        [HttpGet("Products")]
        public ActionResult<IEnumerable<Category>> GetCategoryWithProducts()
        {
            var category = _uow.CategoryRepository.GetCategoriesWithProducts();
            return Ok(category);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<CategoryDTO> GetCategory(int id)
        {
            var category = _uow.CategoryRepository.Get(C => C.CategoryId == id);
            if (category is null)
            {
                return NotFound("categoria não encontrado....");
            }
            var categoryDto = category.ToCategoryDTO();
            return Ok(categoryDto);
        }

        [HttpPost]
        public ActionResult<CategoryDTO> PostCategory(CategoryDTO categoryDto)
        {
            if (categoryDto is null)
            {
                return BadRequest("Categoria está nula ");
            }

            var category = categoryDto.ToCategory();

            var categoryCreated = _uow.CategoryRepository.Create(category);
            _uow.Commit();
            var newCategoryDto = categoryCreated.ToCategoryDTO();
            return new CreatedAtRouteResult("ObterCategoria", new { id = newCategoryDto.CategoryId }, newCategoryDto);
        }
        [HttpPut]
        public ActionResult<CategoryDTO> Put(int id, CategoryDTO categoryDto)
        {
            if (id != categoryDto.CategoryId)
            {
                return BadRequest("Dados invalidos");
            }
            var category = new Category()
            {
                CategoryId = categoryDto.CategoryId,
                Name = categoryDto.Name,
                ImageUrl = categoryDto.ImageUrl
            };
            var categoryUpdated = _uow.CategoryRepository.Updated(category);
            _uow.Commit();
            var categoryDtoUpdated = categoryUpdated.ToCategoryDTO();
            return Ok(categoryDtoUpdated);
        }
        [HttpDelete("{id:int}")]
        public ActionResult<CategoryDTO> Delete(int id)
        {
            var category = _uow.CategoryRepository.Get(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound("O id de categoria passado não existe");
            }
            var categoryDeleted = _uow.CategoryRepository.Delete(category);
            _uow.Commit();
            var categoryDeletedDto = categoryDeleted.ToCategoryDTO();
            return Ok(categoryDeletedDto);
        }
    }
}