using Domain.SeedWork;
using Domain.SharedKernel.ValueObjects;

namespace Domain.Aggregates.Agents;

public class Agent : AggregateRoot
{

    private Agent()
    {
    }

    public Agent(FullName fullName) : this()
    {
        FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
    }

    public FullName FullName { get; } = FullName.Default;
}