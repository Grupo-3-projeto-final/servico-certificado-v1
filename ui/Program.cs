using servico_certificado.Web.Routes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("CorsPolicy", builder => builder
//         .AllowAnyOrigin()
//         .AllowAnyMethod()
//         .AllowAnyHeader());
// });


var app = builder.Build();

// app.UseCors("CorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseHttpsRedirection();

new GerarCertificadoRoute(app).Register();

app.Run();