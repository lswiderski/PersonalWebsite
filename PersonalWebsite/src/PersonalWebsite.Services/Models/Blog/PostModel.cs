using Microsoft.AspNetCore.Html;
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

        public IQueryable<SimplifiedPostViewModel> Search(string query)
        {
            var posts = (from post in db.Posts
                where (post.Content.Contains(query)
                       || post.Excerpt.Contains(query))
                      && post.Status == PostStatusType.PUBLISHED
                select new SimplifiedPostViewModel
                {
                    Name = post.Name,
                    Title = post.Title,
                    Excerpt = post.Excerpt,
                    PostId = post.PostId,
                    ImgURL =
                        db.Set<Image>()
                            .Where(x => x.ImageId == post.HeaderImageId)
                            .Select(x => x.Thumbnail.Path)
                            .FirstOrDefault(),
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
                }).AsQueryable();

            return posts;
        }

        public IQueryable<SimplifiedPostViewModel> GetPublishedSimplifiedPosts()
        {
            var posts = (from post in db.Posts.Include(x => x.PostCategories)
                         .Include(x => x.PostTags)
                         where post.Status == PostStatusType.PUBLISHED
                         select new SimplifiedPostViewModel
                         {
                             Name = post.Name,
                             Title = post.Title,
                             Excerpt = post.Excerpt,
                             PostId = post.PostId,
                             ImgURL = db.Set<Image>().Where(x => x.ImageId == post.HeaderImageId).Select(x => x.Thumbnail.Path).FirstOrDefault(),
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
                         }).AsQueryable();
            return posts;
        }

        public IQueryable<SimplifiedPostViewModel> GetPublishedSimplifiedPostsByTag(string tagName)
        {
            var posts = (from post in db.Posts.Include(x => x.PostCategories)
                         .Include(x => x.PostTags)
                         join postTag in db.PostTags on post.PostId equals postTag.PostId
                         join tag in db.Tags on postTag.TagId equals tag.TagId
                         where post.Status == PostStatusType.PUBLISHED
                         && tag.Name == tagName
                         select new SimplifiedPostViewModel
                         {
                             Name = post.Name,
                             Title = post.Title,
                             Excerpt = post.Excerpt,
                             PostId = post.PostId,
                             ImgURL = db.Set<Image>().Where(x => x.ImageId == post.HeaderImageId).Select(x => x.Thumbnail.Path).FirstOrDefault(),
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
                         }).AsQueryable();
            return posts;
        }

        public IQueryable<SimplifiedPostViewModel> GetPublishedSimplifiedPostsByCategory(string categoryName)
        {

            var posts = (from post in db.Posts.Include(x => x.PostCategories)
                         .Include(x => x.PostTags)
                         join postCategory in db.PostCategories on post.PostId equals postCategory.PostId
                         join category in db.Categories on postCategory.CategoryId equals category.CategoryId
                         where post.Status == PostStatusType.PUBLISHED
                          && category.Name == categoryName
                         select new
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
                             }).ToList(),
                             HeaderImg = db.Set<Image>().Where(x => x.ImageId == post.HeaderImageId).Select(x => x.Thumbnail.Path).FirstOrDefault()
                         }).AsQueryable();

            var selectedposts = posts.Select(x => new SimplifiedPostViewModel
            {
                Name = x.Name,
                Title = x.Title,
                Excerpt = x.Excerpt,
                PostId = x.PostId,
                Categories = x.Categories,
                Tags = x.Tags,
                ImgURL = x.HeaderImg,
            }).AsQueryable();

            return selectedposts;
        }

        public IQueryable<SimplifiedPostViewModel> GetSimplifiedPosts()
        {
            var posts = (from post in db.Posts.Include(x => x.PostCategories)
                         .Include(x => x.PostTags)
                         where post.Status != PostStatusType.DELETED
                         select new SimplifiedPostViewModel
                         {
                             Name = post.Name,
                             Title = post.Title,
                             Excerpt = post.Excerpt,
                             PostId = post.PostId,
                             ImgURL = db.Set<Image>().Where(x => x.ImageId == post.HeaderImageId).Select(x => x.Thumbnail.Path).FirstOrDefault(),
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
                         }).AsQueryable();
            return posts;
        }

        public List<SimplifiedPostViewModel> GetPublishedSimplifiedPostsForFeed(int count)
        {
            var posts = (from post in db.Posts.Include(x => x.PostCategories)
                    .Include(x => x.PostTags)
                where post.Status == PostStatusType.PUBLISHED
                select new SimplifiedPostViewModel
                {
                    Name = post.Name,
                    Title = post.Title,
                    Excerpt = post.Excerpt,
                    PostId = post.PostId,
                    ImgURL =
                        db.Set<Image>()
                            .Where(x => x.ImageId == post.HeaderImageId)
                            .Select(x => x.Thumbnail.Path)
                            .FirstOrDefault(),
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
                }).Take(count).ToList();
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
                Name = model.Name,
                HeaderImageId = model.HeaderImageId
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
                              HeaderImageId = post.HeaderImageId,
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
            var postVM = (from post in db.Posts.Include(x => x.PostCategories).Include(x => x.PostTags).Include(x => x.HeaderImage).ThenInclude(y =>y.File)
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
                              HeaderImageId = post.HeaderImageId,
                              HeaderPath = post.HeaderImage.File.Path,
                              GUID = post.Guid.ToString(),
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

        private IEnumerable<CheckBoxListItem> GetCategoriesCheckBoxListForPost(IEnumerable<int> categoryIds)
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
                              HeaderImageId = post.HeaderImageId,
                              Categories = post.PostCategories.Select(y => y.CategoryId).ToList(),
                              Tags = post.PostTags.Select(y => y.Tag.Name).ToList(),
                          }).FirstOrDefault();

            EditPostViewModel viewModel = new EditPostViewModel
            {
                Name = postVM.Name,
                Title = postVM.Title,
                Excerpt = postVM.Excerpt,
                PostId = postVM.PostId,
                Content = postVM.Content,
                Status = postVM.Status,
                HeaderImageId = postVM.HeaderImageId,
                Categories = this.GetCategoriesCheckBoxListForPost(postVM.Categories).ToList(),
                Tags = postVM.Tags
            };

            return viewModel;
        }

        public void UpdatePost(EditPostViewModel model)
        {
            var post = db.Posts.Include(x => x.PostCategories).ThenInclude(x => x.Category).Include(x => x.PostTags).ThenInclude(x => x.Tag).Where(x => x.PostId == model.PostId).FirstOrDefault();

            post.Name = model.Name;
            post.Title = model.Title;
            post.Content = model.Content;
            post.Excerpt = model.Excerpt;
            post.Status = model.Status;
            post.HeaderImageId = model.HeaderImageId;
            post.ModifiedOn = DateTime.Now;

            if (model.Categories != null)
            {
                foreach (var category in model.Categories)
                {
                    if (category.IsChecked)
                    {
                        if (post.PostCategories == null)
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
                        if (categoryToDelete != null)
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

        public List<UltraSimplifiedPostViewModel> GetTopPublishedPosts(int number, List<string> categories = null)
        {
            var posts = (from post in db.Posts.Include(x => x.PostCategories)
                       .Include(x => x.PostTags)
                         join postCategory in db.PostCategories on post.PostId equals postCategory.PostId
                         join category in db.Categories on postCategory.CategoryId equals category.CategoryId
                         where post.Status == PostStatusType.PUBLISHED
                         && ((categories!= null && categories.Any(x => x == category.Name))
                         || categories == null)
                         select new
                         {
                             Name = post.Name,
                             Title = post.Title,
                             PostId = post.PostId,                           
                             HeaderImg = db.Set<Image>().Where(x => x.ImageId == post.HeaderImageId).Select(x => x.Thumbnail.Path).FirstOrDefault(),
                             PublishedOn = post.PublishedOn
                         }).Take(number).ToList();

            var selectedposts = posts.Select(x => new UltraSimplifiedPostViewModel
            {
                Name = x.Name,
                Title = x.Title,    
                PostId = x.PostId,
                PublishedOn = x.PublishedOn,
                ImgURL = x.HeaderImg,
            }).ToList();

            return selectedposts;
        }
    }
}