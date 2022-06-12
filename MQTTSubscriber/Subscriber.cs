using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System.Threading.Tasks;
using System;
using System.Text;

namespace MQTTSubscriber 
{
    class Subscriber 
    {
        static async Task Main(string[] args)
    {
        var mqttFactory = new MqttFactory();
        var client = mqttFactory.CreateMqttClient();
            var options = new MqttClientOptionsBuilder()
                .WithClientId(Guid.NewGuid().ToString())
                .WithTcpServer("mqtt.1worx.co", 1883)
                .WithClientId("1Worx")
                .WithCredentials("1worx", "kzSP2gQ4cUpn6wxH")
            .WithCleanSession()
            .Build();
        client.UseConnectedHandler(e =>
        {
            Console.WriteLine("Connected to broker successfully");
            var topicFilter = new TopicFilterBuilder()
                .WithTopic("Sino")
                .Build();
            client.SubscribeAsync(topicFilter);
        });
        
        client.UseDisconnectedHandler(e =>
        {
            Console.WriteLine("Disconnected from the broker successfully");
        });
            client.UseApplicationMessageReceivedHandler(e =>
            {
                Console.WriteLine($"Received Message - {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
            });


        await client.ConnectAsync(options);

        Console.ReadLine();

        await client.DisconnectAsync();
    }

    }
    

}


