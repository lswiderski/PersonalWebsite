using Microsoft.EntityFrameworkCore;
using PersonalWebsite.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PersonalWebsite.Data
{
    public interface IDataContext
    {
        DbSet<Setting> Settings { get; set; }
        DbSet<Post> Posts { get; set; }

        DbSet<Category> Categories { get; set; }

        DbSet<Tag> Tags { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
