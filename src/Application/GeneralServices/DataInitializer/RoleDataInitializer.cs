using Common.Constant;
using Domain.Aggregates.Identity;
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
                _unitOfWork.RoleRepository.Add(Role.Create(ConstantRoles.Admin).Data);
            }
            if (!_unitOfWork.RoleRepository.TableNoTracking.Any(p => p.Name == ConstantRoles.User))
            {
                _unitOfWork.RoleRepository.Add(Role.Create(ConstantRoles.User).Data);
            }

            _unitOfWork.CommitChangesAsync().Wait();
        }
    }
}
