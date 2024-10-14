using AutoMapper;
using EventsWebApplication.Server.Application.DTOs.UserDTOs;
using EventsWebApplication.Server.Application.Interfaces.IUserUseCases;
using EventsWebApplication.Server.Application.Pagination;
using EventsWebApplication.Server.Domain.Entities;
using EventsWebApplication.Server.Domain.Interfaces;

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

        public async Task<PagedResult<UserResponseDto>> ExecuteAsync(int pageNumber, int pageSize)
        {
            var users = await _unitOfWork.Users.GetPagedAsync(pageNumber, pageSize);
            PagedResult<UserResponseDto> result = new PagedResult<UserResponseDto>
            {
                Items = _mapper.Map<IEnumerable<UserResponseDto>>(users.Key),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = users.Value
            };
            return result;
        }
    }
}
