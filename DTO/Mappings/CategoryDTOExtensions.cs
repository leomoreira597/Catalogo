using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catalog.Models;
using Npgsql.Replication;

namespace catalog.DTO.Mappings
{
    public static class CategoryDTOExtensions
    {
        public static CategoryDTO? ToCategoryDTO(this Category category)
        {
            if (category is null)
            {
                return null;
            }
            return new CategoryDTO
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                ImageUrl = category.ImageUrl
            };
        }
        public static Category? ToCategory(this CategoryDTO categoryDTO)
        {
            if (categoryDTO is null)
            {
                return null;
            }
            return new Category
            {
                CategoryId = categoryDTO.CategoryId,
                Name = categoryDTO.Name,
                ImageUrl = categoryDTO.ImageUrl
            };
        }
        public static IEnumerable<CategoryDTO> ToCategoryDTOList(this IEnumerable<Category> categories)
        {
            if (categories is null || !categories.Any())
            {
                return new List<CategoryDTO>();
            }
            return categories.Select(category => new CategoryDTO
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                ImageUrl = category.ImageUrl
            }).ToList();
        }
    }
}