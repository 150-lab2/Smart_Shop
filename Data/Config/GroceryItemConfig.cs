using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Config
{
    internal class GroceryItemConfig : IEntityTypeConfiguration<GroceryItem>
    {
        public void Configure(EntityTypeBuilder<GroceryItem> builder)
        {
            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.Department)
                .HasMaxLength(50);
        }
    }
}
