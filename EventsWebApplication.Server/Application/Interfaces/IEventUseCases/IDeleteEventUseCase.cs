using EventsWebApplication.Server.Application.DTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IEventUseCases
{
    public interface IDeleteEventUseCase
    {
        public Task ExecuteAsync(int id);
    }
}
