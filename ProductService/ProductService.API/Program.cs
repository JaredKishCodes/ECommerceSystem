using ProductService.API;
using ProductService.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Swagger setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Controllers and gRPC
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddApiDI(builder.Configuration);

// 👇 Add gRPC and gRPC-Web



var app = builder.Build();

// Swagger UI for dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 👇 Optional: comment out if testing via HTTP (e.g. Postman)
app.UseHttpsRedirection();

app.UseRouting();


app.UseGrpcWeb(); 

app.UseAuthorization();

// 👇 gRPC endpoint with gRPC-Web enabled
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<ProductGrpcService>()
             .EnableGrpcWeb();

    endpoints.MapControllers(); // Also map your API controllers
});

app.Run();
