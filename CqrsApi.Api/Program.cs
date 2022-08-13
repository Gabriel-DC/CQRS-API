using CqrsApi.Domain.Context.Handlers;
using CqrsApi.Domain.Context.Repositories;
using CqrsApi.Domain.Context.Services;
using CqrsApi.Infra.StoreContext.DataContext;
using CqrsApi.Infra.StoreContext.Repositories;
using CqrsApi.Infra.StoreContext.Services;
using CqrsApi.Shared;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

//var teste = builder.Configuration["KKKK"];

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

builder.Services.AddElmahIo(opts =>
{
    opts.ApiKey = AppSharedSettings.ElmahApiKey;
    opts.LogId = AppSharedSettings.ElmahLogId;
});

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Optimal;
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

app.UseElmahIo();

app.UseMvc();
app.UseResponseCompression();

app.UseSwagger();
app.UseSwaggerUI();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CQRS V1");
});

app.Run();
