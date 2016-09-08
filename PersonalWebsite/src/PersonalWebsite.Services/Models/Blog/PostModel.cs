using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalWebsite.Common;
using PersonalWebsite.Common.Enums;
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
        private readonly ICategoryModel categoryModel;
        private readonly ITagModel tagModel;

        public PostModel(DataContext db, ICategoryModel categoryModel, ITagModel tagModel)
        {
            this.db = db;
            this.categoryModel = categoryModel;
            this.tagModel = tagModel;
        }

        public List<SimplifiedPostViewModel> GetPublishedSimplifiedPosts()
        {
            var posts = (from post in db.Posts.Include(x => x.PostCategories).Include(x => x.PostTags)
                         where post.Status == PostStatusType.PUBLISHED
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
                         where post.Status != PostStatusType.DELETED
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
                Status = model.Status,
                Name = model.Name
            };

            db.Posts.Add(post);
            if (model.Categories != null)
            {
                var selectedCategories = model.Categories.Where(x => x.IsChecked).Select(x => x.ID).ToList();
                foreach (var category in selectedCategories)
                {
                    var postCategory = new PostCategory
                    {
                        CategoryId = category,
                        PostId = post.PostId
                    };
                    db.PostCategories.Add(postCategory);
                }
            }

            if (model.Tags != null)
            {
                var tags = model.Tags.Distinct();
                foreach (var tag in tags)
                {
                    var tagFromDb = tagModel.GetTag(tag);

                    if(tagFromDb != null)
                    {
                        var postTag = new PostTag
                        {
                            TagId = tagFromDb.TagId,
                            PostId = post.PostId
                        };
                        db.PostTags.Add(postTag);
                    }
                    else
                    {

                        Tag newTag = new Tag
                        {
                            Name = tag,
                        };

                        db.PostTags.Add(new PostTag
                        {
                            Post = post,
                            Tag = newTag
                        });
                    }
                }
            }

            db.SaveChanges();
        }

        public PostViewModel GetPublishedPost(int id)
        {
            var postVM = (from post in db.Posts.Include(x => x.PostCategories).Include(x => x.PostTags)
                          where post.PostId == id
                          && post.Status == PostStatusType.PUBLISHED
                          select new PostViewModel
                          {
                              Name = post.Name,
                              Title = post.Title,
                              Excerpt = post.Excerpt,
                              PostId = post.PostId,
                              Content = new HtmlString(post.Content),
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
                          && post.Status == PostStatusType.PUBLISHED
                          select new PostViewModel
                          {
                              Name = post.Name,
                              Title = post.Title,
                              Excerpt = post.Excerpt,
                              PostId = post.PostId,
                              Content = new HtmlString(post.Content),
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

        private List<CheckBoxListItem> GetCategoriesCheckBoxListForPost(IEnumerable<int> categoryIds)
        {
            var categories = categoryModel.GetCategories().Select(x => new CheckBoxListItem
            {
                Display = x.Tittle,
                ID = x.CategoryId,
                IsChecked = false
            }).ToList();

            foreach (var category in categories)
            {
                foreach (var item in categoryIds)
                {
                    if (category.ID == item)
                    {
                        category.IsChecked = true;
                    }
                }
            }

            return categories;
        }

        public EditPostViewModel GetPostForEdit(int id)
        {
            var postVM = (from post in db.Posts.Include(x => x.PostCategories).Include(x => x.PostTags)
                          where post.PostId == id
                          && post.Status != PostStatusType.DELETED
                          select new
                          {
                              Name = post.Name,
                              Title = post.Title,
                              Excerpt = post.Excerpt,
                              PostId = post.PostId,
                              Content = post.Content,
                              Status = post.Status,
                              Categories = post.PostCategories.Select(y => y.CategoryId).ToList(),
                              Tags = post.PostTags.Select(y => y.Tag.Name).ToList(),
                              AllTags = db.Tags.Select(y => new SelectListItem
                              {
                                  Text = y.Name,
                                  Value = y.TagId.ToString()
                              })
                          }).FirstOrDefault();

            EditPostViewModel viewModel = new EditPostViewModel
            {
                Name = postVM.Name,
                Title = postVM.Title,
                Excerpt = postVM.Excerpt,
                PostId = postVM.PostId,
                Content = postVM.Content,
                Status = postVM.Status,
                Categories = this.GetCategoriesCheckBoxListForPost(postVM.Categories),
                Tags = postVM.Tags
            };

            return viewModel;
        }

        public void UpdatePost(EditPostViewModel model)
        {
            var post = db.Posts.Include(x => x.PostCategories).ThenInclude(x => x.Category).Include(x => x.PostTags).ThenInclude(x =>x.Tag).Where(x => x.PostId == model.PostId).FirstOrDefault();

            post.Name = model.Name;
            post.Title = model.Title;
            post.Content = model.Content;
            post.Excerpt = model.Excerpt;
            post.Status = model.Status;
            post.ModifiedOn = DateTime.Now;

            if (model.Categories != null)
            {
                foreach (var category in model.Categories)
                {
                    if (category.IsChecked)
                    {
                        if(post.PostCategories == null)
                        {
                            post.PostCategories = new List<PostCategory>(); 
                        }
                        if (!post.PostCategories.Any(x => x.CategoryId == category.ID))
                        {
                            var postCategory = new PostCategory
                            {
                                CategoryId = category.ID,
                                PostId = post.PostId
                            };
                            db.PostCategories.Add(postCategory);
                        }
                    }
                    else
                    {
                        var categoryToDelete = post.PostCategories.Where(x => x.CategoryId == category.ID).FirstOrDefault();
                        if(categoryToDelete != null)
                        {
                            db.PostCategories.Remove(categoryToDelete);
                        }
                        
                    }
                }
            }

            if (model.Tags != null)
            {
                var tags = model.Tags.Distinct().ToList();
                var tagsInPost = post.PostTags.Select(x => x.Tag.Name).ToList();
                var tagsToRemove = (from t in tagsInPost
                                    where !tags.Any(x => x == t)
                                    select t);
                var postTags = post.PostTags.Where(x => tagsToRemove.Any(y => y == x.Tag.Name));

                db.PostTags.RemoveRange(postTags);

                var tagsToAdd = tags.Except(tagsInPost).Except(tagsToRemove);
                foreach (var tag in tagsToAdd)
                {
                    var tagFromDb = tagModel.GetTag(tag);

                    if (tagFromDb != null)
                    {
                        var postTag = new PostTag
                        {
                            TagId = tagFromDb.TagId,
                            PostId = post.PostId
                        };
                        db.PostTags.Add(postTag);
                    }
                    else
                    {
                        Tag newTag = new Tag
                        {
                            Name = tag,
                        };

                        db.PostTags.Add(new PostTag
                        {
                            Post = post,
                            Tag = newTag
                        });
                    }
                }
            }

                db.SaveChanges();
        }

        public void DeletePost(int id)
        {
            var post = db.Posts.Where(x => x.PostId == id).FirstOrDefault();

            post.Status = PostStatusType.PUBLISHED;

            db.SaveChanges();
        }
    }
}