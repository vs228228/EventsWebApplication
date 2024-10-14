using AutoMapper;
using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Application.Interfaces.IUserUseCases;
using EventsWebApplication.Server.Application.Pagination;
using EventsWebApplication.Server.Domain.Interfaces;

namespace EventsWebApplication.Server.Application.UseCases.UserUseCases
{
    public class GetNotificationsUseCase : IGetNotificationsUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetNotificationsUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<NotificationDto>> ExecuteAsync(int userId, int pageNumber, int pageSize)
        {
            var notifications = await _unitOfWork.Notifications.GePagedAsync(userId, pageNumber, pageSize);
            PagedResult<NotificationDto> result = new PagedResult<NotificationDto>
            {
                Items = _mapper.Map<IEnumerable<NotificationDto>>(notifications.Key),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = notifications.Value
            };

            return result;
        }
    }
}
