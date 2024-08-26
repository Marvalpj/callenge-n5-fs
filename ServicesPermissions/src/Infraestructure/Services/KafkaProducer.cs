using Confluent.Kafka;
using Domain.Services;
using System.Text.Json;

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

        public async Task ProduceMessage(string topic, string operationType)
        {
            try
            {
                var message = new
                {
                    Id = Guid.NewGuid().ToString(),
                    NameOperation = operationType
                };

                var jsonMessage = JsonSerializer.Serialize(message);
                var deliveryResult = await producer.ProduceAsync(topic, new Message<Null, string> { Value = jsonMessage });
            }
            catch (ProduceException<Null, string> ex)
            {
                Console.WriteLine($"Error al enviar el mensaje: {ex.Error.Reason}");
                throw; // Lanza la excepción original
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
                throw; // Lanza la excepción original
            }
        }
    }
}
