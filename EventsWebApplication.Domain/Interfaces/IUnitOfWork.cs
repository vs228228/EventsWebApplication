namespace EventsWebApplication.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IEventRepository Events { get; set; }
        IUserRepository Users { get; set; }
        INotificationRepository Notifications { get; set; }
        Task SaveChangesAsync();
    }
}
