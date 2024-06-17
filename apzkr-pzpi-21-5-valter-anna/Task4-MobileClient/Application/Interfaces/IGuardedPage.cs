using Domain.Structs;

namespace Application.Interfaces;

public interface IGuardedPage
{
    IEnumerable<Guard> Guards { get; }
}