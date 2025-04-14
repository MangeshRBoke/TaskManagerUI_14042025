using TaskManagerUI.Controllers;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddHttpClient<TaskController>(client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7271/api/tasks");
//})
//.ConfigurePrimaryHttpMessageHandler(() =>
//{
//    return new HttpClientHandler
//    {
//        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
//    };
//});


var taskApiBaseUrl = builder.Configuration["TaskApi:BaseUrl"];

builder.Services.AddHttpClient("TaskAPI", client =>
{
    client.BaseAddress = new Uri(taskApiBaseUrl);
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
//app.MapDefaultControllerRoute();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Task}/{action=Index}/{id?}");

app.Run();
