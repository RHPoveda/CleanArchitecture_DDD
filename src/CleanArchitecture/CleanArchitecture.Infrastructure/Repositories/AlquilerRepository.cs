using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories;
internal sealed class AlquilerRepository : Repository<Alquiler>, IAlquilerRepository
{
    private static readonly AlquilerStatus[] ActiveAlquilerStatuses = {
        AlquilerStatus.Reservado,
        AlquilerStatus.Confirmado,
        AlquilerStatus.Completado
    };

    public AlquilerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsOverlappingAsync(Vehiculo vehiculo, DateRange duracion, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Alquiler>()
        .AnyAsync(
            x => x.VehiculoId == vehiculo.Id &&
            x.Duracion!.Inicio <= duracion.Fin &&
            x.Duracion!.Fin >= duracion.Inicio && 
            ActiveAlquilerStatuses.Contains(x.Status),
            cancellationToken
        );
    }
}
