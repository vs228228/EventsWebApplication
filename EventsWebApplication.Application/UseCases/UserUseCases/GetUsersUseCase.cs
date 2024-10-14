using EventsWebApplication.Application.DTOs.UserDTOs;
using EventsWebApplication.Application.Interfaces.IUserUseCases;
using EventsWebApplication.Application.Pagination;
using EventsWebApplication.Application.Services;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Interfaces;

namespace EventsWebApplication.Application.UseCases.UserUseCases
{
    public class GetUsersUseCase : IGetUsersUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperService _mapper;

        public GetUsersUseCase(IUnitOfWork unitOfWork, IMapperService mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<UserResponseDto>> ExecuteAsync(int pageNumber, int pageSize)
        {
            var users = await _unitOfWork.Users.GetPagedAsync(pageNumber, pageSize);
            PagedResult<UserResponseDto> result = new PagedResult<UserResponseDto>
            {
                Items = await _mapper.Map<IEnumerable<User>, IEnumerable<UserResponseDto>>(users.Key),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = users.Value
            };
            return result;
        }
    }
}
