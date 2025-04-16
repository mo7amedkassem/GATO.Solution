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
    public  class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Content).IsRequired();
            builder.HasKey(p => p.Id);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.HasQueryFilter(x => !x.IsDeleted);


            builder.HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
