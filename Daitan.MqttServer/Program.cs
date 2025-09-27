

//var config = new ConfigurationBuilder()
//           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//           .Build();

//string connectionString = config.GetConnectionString("DefaultConnection");

//var builder = Host.CreateDefaultBuilder(args)
//    .ConfigureServices((context, services) =>
//    {
//        //services.AddDbContext<ApplicationDbContext>(options =>
//        //  options.UseSqlServer("Server=localhost;Database=Daitan;User Id=sa;Password=SmartLife123++@#;Encrypt=Optional;"));

//        //services.AddScoped<IGatewayDeviceRepository, GatewayDeviceRepository>();
//    });

//var app = builder.Build();

//var scope = app.Services.CreateScope();

//var json = File.ReadAllText("D://2nd.json");
//var repo = scope.ServiceProvider.GetRequiredService<IGatewayDeviceRepository>();
//repo.SaveDeviceReadings(json);





using MQTTnet;
using MQTTnet.Server;
using Newtonsoft.Json;
using System.Text;

var optionsBuilder = new MqttServerOptionsBuilder()
     .WithDefaultEndpoint() // Enables the default TCP endpoint (port 1883)
     .WithDefaultEndpointPort(1883)
     .WithKeepAlive()
     .Build();

        var mqttServer = new MqttFactory().CreateMqttServer(optionsBuilder);

        mqttServer.ClientConnectedAsync += e =>
        {
            Console.WriteLine($"Client connected: {e.ClientId}");
            return Task.CompletedTask;
        };

        mqttServer.ClientDisconnectedAsync += e =>
        {
            Console.WriteLine($"Client disconnected: {e.ClientId}");
            return Task.CompletedTask;
        };
        mqttServer.InterceptingSubscriptionAsync += e =>
        {
            Console.WriteLine("subscribed");
            var data = JsonConvert.SerializeObject(e.Response);
            Console.WriteLine(data);
            return Task.CompletedTask;
        };

        mqttServer.ApplicationMessageEnqueuedOrDroppedAsync += e =>
        {
            Console.WriteLine("Receiveddd ApplicationMessageEnqueuedOrDroppedAsync");

            string topic = e.ApplicationMessage.Topic;
            string payload = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment);
            Console.WriteLine($"Received message: Topic = {topic}, Payload = {payload}");
            return Task.CompletedTask;
        };

        mqttServer.InterceptingPublishAsync += e =>
        {
            Console.WriteLine("Publisheedd");
            string json = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment);
            Console.WriteLine(json);

            //  var repo = scope.ServiceProvider.GetRequiredService<IGatewayDeviceRepository>();
            // var repoRes = repo.SaveDeviceReadings(json);
            // Console.WriteLine(repoRes);

            return Task.CompletedTask;
        };


        await mqttServer.StartAsync();

        Console.WriteLine("MQTT broker is running. Press Enter to exit.");
        Console.ReadLine();

      //  await mqttServer.StopAsync();
    









