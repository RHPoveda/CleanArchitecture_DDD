using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;
internal sealed class AlquilerConfiguration : IEntityTypeConfiguration<Alquiler>
{
    public void Configure(EntityTypeBuilder<Alquiler> builder)
    {
         //* To Table le indica como será el nombre de la tabla
        builder.ToTable("alquileres");

        //? Primary key
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.PrecioPorPeriodo, priceBuilder => {
            priceBuilder.Property(moneda => moneda.TipoMoneda)
            .HasConversion(x => x.Codigo, value => TipoMoneda.FromCodigo(value!));
        });

        builder.OwnsOne(x => x.Mantenimiento, priceBuilder => {
            priceBuilder.Property(moneda => moneda.TipoMoneda)
            .HasConversion(x => x.Codigo, value => TipoMoneda.FromCodigo(value!));
        });

        builder.OwnsOne(x => x.Accesorios, priceBuilder => {
            priceBuilder.Property(moneda => moneda.TipoMoneda)
            .HasConversion(x => x.Codigo, value => TipoMoneda.FromCodigo(value!));
        });

        builder.OwnsOne(x => x.PrecioTotal, priceBuilder => {
            priceBuilder.Property(moneda => moneda.TipoMoneda)
            .HasConversion(x => x.Codigo, value => TipoMoneda.FromCodigo(value!));
        });

        builder.OwnsOne(x => x.Duracion);

        //? definir foreign keys
        builder.HasOne<Vehiculo>()
            .WithMany()
            .HasForeignKey(x => x.VehiculoId);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId);
    }
}