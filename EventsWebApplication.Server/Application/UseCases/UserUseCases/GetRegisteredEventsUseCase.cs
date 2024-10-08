﻿using AutoMapper;
using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Application.Interfaces.IUserUseCases;
using EventsWebApplication.Server.Domain.Interfaces;

namespace EventsWebApplication.Server.Application.UseCases.UserUseCases
{
    public class GetRegisteredEventsUseCase : IGetRegisteredEventsUseCase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRegisteredEventsUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventDto>> ExecuteAsync(int userId)
        {
            var events = await _unitOfWork.Users.GetRegisteredEventsAsync(userId);
            return _mapper.Map<IEnumerable<EventDto>>(events);
        }
    }
}
