using CqrsApi.Domain.Context.Handlers;
using CqrsApi.Domain.Context.Repositories;
using CqrsApi.Domain.Context.Services;
using CqrsApi.Infra.StoreContext.DataContext;
using CqrsApi.Infra.StoreContext.Repositories;
using CqrsApi.Infra.StoreContext.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services
    .AddScoped<StoreDbContext, StoreDbContext>()
    .AddTransient<CreateCustomerHandler, CreateCustomerHandler>()
    .AddTransient<ICustomerRepository, CustomerRepository>()
    .AddTransient<IEmailService, EmailService>();

var app = builder.Build();

if(app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseMvc();

app.Run();
