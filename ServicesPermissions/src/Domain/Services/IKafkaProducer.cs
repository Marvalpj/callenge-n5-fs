
namespace Domain.Services
{
    public interface IKafkaProducer
    {
        Task ProduceMessage(string topic, string operationType, string content = "");
    }
}
