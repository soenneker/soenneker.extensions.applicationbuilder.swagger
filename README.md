[![](https://img.shields.io/nuget/v/soenneker.extensions.applicationbuilder.swagger.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.applicationbuilder.swagger/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.applicationbuilder.swagger/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.applicationbuilder.swagger/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.applicationbuilder.swagger.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.applicationbuilder.swagger/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.applicationbuilder.swagger/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.applicationbuilder.swagger/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.ApplicationBuilder.Swagger

Adds Swashbuckle's Swagger JSON middleware and a Swagger UI configured for a single `v1` document.

## Installation

```bash
dotnet add package Soenneker.Extensions.ApplicationBuilder.Swagger
```

## Register Swagger services

```csharp
using Microsoft.OpenApi;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Example API",
        Version = "v1"
    });
});
```

This package configures middleware only. `AddSwaggerGen()` and a document named `v1` must already be registered.

## Configure the pipeline

```csharp
using Soenneker.Extensions.ApplicationBuilder.Swagger;

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
    app.ConfigureSwagger();

app.MapControllers();
app.Run();
```

`ConfigureSwagger()` provides:

- the default JSON route at `/swagger/{documentName}/swagger.json`
- Swagger UI at `/swagger`
- a UI entry for the `v1` document
- request-duration display and deep links in the UI

The UI uses a relative `./v1/swagger.json` URL so it follows an application `PathBase` when hosted below a reverse-proxy subpath.

The helper does not inspect the hosting environment, require authentication, or hide the OpenAPI document. Call it only in intended environments or place suitable access controls in front of both `/swagger` and `/swagger/v1/swagger.json`; API descriptions can reveal routes, request models, and security scheme metadata even when the underlying endpoints remain authorized.

The `v1` document name and `/swagger` UI prefix are fixed. Applications that publish multiple documents or need custom UI settings should configure `UseSwagger()` and `UseSwaggerUI()` directly.
