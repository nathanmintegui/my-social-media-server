using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd();

        builder.Property(u => u.Cep)
            .IsRequired()
            .HasColumnName("cep")
            .HasColumnType("text")
            .HasMaxLength(8);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasColumnName("email")
            .HasColumnType("text")
            .HasMaxLength(255);

        builder.Property(u => u.Name)
            .HasColumnName("name")
            .HasColumnType("text")
            .HasMaxLength(255);

        builder.Property(u => u.Password)
            .IsRequired()
            .HasColumnName("password")
            .HasColumnType("text")
            .HasMaxLength(128);

        builder.Property(u => u.Nickname)
            .IsRequired()
            .HasColumnName("nickname")
            .HasColumnType("text")
            .HasMaxLength(50);

        builder.Property(u => u.Photo)
            .HasColumnName("photo")
            .HasColumnType("text")
            .HasMaxLength(512);

        builder.Property(u => u.BirthDate)
            .IsRequired()
            .HasColumnName("birth_date")
            .HasColumnType("date");
    }
}