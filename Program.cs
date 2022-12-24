using CoreApiWithImage.Models.Domain;
using CoreApiWithImage.Repository.Abstract;
using CoreApiWithImage.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));
builder.Services.AddTransient<IFileService,FileService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions { 
    FileProvider = new PhysicalFileProvider( 
        Path.Combine(builder.Environment.ContentRootPath, "Uploads")), 
    RequestPath = "/Resources" });

app.UseAuthorization();

app.MapControllers();

app.Run();
