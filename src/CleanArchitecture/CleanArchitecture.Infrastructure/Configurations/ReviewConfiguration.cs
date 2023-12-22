using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Reviews;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;
internal sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        //* To Table le indica como será el nombre de la tabla
        builder.ToTable("reviews");

        //? Primary key
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Rating)
            .HasConversion(m => m!.Value, value => Rating.Create(value).Value); //? object value

        builder.Property(x => x.Comentario)
            .HasMaxLength(200)
            .HasConversion(m => m!.Value, value => new Comentario(value)); //? object value

        //? definir foreign keys
        builder.HasOne<Vehiculo>()
            .WithMany()
            .HasForeignKey(x => x.VehiculoId);

        builder.HasOne<Alquiler>()
            .WithMany()
            .HasForeignKey(x => x.AlquilerId);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId);
    }
}