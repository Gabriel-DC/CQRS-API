using CqrsApi.Domain.Context.Handlers;
using CqrsApi.Domain.Context.Repositories;
using CqrsApi.Domain.Context.Services;
using CqrsApi.Infra.StoreContext.DataContext;
using CqrsApi.Infra.StoreContext.Repositories;
using CqrsApi.Infra.StoreContext.Services;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;    
    options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.SmallestSize;
});

builder.Services
    .AddScoped<StoreDbContext, StoreDbContext>()
    .AddTransient<CreateCustomerHandler, CreateCustomerHandler>()
    .AddTransient<ICustomerRepository, CustomerRepository>()
    .AddTransient<IEmailService, EmailService>();

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "CQRS API", Version = "v1" });
});

var app = builder.Build();

if(app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseMvc();
app.UseResponseCompression();

app.UseSwagger();
app.UseSwaggerUI();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CQRS V1");
});

app.Run();
