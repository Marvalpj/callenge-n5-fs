using Confluent.Kafka;
using Domain.Services;
using Newtonsoft.Json;

namespace Infraestructure.Services
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly IProducer<Null, string> producer;

        public KafkaProducer(string bootstrapServers)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
                EnableIdempotence = true,
                MessageSendMaxRetries = 2
            };
            producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task ProduceMessage(string topic, string operationType, string content = "")
        {
            try
            {
                KafkaMessage kafkaMessage = new KafkaMessage()
                {
                    Id = Guid.NewGuid(),
                    NameOperation = operationType,
                    Content = content
                };

                await producer.ProduceAsync(topic, new Message<Null, string> { Value = JsonConvert.SerializeObject(kafkaMessage) });
            }
            catch (ProduceException<Null, string> ex)
            {
                Console.WriteLine($"Error al enviar el mensaje: {ex.Error.Reason}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
                throw; 
            }
        }
    }
}
