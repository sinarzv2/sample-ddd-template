using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.AccountApplication.Dto;
using Application.GeneralServices.JwtServices;
using Common.Constant;
using Common.Models;
using Common.Resources.Messages;
using Domain.Aggregates.Identity;
using Domain.Entities.IdentityModel;
using Infrastructure.UnitOfWork;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Application.AccountApplication.Services
{
    public class AccountService : IAcountService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(UserManager<User> userManager, IMapper mapper, IJwtService jwtService,
            IOptionsSnapshot<SiteSettings> siteSetting, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
            _jwtSettings = siteSetting.Value.JwtSettings;
        }


    }
}
