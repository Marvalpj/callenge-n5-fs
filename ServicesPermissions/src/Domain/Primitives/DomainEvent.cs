using MediatR;


namespace Domain.Primitives
{
    public class DomainEvent : INotification
    {
        public long Id { get; }

        public DomainEvent(long id)
        {
            Id = id;
        }
    }
}
