using CleanArchitecture.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;
internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //* To Table le indica como será el nombre de la tabla
        builder.ToTable("users");

        //? Primary key
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nombre)
            .HasMaxLength(200)
            .HasConversion(m => m!.Value, value => new Nombre(value)); //? object value

        builder.Property(x => x.Apellido)
            .HasMaxLength(200)
            .HasConversion(m => m!.Value, value => new Apellido(value)); //? object value

        builder.Property(x => x.Email)
            .HasMaxLength(200)
            .HasConversion(m => m!.Value, value => new Email(value)); //? object value

        //? LLaves unicas
        builder.HasIndex(x => x.Email).IsUnique();

    }
}