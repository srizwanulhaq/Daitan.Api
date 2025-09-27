//using System;
//using System.Text;
//using System.Threading.Tasks;
//using MQTTnet;
//using MQTTnet.Protocol;
//using MQTTnet.Server;

//public class MqttBroker
//{
//    public async Task StartAsync()
//    {
//        var optionsBuilder = new MqttServerOptionsBuilder()
//            .WithDefaultEndpoint()
//            .WithDefaultEndpointPort(1883)
//            .WithConnectionValidator(OnClientConnecting)
//            .WithSubscriptionInterceptor(OnClientSubscribing)
//            .WithApplicationMessageInterceptor(OnApplicationMessageReceived);

//        var mqttServer = new MqttFactory().CreateMqttServer();

//        await mqttServer.StartAsync(optionsBuilder.Build());

//        Console.WriteLine("MQTT Broker is running...");
//    }

//    private void OnClientConnecting(MqttConnectionValidatorContext context)
//    {
//        Console.WriteLine($"Client '{context.ClientId}' is trying to connect...");

//        if (context.Username != "testuser" || context.Password != "secret")
//        {
//            context.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
//            Console.WriteLine("Connection rejected due to invalid credentials.");
//        }
//        else
//        {
//            context.ReasonCode = MqttConnectReasonCode.Success;
//            Console.WriteLine("Client authenticated successfully.");
//        }
//    }

//    private void OnClientSubscribing(MqttSubscriptionInterceptorContext context)
//    {
//        Console.WriteLine($"Client '{context.ClientId}' is subscribing to '{context.TopicFilter.Topic}'");

//        // Allow subscription
//        context.AcceptSubscription = true;
//    }

//    private void OnApplicationMessageReceived(MqttApplicationMessageInterceptorContext context)
//    {
//        Console.WriteLine($"Message received from '{context.ClientId}':");
//        Console.WriteLine($"  Topic: {context.ApplicationMessage.Topic}");
//        Console.WriteLine($"  Payload: {Encoding.UTF8.GetString(context.ApplicationMessage.Payload ?? Array.Empty<byte>())}");
//    }
//}

