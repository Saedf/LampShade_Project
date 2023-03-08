using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infrastructure.EfCore.Mappings
{
    public class ArticleCategoryMappings:IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder.ToTable("ArticleCategories");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(500);
            builder.Property(x => x.Description).HasMaxLength(2000);
            builder.Property(x => x.Picture).HasMaxLength(500);
            builder.Property(x => x.PictureAlt).HasMaxLength(300);
            builder.Property(x => x.PictureTitle).HasMaxLength(300);
            builder.Property(x => x.Slug).HasMaxLength(300);
            builder.Property(x => x.CanonicalAddress).HasMaxLength(500);
            builder.Property(x => x.MetaDescription).HasMaxLength(500);
            builder.Property(x => x.KeyWords).HasMaxLength(100);



        }
    }
}
