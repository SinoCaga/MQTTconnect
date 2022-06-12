using MQTTnet;
using System.Text.Json;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using static Newtonsoft.Json.JsonConverter;
using System.Web.Helpers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;

namespace MQTTpublisher
{
    class Publisher 
    {

        
        static async Task Main(string[] args) 
        {
            
           /* var customers = new List<Customer>
            {
                new Customer
                {
                    Name = "Sino",
                    Age = 33,
                    Car = "Ford"
                }
            };
            
             var customerJson = JsonConvert.SerializeObject((customers).ToString());

            Console.WriteLine(customerJson + "+++++++++++++++++");*/

            var mqttFactory = new MqttFactory();
            var client = mqttFactory.CreateMqttClient();
            var options = new MqttClientOptionsBuilder()
                .WithClientId(Guid.NewGuid().ToString())
                .WithTcpServer("mqtt.1worx.co",1883)
                .WithCredentials("1worx", "kzSP2gQ4cUpn6wxH")
                .WithCleanSession()
                .Build();
            client.UseConnectedHandler(e =>
                {
               // Console.WriteLine("Connected to broker successfully");
            });

            client.UseDisconnectedHandler(e =>
            {
                Console.WriteLine("Disconnected from the broker successfully");
            });

            await client.ConnectAsync(options);

            Console.WriteLine("Please press a key to publish the message");

            Console.ReadLine();

            await PublishMessageAsync(client);
            await PublishMessageAsync1(client);
            await PublishMessageAsync2(client);
            await PublishMessageAsync3(client);

            await client.DisconnectAsync();

        }

        private static async Task PublishMessageAsync(IMqttClient client)
        {
            string messagePayLoad = "BMW M4 GTS";
            var message = new MqttApplicationMessageBuilder()
                .WithTopic("Car")
                .WithPayload(messagePayLoad)
                .WithAtLeastOnceQoS()
                .Build();
            if (client.IsConnected) 
            {
                await client.PublishAsync(message);
            }
        }

        private static async Task PublishMessageAsync1(IMqttClient client)
        {
            string messagePayLoad = "Sino is a Guru!";
            var message = new MqttApplicationMessageBuilder()
                .WithTopic("Name")
                .WithPayload(messagePayLoad)
                .WithAtLeastOnceQoS()
                .Build();
            if (client.IsConnected)
            {
                await client.PublishAsync(message);
            }
        }

        private static async Task PublishMessageAsync2(IMqttClient client)
        {
            int messagePayLoad = 8;
            var message = new MqttApplicationMessageBuilder()
                .WithTopic("Age")
                .WithPayload(messagePayLoad.ToString())
                .WithAtLeastOnceQoS()
                .Build();
            if (client.IsConnected)
            {
                await client.PublishAsync(message);
            }
        }
        public class WeatherForecast
        {
            
            
            public int Temperature { get; internal set; }
            public int OilLevel { get; internal set; }
            public int Humidity { get; internal set; }
        }



        private static async Task PublishMessageAsync3(IMqttClient client)
        {
            /* string url = "https://my-json-server.typicode.com/typicode/demo/posts";
             HttpClient httpClient = new HttpClient();
             try
             {
                 var httpResponseMessage = await httpClient.GetAsync(url);
                 string JsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();
                 Console.WriteLine(JsonResponse);

                 var myPost = JsonConvert.DeserializeObject<Post[]>(JsonResponse);

                 foreach (var post in myPost) 
                 {
                     Console.WriteLine($"{post.Id}{post.Title}");
                 }

             }
             catch (global::System.Exception e)
             {

                 Console.WriteLine(e.Message);
             }
             finally 
             {
                 httpClient.Dispose();
             }*/

            /*Function generateRandomJson()
            {
                //, "string", "boolean", "array", "object"
                var choices = "number";
                var choice = chooseOne(choices);

                function chooseOne(choices)
                {
                    return choices[parseInt(Math.random() * choices.length)];
                }
                if (choice == "number")
                {
                    return generateRandomNumber();
                }

                function generateRandomNumber()
                {
                    var maxNum = 2 **32;
                    var number = Math.random() * maxNum;
                    var isInteger = chooseOne([true, false]);
            var isNegative = chooseOne([true, false]);

            if (isInteger) number = parseInt(number);
            if (isNegative) number = -number;

            return number;*/

            /*Function pickRandomQuestion()
            {
                
                var obj_keys = Object.keys(window.questionnaire);
                var ran_key = obj_keys[Math.Floor(Math.random() * obj_keys.length)];
                window.selectedquestion = window.questionnaire[ran_key];
                console.log(window.selectedquestion);
                console.log(window.questionnaire);
            }
            */
            Random random = new Random();
            /*for (int i = 0; i < 10; i++) 
            {
                Console.WriteLine(random.Next(100));
            }*/

            var jsonData = new WeatherForecast
            {
                
                Temperature = random.Next(100),
                OilLevel = random.Next(100),
                Humidity = random.Next(100),
                
            };

            string jsonString = System.Text.Json.JsonSerializer.Serialize(jsonData);

            Console.WriteLine(jsonString);

             /*string jsonData = @"{ 
                
                'Temperature':'45',
                'OilLevel':'35',
                'Humidty':'22'
                }";
            */
            

          
            var details =  JObject.Parse(jsonString);//converting json string to json object then storing it in details
            string messagePayLoad = details.ToString();
            var message = new MqttApplicationMessageBuilder()
                .WithTopic("JsonPropertyValues")
                .WithPayload(messagePayLoad)
                .WithAtLeastOnceQoS()
                .Build();
           
            
            if (client.IsConnected)
            {
                await client.PublishAsync(message);
            }


        }

        
        }

       





    }
        
    


