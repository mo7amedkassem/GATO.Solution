using Gato.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gato.Repository.Data.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(c => c.Content).IsRequired();
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);


            // soft delete filter
            builder.HasQueryFilter(x => !x.IsDeleted);


            builder.Property(c => c.UserId)
            .IsRequired(false); // Allow null values

            // if post delete => commemnt also delete 

            builder.HasOne(c => c.Post).
                WithMany(p => p.Comments).
                HasForeignKey(c => c.PostId)
               .OnDelete(DeleteBehavior.Cascade);

            //builder.HasOne(c => c.User)
            //.WithMany(u => u.Comments)
            //.HasForeignKey(c => c.UserId)
            //.OnDelete(DeleteBehavior.NoAction);

            
            

        }
    }
}
