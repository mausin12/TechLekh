using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLekh.Core.Domain;

namespace TechLekh.Infra.Data.EntityConfigurations
{
    internal class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.ToTable("BlogPosts");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Heading)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(b => b.PageTitle)
                .HasColumnType("varchar(1000)")
                .IsRequired();

            builder.HasMany(b => b.Tags)
                .WithMany(t => t.BlogPosts)
                //.UsingEntity(t => t.ToTable("BlogPostTags"));
                .UsingEntity<Dictionary<string, object>>(
                    "BlogPostTags",
                    j => j.HasOne<Tag>()
                          .WithMany()
                          .HasForeignKey("TagId"),
                    j => j.HasOne<BlogPost>()
                          .WithMany()
                          .HasForeignKey("BlogPostId")
                );

            builder.HasMany(b => b.Likes)
                .WithOne()
                .HasForeignKey(l => l.BlogPostId);

            //builder.HasMany(b => b.Likes)
            //    .WithOne(l => l.BlogPost) // If BlogPost Navigation property exist
            //    .HasForeignKey(l => l.BlogPostId);

            builder.HasMany(b => b.Comments)
                .WithOne()
                .HasForeignKey(c => c.BlogPostId);

        }
    }
}
