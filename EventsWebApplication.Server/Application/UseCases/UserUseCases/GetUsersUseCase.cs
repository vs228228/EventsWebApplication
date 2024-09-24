using AutoMapper;
using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Application.Interfaces.IUserUseCases;
using EventsWebApplication.Server.Domain.Interfaces;
using EventsWebApplication.Server.Domain.Pagination;

namespace EventsWebApplication.Server.Application.UseCases.UserUseCases
{
    public class GetUsersUseCase : IGetUsersUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUsersUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<UserDto>> ExecuteAsync(int pageNumber, int pageSize)
        {
            var users = await _unitOfWork.Users.GetPagedAsync(pageNumber, pageSize);
            return _mapper.Map<PagedResult<UserDto>>(users);
        }
    }
}
