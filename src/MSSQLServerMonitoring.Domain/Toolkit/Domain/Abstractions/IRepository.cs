namespace MSSQLServerMonitoring.Domain.Toolkit.Domain.Abstractions
{
    public interface IRepository<TAggregate> where TAggregate : Entity, IAggregateRoot
    {
    }
}
