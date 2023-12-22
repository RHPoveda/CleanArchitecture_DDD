
using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Vehiculos;
public static class VehiculoErrors
{
    public static Error NotFound = new(
        "Vehiculo.NotFound",
        "No existe el vehiculo buscado por este id"
    );
}