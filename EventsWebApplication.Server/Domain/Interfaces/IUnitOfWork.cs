namespace EventsWebApplication.Server.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IEventRepository Events { get; set; }
        IUserRepository Users { get; set; }
        Task SaveChangesAsync();
    }
}
