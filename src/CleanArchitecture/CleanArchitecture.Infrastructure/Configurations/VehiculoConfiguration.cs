using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;
internal sealed class VehiculoConfiguration : IEntityTypeConfiguration<Vehiculo>
{
    public void Configure(EntityTypeBuilder<Vehiculo> builder)
    {
        //* To Table le indica como será el nombre de la tabla
        builder.ToTable("vehiculos");

        //? Primary key
        builder.HasKey(x => x.Id);

        //? Se le indica que es dueño de una dirección
        //* A nivel de posgresql los campos se añadiran a nivel de tabla
        //* Esto para cuando es un object value con multiples propiedades
        builder.OwnsOne(x => x.Direccion);

        //* Esto cuando es un object value con una propiedad
        builder.Property(x => x.Modelo)
            .HasMaxLength(200)
            .HasConversion(m => m!.Value, value => new Modelo(value)); //? Debido a que es un object value, se tiene que convertir a un tipo de dato primitivo

        builder.Property(x => x.Vin)
            .HasMaxLength(500)
            .HasConversion(m => m!.Value, value => new Vin(value)); //? object value
        
        //* Este caso es cuando dentro de un object value hay otro dentro de el
        builder.OwnsOne(x => x.Precio, priceBuilder => {
            priceBuilder.Property(moneda => moneda.TipoMoneda)
            .HasConversion(x => x.Codigo, value => TipoMoneda.FromCodigo(value!));
        });

        builder.OwnsOne(x => x.Mantenimiento, priceBuilder => {
            priceBuilder.Property(moneda => moneda.TipoMoneda)
            .HasConversion(x => x.Codigo, value => TipoMoneda.FromCodigo(value!));
        });

        builder.Property<uint>("Version").IsRowVersion();

    }
}