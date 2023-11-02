using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings;

public class PostMap : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        // TODO: tryna use other column types with postgres
        builder.ToTable("post");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.OwnerId)
            .HasColumnType("int");

        builder.Property(p => p.Image)
            .HasColumnType("text")
            .HasMaxLength(512);

        builder.Property(p => p.Message)
            .HasColumnType("text")
            .HasMaxLength(256);

        builder.Property(p => p.Privacy)
            .HasColumnType("text")
            .HasMaxLength(64);

        // TODO: this may cause an error
        builder.Property(p => p.CreatedAt)
            .HasColumnType("datetime");
    }
}