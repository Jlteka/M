using BSystem;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<MessageQueue>();
builder.Services.AddSingleton<IMessageQueue>(m => m.GetRequiredService<MessageQueue>());
builder.Services.AddSingleton<IObservable<object>>(o => o.GetRequiredService<MessageQueue>());
builder.Services.AddSingleton<IMessageHandler, MessageHandler>();
builder.Services.AddHostedService<MessageService>();
builder.Services.AddSingleton<IObserver<object>>(x => x.GetRequiredService<MessageService>());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();