using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PersonalWebsite.Data.Entities;

namespace PersonalWebsite.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20160827130949_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PersonalWebsite.Data.Entities.Setting", b =>
                {
                    b.Property<int>("SettingId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<byte>("Type");

                    b.Property<string>("Value");

                    b.HasKey("SettingId");

                    b.ToTable("Settings");
                });
        }
    }
}
