using Grpc.Core;
using Karami.Core.Common.ClassHelpers;
using Karami.Core.Grpc.Category;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Core.WebAPI.Extensions;
using Karami.UseCase.CategoryUseCase.Commands.CheckExist;
using Karami.UseCase.CategoryUseCase.Commands.Create;
using Karami.UseCase.CategoryUseCase.Commands.Delete;
using Karami.UseCase.CategoryUseCase.Commands.Update;
using Karami.UseCase.CategoryUseCase.DTOs.ViewModels;
using Karami.UseCase.CategoryUseCase.Queries.ReadAllPaginated;
using Karami.UseCase.CategoryUseCase.Queries.ReadOne;
using Karami.WebAPI.Frameworks.Extensions.Mappers.CategoryMappers;

namespace Karami.WebAPI.EntryPoints.GRPCs;

public class CategoryRPC : CategoryService.CategoryServiceBase
{
    private readonly IMediator      _mediator;
    private readonly IConfiguration _configuration;

    public CategoryRPC(IMediator mediator, IConfiguration configuration)
    {
        _mediator      = mediator;
        _configuration = configuration;
    }

    public override async Task<CheckExistResponse> CheckExist(CheckExistRequest request, ServerCallContext context)
    {
        var query = request.ToQuery<CheckExistCommand>();

        var result = await _mediator.DispatchAsync<bool>(query, context.CancellationToken);

        return result.ToRpcResponse<CheckExistResponse>(_configuration);
    }

    public override async Task<ReadOneResponse> ReadOne(ReadOneRequest request, ServerCallContext context)
    {
        var query = request.ToQuery<ReadOneQuery>();
        
        var result = await _mediator.DispatchAsync<CategoriesViewModel>(query, context.CancellationToken);

        return result.ToRpcResponse<ReadOneResponse>(_configuration);
    }

    public override async Task<ReadAllPaginatedResponse> ReadAllPaginated(ReadAllPaginatedRequest request, 
        ServerCallContext context
    )
    {
        var query = request.ToQuery<ReadAllPaginatedQuery>();
        
        var result =
            await _mediator.DispatchAsync<PaginatedCollection<CategoriesViewModel>>(query, context.CancellationToken);

        return result.ToRpcResponse<ReadAllPaginatedResponse>(_configuration);
    }

    public override async Task<CreateResponse> Create(CreateRequest request, ServerCallContext context)
    {
        var command = request.ToCommand<CreateCommand>(context.GetHttpContext().GetTokenOfGrpcHeader());
        
        var result = await _mediator.DispatchAsync<string>(command, context.CancellationToken);

        return result.ToRpcResponse<CreateResponse>(_configuration);
    }

    public override async Task<UpdateResponse> Update(UpdateRequest request, ServerCallContext context)
    {
        var command = request.ToCommand<UpdateCommand>(context.GetHttpContext().GetTokenOfGrpcHeader());
        
        var result = await _mediator.DispatchAsync<string>(command, context.CancellationToken);

        return result.ToRpcResponse<UpdateResponse>(_configuration);
    }

    public override async Task<DeleteResponse> Delete(DeleteRequest request, ServerCallContext context)
    {
        var command = request.ToCommand<DeleteCommand>(context.GetHttpContext().GetTokenOfGrpcHeader());
        
        var result = await _mediator.DispatchAsync<string>(command, context.CancellationToken);

        return result.ToRpcResponse<DeleteResponse>(_configuration);
    }
}