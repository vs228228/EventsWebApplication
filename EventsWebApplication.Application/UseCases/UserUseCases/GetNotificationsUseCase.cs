using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.Interfaces.IUserUseCases;
using EventsWebApplication.Application.Pagination;
using EventsWebApplication.Application.Services;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Interfaces;

namespace EventsWebApplication.Application.UseCases.UserUseCases
{
    public class GetNotificationsUseCase : IGetNotificationsUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperService _mapper;

        public GetNotificationsUseCase(IUnitOfWork unitOfWork, IMapperService mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<NotificationDto>> ExecuteAsync(int userId, int pageNumber, int pageSize)
        {
            var notifications = await _unitOfWork.Notifications.GetPagedAsync(userId, pageNumber, pageSize);
            PagedResult<NotificationDto> result = new PagedResult<NotificationDto>
            {
                Items = await _mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationDto>>(notifications.Key),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = notifications.Value
            };

            return result;
        }
    }
}
