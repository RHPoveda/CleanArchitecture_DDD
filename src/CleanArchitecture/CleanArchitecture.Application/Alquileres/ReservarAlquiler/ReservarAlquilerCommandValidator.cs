using FluentValidation;

namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler;
public class ReservarAlquilerCommandValidator : AbstractValidator<ReservarAlquilerCommad>
{
    public ReservarAlquilerCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.VehiculoId).NotEmpty();
        RuleFor(c => c.FechaInicio).LessThan(c => c.FechaFin);
    }
}