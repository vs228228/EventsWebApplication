using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.Interfaces.IEventUseCases
{
    public interface IDeleteEventUseCase
    {
        public Task ExecuteAsync(int id);
    }
}
