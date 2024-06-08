using Domic.Core.Infrastructure.Extensions;
using Domic.Core.WebAPI.Extensions;
using Domic.WebAPI.EntryPoints.GRPCs;
using Domic.WebAPI.Frameworks.Extensions;

using C_SQLContext = Domic.Persistence.Contexts.C.SQLContext;

/*-------------------------------------------------------------------*/

WebApplicationBuilder builder = WebApplication.CreateBuilder();

#region Configs

builder.WebHost.ConfigureAppConfiguration((context, builder) => builder.AddJsonFiles(context.HostingEnvironment));

#endregion

/*-------------------------------------------------------------------*/

#region Service Container

builder.RegisterHelpers();
builder.RegisterELK();
builder.RegisterEntityFrameworkCoreCommand<C_SQLContext, string>();
builder.RegisterCommandRepositories();
builder.RegisterCommandQueryUseCases();
builder.RegisterDistributedCaching();
builder.RegisterGrpcServer();
builder.RegisterMessageBroker();
builder.RegisterEventsPublisher();
builder.RegisterServices();

builder.Services.AddMvc();

#endregion

/*-------------------------------------------------------------------*/

WebApplication application = builder.Build();

/*-------------------------------------------------------------------*/

//Primary processing

application.Services.AutoMigration<C_SQLContext>();

/*-------------------------------------------------------------------*/

#region Middleware

if (application.Environment.IsProduction())
{
    application.UseHsts();
    application.UseHttpsRedirection();
}

application.UseRouting();

application.UseEndpoints(endpoints => {

    endpoints.HealthCheck(application.Services);
    
    #region GRPC's Services

    endpoints.MapGrpcService<CategoryRPC>();

    #endregion

});

#endregion

/*-------------------------------------------------------------------*/

application.Run();

/*-------------------------------------------------------------------*/

//For Integration Test

public partial class Program;