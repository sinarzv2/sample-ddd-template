using Domain.SeedWork;

namespace Application.AccountApplication.Queries
{
    public class ExistUserByUsernameQuery: IQuery<bool>
    {
        public ExistUserByUsernameQuery(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; init; }
    }
}
