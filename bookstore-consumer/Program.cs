using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Confluent.Kafka;

namespace bookstore_consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            ConsumerConfig consumerConfig = null;
            
            CreateConfig();
            CreateConsumerAndConsume();
            

            void CreateConfig()
            {
                consumerConfig = new ConsumerConfig
                {
                    BootstrapServers = "localhost:9092",
                    GroupId = "test-group",
                    AutoOffsetReset = AutoOffsetReset.Earliest
                };
            }

            void CreateConsumerAndConsume()
            {

                var cb = new ConsumerBuilder<string, string>(consumerConfig);

                Console.WriteLine("Press Ctrl+C to exit");

                Console.CancelKeyPress += new ConsoleCancelEventHandler(OnExit);

                using (var consumer = cb.Build())
                {
                    consumer.Subscribe("testTopic");

                    try
                    {
                        while (!cts.IsCancellationRequested)
                        {
                            var cr = consumer.Consume(cts.Token);
                            var offset = cr.TopicPartitionOffset;
                            Console.WriteLine($"Message '{cr.Value}' at: '{offset}'.");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        consumer.Close();
                    }
                }
            }

            void OnExit(object sender, ConsoleCancelEventArgs args)
            {
                args.Cancel = true;
                Console.WriteLine("In OnExit");
                cts.Cancel();

            }

        }
    }
}
