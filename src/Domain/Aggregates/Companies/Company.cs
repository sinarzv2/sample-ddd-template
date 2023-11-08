using Common.Utilities;
using Domain.Aggregates.Companies.ValueObjects;
using Domain.SeedWork;

namespace Domain.Aggregates.Companies;

public class Company : AggregateRoot
{
    private Company()
    {
    }

    public Company(CompanyName name, NationalIdentity nationalIdentity, string address, string description) : this()
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));

        NationalIdentity = nationalIdentity ?? throw new ArgumentNullException(paramName: nameof(nationalIdentity));

        Address = address.Fix() ?? string.Empty;

        Description = description.Fix() ?? string.Empty;
    }

    public string Address { get; } = string.Empty;

    public string Description { get; } = string.Empty;

    public CompanyName Name { get; } = CompanyName.Default;

    public NationalIdentity NationalIdentity { get; } = NationalIdentity.Default;


}