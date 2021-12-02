
using RedisCache.Core;

var builder = WebApplication.CreateBuilder(args);

//add RedisCache.Core services 
builder.Services.AddRedisCache(options =>
{
    options.Configuration = builder.Configuration["RedisCache:Connection"];
    options.InstanceName = builder.Configuration["RedisCache:InstanceName"];
});

builder.Services.AddControllersWithViews();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

});

app.Run();
