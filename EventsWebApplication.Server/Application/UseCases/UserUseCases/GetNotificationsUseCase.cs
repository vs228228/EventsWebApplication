using AutoMapper;
using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Application.Interfaces.IUserUseCases;
using EventsWebApplication.Server.Domain.Interfaces;
using EventsWebApplication.Server.Domain.Pagination;

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
            return _mapper.Map<PagedResult<NotificationDto>>(notifications);
        }
    }
}
