using PersonalWebsite.Common;
using PersonalWebsite.Data;
using PersonalWebsite.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PersonalWebsite.Services.Models
{
    public class CategoryModel : ICategoryModel
    {
        private readonly DataContext db;

        public CategoryModel(DataContext db)
        {
            this.db = db;
        }

        public void AddCategory(AddCategoryViewModel model)
        {
            db.Categories.Add(new Category
            {
                Name = model.Name,
                Tittle = model.Tittle
            });
            db.SaveChanges();
        }

        public CategoryViewModel GetCategory(int id)
        {
            var category = (from cat in db.Categories
                            where cat.CategoryId == id
                            select new CategoryViewModel
                            {
                                Name = cat.Name,
                                Tittle = cat.Tittle,
                                Uses = cat.Uses,
                                CategoryId = cat.CategoryId
                            }).FirstOrDefault();

            return category;
        }

        public List<CheckBoxListItem> GetEmptyCategoriesCheckBoxList()
        {
            var categories = GetCategories();

            var viewModel = categories.Select(x => new CheckBoxListItem
            {
                Display = x.Tittle,
                ID = x.CategoryId,
                IsChecked = false
            }).ToList();

            return viewModel;
        }

        public List<CategoryViewModel> GetCategories()
        {
            var categories = (from cat in db.Categories
                              select new CategoryViewModel
                              {
                                  Name = cat.Name,
                                  Tittle = cat.Tittle,
                                  Uses = cat.Uses,
                                  CategoryId = cat.CategoryId
                              }).ToList();

            return categories;
        }

        public void EditCategory(EditCategoryViewModel model)
        {
            var category = db.Categories.SingleOrDefault(x => x.CategoryId == model.CategoryId);

            if (category != null)
            {
                category.Name = model.Name;
                category.Tittle = model.Tittle;
                db.SaveChanges();
            }
        }

        public int GetUsesOfCategory(int id)
        {
            var uses = db.Categories.Where(x => x.CategoryId == id).Select(x => x.Uses).FirstOrDefault();

            return uses;
        }

        public void CalculateUses(int id)
        {
            var category = db.Categories.SingleOrDefault(x => x.CategoryId == id);
            if (category == null)
            {
                return;
            }
            var uses = category.PostCategories.Where(y => y.CategoryId == id).Count();

            category.Uses = uses;
            db.SaveChanges();
        }

        public List<CategoryViewModel> GetCategoriesUsedByPost(int id)
        {
            //TODO I'm not sure if I need use condition here, check it later
            var categories = db.Posts.SingleOrDefault(x => x.PostId == id)?.PostCategories.Select(y => new CategoryViewModel
            {
                Name = y.Category.Name,
                CategoryId = y.Category.CategoryId,
                Tittle = y.Category.Tittle,
                Uses = y.Category.Uses
            }).ToList();

            return categories;
        }

        public void DeleteCategory(int id)
        {
            var category = db.Categories.SingleOrDefault(x => x.CategoryId == id);

            if (category != null)
            {
                db.Categories.Remove(category);
                db.SaveChanges();
            }
        }

        public EditCategoryViewModel GetCategoryForEdit(int id)
        {
            var category = GetCategory(id);
            if (category != null)
            {
                return new EditCategoryViewModel
                {
                    Name = category.Name,
                    Tittle = category.Tittle,
                    CategoryId = category.CategoryId
                };
            }
            return new EditCategoryViewModel();
        }
    }
}