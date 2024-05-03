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
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            var categories = _context.Categories.AsNoTracking().ToList();
            if (categories is null)
            {
                return NotFound("categorias não encontradas...");
            }
            return categories;
        }

        [HttpGet("Products")]
        public ActionResult<IEnumerable<Category>> GetCategoryWithProducts()
        {
            return _context.Categories.Include(product => product.Products).AsNoTracking().ToList();
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Category> GetCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(category => category.CategoryId == id);
            if (category is null)
            {
                return NotFound("categoria não encontrado....");
            }
            return category;
        }

        [HttpPost]
        public ActionResult PostCategory(Category category)
        {
            if (category is null)
            {
                return BadRequest("Categoria está nula ");
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterCategoria", new { id = category.CategoryId }, category);
        }
    }
}