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
            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Establish a many-to-many relationship between Recipe's and Grocery Items
            builder.HasMany(m => m.Items)
                .WithMany();
        }
    }
}
