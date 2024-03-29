using Ecom.Common.MassTransit;
using Ecom.Common.MongoDB;
using Ecom.Common.Settings;
using Ecom.Inventory.Entities;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddServiceDiscovery(o => o.UseEureka());

// var allowedOriginsPolicy = "simpleSpecification";
var serviceSettings = builder.Configuration.GetSection("ServiceSettings").Get<ServiceSettings>();
builder.Services.AddMongo().AddMongoRepository<InventoryItem>("InventoryItems").AddMassTransitWithRabbitMq();

builder.Services.AddAuthentication("Bearer")
.AddIdentityServerAuthentication("Bearer", options =>
{
    options.Authority = "http://authenticationserver:80";
    options.ApiName = "EComAPI";
    options.RequireHttpsMetadata = false;
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
