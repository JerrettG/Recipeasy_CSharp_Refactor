using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using Recipeasy_CSharp_Refactor.Repositories;
using Recipeasy_CSharp_Refactor.Services;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the AWS services
var awsCredentials = new BasicAWSCredentials(
    System.Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID"),
    System.Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY")
);

var awsRegion = Amazon.RegionEndpoint.GetBySystemName("us-west-1");

builder.Services.AddDefaultAWSOptions(new Amazon.Extensions.NETCore.Setup.AWSOptions
{
    Credentials = awsCredentials,
    Region = awsRegion
});

builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddScoped<DynamoDBContext>(provider =>
{
    var dynamoDbClient = provider.GetService<IAmazonDynamoDB>();
    return new DynamoDBContext(
        dynamoDbClient,
        new DynamoDBContextConfig { IgnoreNullValues = true}
        );
});

// Configure injection of Repositories
builder.Services.AddScoped<FridgeItemRepository>();
builder.Services.AddScoped<SavedRecipeRepository>();

// Configure Redis connection
var redisConnectionString = "localhost:6379";
var redis = ConnectionMultiplexer.Connect(redisConnectionString);

// Configure injection of Redis
builder.Services.AddSingleton<IDatabase>(provider =>
{
    var redis = provider.GetRequiredService<IConnectionMultiplexer>();
    return redis.GetDatabase();
});
builder.Services.AddScoped<FridgeService>();
builder.Services.AddScoped<SavedRecipeService>();
builder.Services.AddScoped<RecipeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

