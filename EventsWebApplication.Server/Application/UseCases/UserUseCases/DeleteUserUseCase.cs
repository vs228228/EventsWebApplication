using EventsWebApplication.Server.Application.Interfaces.IUserUseCases;
using EventsWebApplication.Server.Domain.Interfaces;

namespace EventsWebApplication.Server.Application.UseCases.UserUseCases
{
    public class DeleteUserUseCase : IDeleteUserUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if(user == null)
            {
                throw new KeyNotFoundException();
            }
            var events = await _unitOfWork.Users.GetRegisteredEventsAsync(id);
            foreach (var item in events)
            {
                await _unitOfWork.Events.UnregisterUserFromEventAsync(id, item.Id);
            }
            await _unitOfWork.Users.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
