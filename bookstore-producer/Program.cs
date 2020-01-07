using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Confluent.Kafka;

namespace bookstore
{
    class Program
    {
        static void Main(string[] args)
        {
            AutoResetEvent _closing = new AutoResetEvent(false);

            IProducer<string, string> producer = null;
            ProducerConfig producerConfig = null;

            CreateConfig();
            CreateProducer();
            SendMessage("testTopic", "This is a test42");
            Console.WriteLine("Press Ctrl+C to exit");

            Console.CancelKeyPress += new ConsoleCancelEventHandler(OnExit);
            _closing.WaitOne();


            void OnExit(object sender, ConsoleCancelEventArgs args)
            {
                Console.WriteLine("Exit");
                _closing.Set();
            }

            void CreateConfig()
            {
                producerConfig = new ProducerConfig
                {
                    BootstrapServers = "localhost:9092"
                };
            }

            void CreateProducer()
            {
                var pb = new ProducerBuilder<string, string>(producerConfig);
                producer = pb.Build();
            }

            async void SendMessage(string topic, string message)
            {
                var msg = new Message<string, string>
                {
                    Key = null,
                    Value = message
                };

                var delRep = await producer.ProduceAsync(topic, msg);
                var topicOffset = delRep.TopicPartitionOffset;

                Console.WriteLine($"Delivered '{delRep.Value}' to: {topicOffset}");
            }

        }
    }
}
