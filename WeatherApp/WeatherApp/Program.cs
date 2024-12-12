var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost5048",
        builder => builder.WithOrigins("http://127.0.0.1:5500") // Doğru kökeni ekleyin
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});
var app = builder.Build();
app.UseCors("AllowLocalhost5048");
app.MapControllers();


app.Run();
