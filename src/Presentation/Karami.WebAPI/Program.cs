using Karami.Core.Infrastructure.Extensions;
using Karami.Core.WebAPI.Extensions;
using Karami.WebAPI.EntryPoints.GRPCs;
using Karami.WebAPI.Frameworks.Extensions;

using C_SQLContext = Karami.Persistence.Contexts.C.SQLContext;

/*-------------------------------------------------------------------*/

WebApplicationBuilder builder = WebApplication.CreateBuilder();

#region Configs

builder.WebHost.ConfigureAppConfiguration((context, builder) => builder.AddJsonFiles(context.HostingEnvironment));

#endregion

/*-------------------------------------------------------------------*/

#region Service Container

builder.RegisterHelpers();
builder.RegisterCommandSqlServer<C_SQLContext>();
builder.RegisterCommandRepositories();
builder.RegisterCommandQueryUseCases();
builder.RegisterMessageBroker();
builder.RegisterJobs();
builder.RegisterCaching();
builder.RegisterGrpcServer();
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

public partial class Program {}