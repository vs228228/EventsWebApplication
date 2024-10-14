namespace EventsWebApplication.Application.Interfaces.IUserUseCases
{
    public interface IDeleteUserUseCase
    {
        public Task ExecuteAsync(int id);
    }
}
