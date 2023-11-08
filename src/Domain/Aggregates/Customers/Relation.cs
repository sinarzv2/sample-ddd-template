using Domain.Aggregates.Companies;
using Domain.SeedWork;

namespace Domain.Aggregates.Customers;

public class Relation : Entity
{

    protected Relation()
    {
    }

    public Relation(Customer customer, Company company) : this()
    {
        Company = company ?? throw new ArgumentNullException(paramName: nameof(company));
        Customer = customer ?? throw new ArgumentNullException(paramName: nameof(customer));
    }

    public Customer Customer { get; init; }

    public Company Company { get; init; }

}