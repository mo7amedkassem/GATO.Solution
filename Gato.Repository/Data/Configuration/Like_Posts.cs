using Gato.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gato.Repository.Data.Configuration
{
    public class Like_Posts : IEntityTypeConfiguration<Like_Post>
    {
        public void Configure(EntityTypeBuilder<Like_Post> builder)
        {
            builder.HasKey(l => l.Id);

            ////Composite Index
            //builder.HasIndex(l => new{ l.UserId , l.CommentId }).IsUnique();
            //builder.HasIndex(l => new { l.UserId, l.PostId }).IsUnique();
            //check row no deleted
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.HasQueryFilter(x => !x.IsDeleted);


            // builder.HasOne(l => l.post).
            // WithMany(p => p.likes).
            // HasForeignKey(l => l.PostId)
            //.OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(l => l.post)
                .WithMany(c => c.likes_posts)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.Cascade);


            //builder.HasOne(l => l.user)
            //    .WithMany(u => u.likes_posts)
            //    .HasForeignKey(l => l.UserId)
            //    .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
