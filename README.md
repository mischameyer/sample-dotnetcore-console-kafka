# sample-dotnetcore-console-kafka
Two simple .net core console apps (producer & consumer) using Kafka as a message broker. As addition you can use Confluent Control Center by using Option 2 (see below) with Docker.

## Option 1 (manually): Start Zookeeper and Kafka-Server manually

### Precon:
Install Kafka (latest version) on your local machine: [https://kafka.apache.org/downloads]

### Step 1.1: Start the Zookeeper Server (in separate Terminal):
```bin\windows\zookeeper-server-start.bat config/zookeeper.properties```

### Step 1.2: Start the Kafka Server (in separate Terminal):
```bin\windows\kafka-server-start.bat config/server.properties```

## Option 2 (with Docker): Start Zookeeper, Kafka-Server, Broker and Confluent with Docker

### Step 2.1: Clone Confluent-Examples from Github
```git clone https://github.com/confluentinc/examples/tree/5.3.1-post/cp-all-in-one```

### Step 2.2: Docker-Compose
Make sure you have at least 8192MB Memory allocated in docker (Settings, Advanced).

```cd cp-all-in-one```

```docker-compose up -d --build```

### Step 2.3: Check State of Docker-Containers
```docker-compose ps```

All states should have the value 'Up'

### Step 2.4: Use Confluent Control Center
Go to a webbrowser and start Confluent Control Center: localhost:9021

Under 'topics' you can create a new topic called 'testTopic' with 0 partitions.

### Step 3: Start the .NET Console Producer (in separate Terminal):

```cd bookstore-producer```

dotnet run args[-newmsg, -topic, -partition]  (i.e. ```dotnet run -newmsg 24 -topic testTopicP12 -partition 12```)

The result should be like that:

- Press Ctrl+C to exit
- Delivered 'This is a test: 5' to: testTopicP12 [[5]] @19
- Delivered 'This is a test: 2' to: testTopicP12 [[2]] @14
- Delivered 'This is a test: 17' to: testTopicP12 [[5]] @21
- ...

### Step 4: Start the .NET Console Consumer (in separate Terminal):

```cd bookstore-consumer```

dotnet run args[-topic, -group]  (i.e. ```dotnet run -topic testTopicP12 -group testTopic12```)

The result should be like that:

- Press Ctrl+C to exit
- Topic: testTopicP12
- Group: testTopic12
- Message 'This is a test: 22' at: 'testTopicP12 [[10]] @10'.
- Message 'This is a test: 10' at: 'testTopicP12 [[10]] @11'.
- Message 'This is a test: 22' at: 'testTopicP12 [[10]] @12'.
- ...
