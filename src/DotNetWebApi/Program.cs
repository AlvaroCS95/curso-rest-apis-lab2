using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;

WebHost.CreateDefaultBuilder().Configure(app =>
{
    app.UseRouting();
    app.UseEndpoints(e =>
    {
        e.MapGet("/", c => c.Response.WriteAsync("Hello world!"));
        e.MapGet("hello", c => c.Response.WriteAsync("Hello, visitor!"));
        e.MapGet("hello/{name}", context => {

            context.Response.WriteAsync($"Hello, {context.Request.RouteValues["name"]}");
            return System.Threading.Tasks.Task.CompletedTask;

        });

        
        e.MapGet("hellox/{name}", context => 

            context.Response.WriteAsJsonAsync(new {Message="Hello, visitante"})
            

        );
        //e.MapGet("hello/{name}", c => c.Response.WriteAsync($"Hello, {c.Request.RouteValues["name"]}"));

       e.MapPost("hellos", async context => {

           var value = new StringValues(context.Request.Host.Host);
         //  byte[] buffer = new byte[context.Request.Body.Length]; 
         //  var content = await context.Request.Body.ReadAsync(buffer,0,(int) context.Request.Body.Length);

         using var reader = new StreamReader (context.Request.Body,System.Text.Encoding.UTF8);
         var content = await reader.ReadToEndAsync();

            context.Response.Headers.Add("Host-name",value);
            
            System.Console.WriteLine(content);
            context.Response.StatusCode=200;

     });
        
    });
}).Build().Run();


/*
{}
curso-rest-apis-lab2
*/