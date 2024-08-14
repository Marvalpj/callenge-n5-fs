# config project

# kafka
# crear topico kafka
kafka-topics --bootstrap-server kafka:9092 --create --topic permission-topic
# producer
kafka-console-producer --bootstrap-server kafka:9092 --topic permission-topic
# consumer 
# producer
kafka-console-consumer --bootstrap-server kafka:9092 --topic permission-topic --from-beginning