using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ASystem;
using ASystem.BSystemClient;
using ASystem.RandomMessageGeneration;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddHttpClient("BSystem", client =>
        {
            client.BaseAddress = new Uri("http://localhost:7210/");
        });

        services.Configure<MessageGenerationSettings>(context.Configuration.GetSection(nameof(MessageGenerationSettings)));

        services.AddScoped<IApiClient, BSystemApiClient>();
        services.AddScoped<ISystemClient, BSystemClient>();
        services.AddScoped<IMessageStringGenerator, RandomMessageStringGenerator>();
        services.AddScoped<MessageGenerator>();

        services.AddHostedService<MessageSender>();
    })
    .Build();

await host.RunAsync();