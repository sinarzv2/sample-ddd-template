using Common.Constant;
using Domain.Entities.IdentityModel;
using Infrastructure.UnitOfWork;

namespace Application.GeneralServices.DataInitializer
{
    public class RoleDataInitializer : IDataInitializer
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleDataInitializer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void InitializeData()
        {
            if (!_unitOfWork.RoleRepository.TableNoTracking.Any(p => p.Name == ConstantRoles.Admin))
            {
                _unitOfWork.RoleRepository.Add(new Role()
                {
                    Name = ConstantRoles.Admin,
                    NormalizedName = ConstantRoles.Admin.ToUpper()
                });
            }
            if (!_unitOfWork.RoleRepository.TableNoTracking.Any(p => p.Name == ConstantRoles.User))
            {
                _unitOfWork.RoleRepository.Add(new Role()
                {
                    Name = ConstantRoles.User,
                    NormalizedName = ConstantRoles.User.ToUpper()
                });
            }

            _unitOfWork.CommitChangesAsync().Wait();
        }
    }
}
