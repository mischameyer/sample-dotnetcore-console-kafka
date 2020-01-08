# sample-dotnetcore-console-kafka
Two simple .net core console apps (producer & consumer) using Kafka as a message broker. As addition you can use Confluent Control Center by using Option 2 (see below) with docker.

## Precon:
Install Kafka (latest version) on your local machine: [https://kafka.apache.org/downloads]

## Option 1 (manually): Start Zookeeper and Kafka-Server manually

### Step 1.1: Start the Zookeeper Server (in separate Terminal):
bin\windows\zookeeper-server-start.bat config/zookeeper.properties

### Step 1.2: Start the Kafka Server (in separate Terminal):
bin\windows\kafka-server-start.bat config/server.properties

## Option 2 (with Docker): Start Zookeeper, Kafka-Server, Broker and Confluent with Docker

### Step 2.1: Clone Confluent-Examples from Github
git clone https://github.com/confluentinc/examples/tree/5.3.1-post/cp-all-in-one

### Step 2.2: Docker-Compose
make sure you have at least 8192MB Memory allocated in docker (Settings, Advanced).

cd cp-all-in-one
docker-compose up -d --build

### Step 2.3: Check State of Docker-Containers
docker-compose ps

Alle States have to be set to 'Up'

### Step 2.4: Use Confluent Control Center
Go to a webbrowser and start Confluent Control Center: localhost:9021

Under 'topics' you can create a new topic called 'testTopic' with 0 partitions.

### Step 3: Start the .NET Console Producer (in separate Terminal):

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

### Step 4: Start the .NET Console Consumer (in separate Terminal):

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
