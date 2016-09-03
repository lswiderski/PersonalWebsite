using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Services.Models
{
    public interface ICategoryModel
    {
        void AddCategory(AddCategoryViewModel model);

        void DeleteCategory(int id);
        CategoryViewModel GetCategory(int id);
        List<CategoryViewModel> GetCategories();
        void EditCategory(EditCategoryViewModel model);
        int GetUsesOfCategory(int id);
        void CalculateUses(int id);
        List<CategoryViewModel> GetCategoriesUsedByPost(int id);

        EditCategoryViewModel GetCategoryForEdit(int id);
    }
}
