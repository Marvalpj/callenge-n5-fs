
namespace Infraestructure
{
    public class KafkaMessage
    {
        public Guid Id { get; set; }
        public string NameOperation { get; set; }
        public string Content { get; set; }
    }
}
