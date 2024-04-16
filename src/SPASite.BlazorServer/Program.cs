using SPASite.BlazorServer.App;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddMvc()
    .AddCofoundry(builder.Configuration);

builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<MemberState>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCofoundry();

app.Run();
