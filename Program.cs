using System.Text;
using authorization.services;
using AutoMapper;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using sample_one.hubs;
using sample_one.middleware;
using sample_one.models;
using sample_one.models.dto;
using sample_one.services.auth;
using sample_one.services.Bios;
using sample_one.services.Comments;
using sample_one.services.Connection;
using sample_one.services.post;
using sample_one.utility;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard authorization using bearing schema",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();


});




// var configuration = new MapperConfiguration(cfg => 
// {
//     cfg.CreateMap<User, UserResponseDto>();
//     cfg.CreateMap<UserRequestDto, User>();
// });

builder.Services.AddAutoMapper(typeof(Program));






builder.Services.AddSingleton<IFreeSql>(r =>
{
    IFreeSql fsql = new FreeSql.FreeSqlBuilder()
        .UseConnectionString(FreeSql.DataType.Sqlite, @"Data Source=test.db")
        .UseAutoSyncStructure(true)
        .Build();
    return fsql;
});

builder.Services.AddSignalR();



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("ersion=v4.7.2, .NETFramework,Version=v4.8, .NETFramework,Version=v4.8.1' instead of the project target framework 'net7.0'. This package may not be fully compatible with your project.")),
        ValidateIssuer = false,
        ValidateAudience = false

    };





});




// builder.Services.AddSingleton(cloudinary);



builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<IPasswordManager, PasswordManager>();
builder.Services.AddSingleton<ITokenGenerator, JwtTokenGenerator>();
builder.Services.AddSingleton<IBioService, BioService>();
builder.Services.AddSingleton<IConnectionService, ConnectionService>();
builder.Services.AddSingleton<IPostService, PostService>();
builder.Services.AddSingleton<IPostInterAction, PostInterAction>();
builder.Services.AddSingleton<ICommentService, CommentService>();
builder.Services.AddSingleton<IFileUploader, FileUploader>();




builder.Services.AddCors(options =>
   {
       options.AddPolicy("CorsPolicy",
           builder => builder
               .WithOrigins("http://127.0.0.1:5500")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials());
   });



// builder.Configuration.AddConfiguration(()=>{})



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseAuthorization();











app.UseHttpsRedirection();





app.MapControllers();
app.MapWhen((context) =>
        context.Request.Path.StartsWithSegments("/api/Connection"), secureApp =>
        {
            secureApp.UseMiddleware<JwtMiddleware>();
            secureApp.UseEndpoints(endpoints =>
 {
     endpoints.MapControllerRoute(
         name: "connection",
         pattern: "api/{controller=Connection}/{action=Index}/{id?}",
         defaults: new { controller = "Connection", action = "Index" }
     );
 });



        });

app.MapWhen((context) =>
        context.Request.Path.StartsWithSegments("/api/Post"), secureApp =>
        {
            secureApp.UseMiddleware<JwtMiddleware>();
            secureApp.UseEndpoints(endpoints =>
 {
     endpoints.MapControllerRoute(
         name: "post",
         pattern: "api/{controller=Post}/{action=Index}/{id?}",
         defaults: new { controller = "Post", action = "Index" }
     );
 });



        });
app.MapWhen((context) =>
        context.Request.Path.StartsWithSegments("/api/Comment"), secureApp =>
        {
            secureApp.UseMiddleware<JwtMiddleware>();
            secureApp.UseEndpoints(endpoints =>
 {
     endpoints.MapControllerRoute(
         name: "comment",
         pattern: "api/{controller=Comment}/{action=Index}/{id?}",
         defaults: new { controller = "Comment", action = "Index" }
     );
 });



        });


app.UseCors("CorsPolicy");

app.UseEndpoints(endpoints =>
       {
           endpoints.MapHub<NotificationHub>("/notificationHub");
           // Additional endpoints if needed
       });

app.Run();
