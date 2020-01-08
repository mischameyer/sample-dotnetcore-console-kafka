using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace bookstore
{
    class Program
    {
        static void Main(string[] args)
        {

            var switchMappings = new Dictionary<string, string>()
             {
                 { "-newmsg", "key1" },
                 { "-topic", "key2" },
                 { "-partition", "key3" },
             };
            var builder = new ConfigurationBuilder();
            builder.AddCommandLine(args, switchMappings);

            var config = builder.Build();             

            int _numberOfMessages = 0;
            int _numberOfPartitions = 1;
            int _maxNumberOfMessagesToDisplay = 100;

            if (args.Length == 0)
            {
                _numberOfMessages = 1;
            }
            else
            {
                try
                {
                    _numberOfMessages = Convert.ToInt32(config["Key1"]);
                    _numberOfPartitions = Convert.ToInt32(config["Key3"]);
                }
                catch
                {
                    Console.WriteLine("Invalid argument!");
                    Environment.Exit(0);
                }
            }

            AutoResetEvent _closing = new AutoResetEvent(false);

            IProducer<string, string> producer = null;
            ProducerConfig producerConfig = null;

            CreateConfig();
            CreateProducer();

            for(int i = 0; i < _numberOfMessages; i++)
            {
                var _key = _numberOfPartitions == 0 ? 1 : i % _numberOfPartitions;
                Console.WriteLine(_key);

                SendMessage(!string.IsNullOrWhiteSpace(config["Key2"]) ? config["Key2"] : "testTopic", string.Format("This is a test: {0}", i), _numberOfMessages < _maxNumberOfMessagesToDisplay ? true : false, _key);
            }
            
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
                    BootstrapServers = "localhost:9092",                    
                };
            }

            void CreateProducer()
            {
                var pb = new ProducerBuilder<string, string>(producerConfig);
                producer = pb.Build();
            }

            async void SendMessage(string topic, string message, bool display, int key)
            {                
                var msg = new Message<string, string>
                {
                    Key = key.ToString(),
                    Value = message
                };

                DeliveryResult<string, string> delRep;

                if (key > 1)
                {
                    var p = new Partition(key);
                    var tp = new TopicPartition(topic, p);
                    delRep = await producer.ProduceAsync(tp, msg);
                } else
                {
                    delRep = await producer.ProduceAsync(topic, msg);
                }
                                                
                var topicOffset = delRep.TopicPartitionOffset;

                if (display) { Console.WriteLine($"Delivered '{delRep.Value}' to: {topicOffset}"); }
            }

        }
    }
}
