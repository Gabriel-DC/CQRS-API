var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

var app = builder.Build();

if(app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseMvc();

app.Run();
