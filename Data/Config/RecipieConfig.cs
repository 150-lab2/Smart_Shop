using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Config
{
    internal class RecipeConfig : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Recipe> builder)
        {
            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(70);

            builder.Property(m => m.ApiId)
                .HasMaxLength(25);
        }
    }
}
