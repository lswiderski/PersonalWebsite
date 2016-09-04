using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalWebsite.Data;
using PersonalWebsite.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalWebsite.Services.Models
{
    public class PostModel : IPostModel
    {
        private readonly DataContext db;

        public PostModel(DataContext db)
        {
            this.db = db;
        }

        public List<SimplifiedPostViewModel> GetPublishedSimplifiedPosts()
        {
            var posts = (from post in db.Posts.Include(x => x.PostCategories).Include(x => x.PostTags)
                         where post.IsPublished == true
                         && post.IsDeleted == false
                         && post.IsTrashed == false
                         select new SimplifiedPostViewModel
                         {
                             Name = post.Name,
                             Title = post.Title,
                             Excerpt = post.Excerpt,
                             PostId = post.PostId,
                             Categories = post.PostCategories.Select(y => new CategoryViewModel
                             {
                                 CategoryId = y.CategoryId,
                                 Name = y.Category.Name,
                                 Tittle = y.Category.Tittle
                             }).ToList(),
                             Tags = post.PostTags.Select(y => new TagViewModel
                             {
                                 TagId = y.TagId,
                                 Name = y.Tag.Name
                             }).ToList()
                         }).ToList();
            return posts;
        }

        public List<SimplifiedPostViewModel> GetSimplifiedPosts()
        {
            var posts = (from post in db.Posts.Include(x => x.PostCategories).Include(x => x.PostTags)
                         where post.IsDeleted == false
                         select new SimplifiedPostViewModel
                         {
                             Name = post.Name,
                             Title = post.Title,
                             Excerpt = post.Excerpt,
                             PostId = post.PostId,
                             Categories = post.PostCategories.Select(y => new CategoryViewModel
                             {
                                 CategoryId = y.CategoryId,
                                 Name = y.Category.Name,
                                 Tittle = y.Category.Tittle
                             }).ToList(),
                             Tags = post.PostTags.Select(y => new TagViewModel
                             {
                                 TagId = y.TagId,
                                 Name = y.Tag.Name
                             }).ToList()
                         }).ToList();
            return posts;
        }

        public void AddPost(AddPostViewModel model)
        {
            var post = new Post
            {
                Content = model.Content,
                CreatedOn = DateTime.Now,
                Title = model.Title,
                Excerpt = model.Excerpt,
                Guid = Guid.NewGuid(),
                IsPublished = model.IsPublished,
                Name = model.Name
            };

            db.Posts.Add(post);
            if(model.Categories != null)
            {
                foreach (var category in model.Categories)
                {
                    var postCategory = new PostCategory
                    {
                        CategoryId = category,
                        PostId = post.PostId
                    };
                    db.PostCategories.Add(postCategory);
                }
            }
           
            if(model.Tags != null)
            {
                foreach (var tag in model.Tags)
                {
                    var postTag = new PostTag
                    {
                        TagId = tag,
                        PostId = post.PostId
                    };
                    db.PostTags.Add(postTag);
                }
            }
            

            db.SaveChanges();
        }

        public PostViewModel GetPublishedPost(int id)
        {
            var postVM = (from post in db.Posts.Include(x => x.PostCategories).Include(x => x.PostTags)
                          where post.PostId == id
                          && post.IsPublished == true
                          && post.IsDeleted == false
                          && post.IsTrashed == false
                          select new PostViewModel
                          {
                              Name = post.Name,
                              Title = post.Title,
                              Excerpt = post.Excerpt,
                              PostId = post.PostId,
                              Content = post.Content,
                              CreatedOn = post.CreatedOn,
                              Categories = post.PostCategories.Select(y => new CategoryViewModel
                              {
                                  CategoryId = y.CategoryId,
                                  Name = y.Category.Name,
                                  Tittle = y.Category.Tittle
                              }).ToList(),
                              Tags = post.PostTags.Select(y => new TagViewModel
                              {
                                  TagId = y.TagId,
                                  Name = y.Tag.Name
                              }).ToList()
                          }).FirstOrDefault();

            return postVM;
        }

        public PostViewModel GetPublishedPost(string name)
        {
            var postVM = (from post in db.Posts.Include(x => x.PostCategories).Include(x => x.PostTags)
                          where post.Name == name
                          && post.IsPublished == true
                          && post.IsDeleted == false
                          && post.IsTrashed == false
                          select new PostViewModel
                          {
                              Name = post.Name,
                              Title = post.Title,
                              Excerpt = post.Excerpt,
                              PostId = post.PostId,
                              Content = post.Content,
                              CreatedOn = post.CreatedOn,
                              Categories = post.PostCategories.Select(y => new CategoryViewModel
                              {
                                  CategoryId = y.CategoryId,
                                  Name = y.Category.Name,
                                  Tittle = y.Category.Tittle
                              }).ToList(),
                              Tags = post.PostTags.Select(y => new TagViewModel
                              {
                                  TagId = y.TagId,
                                  Name = y.Tag.Name
                              }).ToList()
                          }).FirstOrDefault();

            return postVM;
        }

        public EditPostViewModel GetPostForEdit(int id)
        {
            var postVM = (from post in db.Posts.Include(x => x.PostCategories).Include(x => x.PostTags)
                          where post.PostId == id
                          && post.IsDeleted == false
                          select new EditPostViewModel
                          {
                              Name = post.Name,
                              Title = post.Title,
                              Excerpt = post.Excerpt,
                              PostId = post.PostId,
                              Content = post.Content,
                              IsPublished = post.IsPublished,
                              IsTrashed = post.IsTrashed,
                              Categories = post.PostCategories.Select(y => new CategoryViewModel
                              {
                                  CategoryId = y.CategoryId,
                                  Name = y.Category.Name,
                                  Tittle = y.Category.Tittle
                              }).ToList(),
                              Tags = post.PostTags.Select(y => new TagViewModel
                              {
                                  TagId = y.TagId,
                                  Name = y.Tag.Name
                              }).ToList(),
                               AllCategories = db.Categories.Select(y => new SelectListItem
                               {
                                    Value = y.CategoryId.ToString(),
                                     Text = y.Tittle
                               }).ToList(),
                               AllTags = db.Tags.Select (y => new SelectListItem
                               {
                                    Text = y.Name,
                                    Value = y.TagId.ToString()
                               })
                          }).FirstOrDefault();

            return postVM;
        }

        public void UpdatePost(EditPostViewModel model)
        {
            var post = db.Posts.Where(x => x.PostId == model.PostId).FirstOrDefault();

            post.Name = model.Name;
            post.Title = model.Title;
            post.Content = model.Content;
            post.Excerpt = model.Excerpt;
            post.IsPublished = model.IsPublished;
            post.IsTrashed = model.IsTrashed;
            post.ModifiedOn = DateTime.Now;
            if(model.Tags != null)
            {
                post.PostTags = model.Tags.Select(y => new PostTag
                {
                    PostId = post.PostId,
                    TagId = y.TagId
                }).ToList();
            }
            
            if(model.Categories != null)
            {
                post.PostCategories = model.Categories.Select(y => new PostCategory
                {
                    PostId = post.PostId,
                    CategoryId = y.CategoryId
                }).ToList();
            }
          

            db.SaveChanges();
        }

        public void DeletePost(int id)
        {
            var post = db.Posts.Where(x => x.PostId == id).FirstOrDefault();

            post.IsDeleted = true;

            db.SaveChanges();
        }
    }
}