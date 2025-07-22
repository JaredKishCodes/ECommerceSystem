using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using ProductService.API;
using ProductService.Application.Services;
using ProductService.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Controllers and gRPC
builder.Services.AddControllers();
builder.Services.AddApiDI(builder.Configuration);
builder.Services.AddGrpc(); // 👈 Add this line for gRPC support

// Configure Kestrel for both HTTP/1.1 and HTTP/2
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5002, listenOptions =>
    {

        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
    });
});

var app = builder.Build();

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// DB Migration
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
    dbContext.Database.Migrate();
}

// Middleware
app.UseRouting();

app.UseGrpcWeb();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<ProductGrpcService>()
             .EnableGrpcWeb(); // gRPC with gRPC-Web support

    endpoints.MapControllers(); // REST API for Swagger
});

app.Run();
