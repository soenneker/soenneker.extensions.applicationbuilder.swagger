using Microsoft.AspNetCore.Builder;

namespace Soenneker.Extensions.ApplicationBuilder.Swagger;

/// <summary>
/// A collection of helpful IApplicationBuilder extension methods involving Swagger/Swashbuckle
/// </summary>
public static class ApplicationBuilderSwaggerExtension
{
    /// <summary>
    /// Configures the Swagger middleware for serving the OpenAPI/Swagger specification and UI.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> used to configure the application's request pipeline.</param>
    /// <remarks>
    /// - Enables Swagger JSON endpoint at <c>/swagger/v1/swagger.json</c>.
    /// - Sets up the Swagger UI at the <c>/swagger</c> route.
    /// - Displays request duration in the Swagger UI.
    /// </remarks>
    public static void ConfigureSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            c.RoutePrefix = "swagger";
            c.DisplayRequestDuration();
            c.EnableDeepLinking();
        });
    }
}
