# sample-dotnetcore-console-kafka
A simple .net core console app using Kafka as a message broker

## Precon:
Install Kafka (latest version) on your local machine: [https://kafka.apache.org/downloads]

## Step 1: Start the Zookeper Server (in separate Terminal):
bin\windows\zookeeper-server-start.bat config/zookeeper.properties

## Step 2: Start the Kafka Server (in separate Terminal):
bin\windows\kafka-server-start.bat config/server.properties

## Step 3: Start the Producer (in separate Terminal):

cd bookstore-producer

dotnet run [arg = number] (i.e. dotnet run 11)

The result should be like that:

Press Ctrl+C to exit
- Delivered 'This is a test: 4' to: testTopic [[0]] @1037
- Delivered 'This is a test: 3' to: testTopic [[0]] @1036
- Delivered 'This is a test: 5' to: testTopic [[0]] @1038
- Delivered 'This is a test: 1' to: testTopic [[0]] @1034
- Delivered 'This is a test: 6' to: testTopic [[0]] @1039
- Delivered 'This is a test: 0' to: testTopic [[0]] @1033
- Delivered 'This is a test: 2' to: testTopic [[0]] @1035
- Delivered 'This is a test: 7' to: testTopic [[0]] @1040
- Delivered 'This is a test: 8' to: testTopic [[0]] @1041
- Delivered 'This is a test: 9' to: testTopic [[0]] @1042
- Delivered 'This is a test: 10' to: testTopic [[0]] @1043

## Step 4: Start the Consumer (in separate Terminal):

cd bookstore-consumer

dotnet run

The result should be like that:

Press Ctrl+C to exit
- Message 'This is a test: 0' at: 'testTopic [[0]] @1033'.
- Message 'This is a test: 1' at: 'testTopic [[0]] @1034'.
- Message 'This is a test: 2' at: 'testTopic [[0]] @1035'.
- Message 'This is a test: 3' at: 'testTopic [[0]] @1036'.
- Message 'This is a test: 4' at: 'testTopic [[0]] @1037'.
- Message 'This is a test: 5' at: 'testTopic [[0]] @1038'.
- Message 'This is a test: 6' at: 'testTopic [[0]] @1039'.
- Message 'This is a test: 7' at: 'testTopic [[0]] @1040'.
- Message 'This is a test: 8' at: 'testTopic [[0]] @1041'.
- Message 'This is a test: 9' at: 'testTopic [[0]] @1042'.
- Message 'This is a test: 10' at: 'testTopic [[0]] @1043'.
