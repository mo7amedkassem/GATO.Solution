using Gato.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gato.Repository.Data.Configuration
{
    internal class Like_CommentsConfiguration : IEntityTypeConfiguration<Like_Comment>
    {
        public void Configure(EntityTypeBuilder<Like_Comment> builder)
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


            builder.HasOne(l => l.comment)
                .WithMany(c => c.likes)
                .HasForeignKey(l => l.CommentId)
                .OnDelete(DeleteBehavior.Cascade);


            //builder.HasOne(l => l.user)
            //    .WithMany(u => u.likes_Comments)
            //    .HasForeignKey(l => l.UserId)
            //    .OnDelete(DeleteBehavior.SetNull);





        }
    }
}