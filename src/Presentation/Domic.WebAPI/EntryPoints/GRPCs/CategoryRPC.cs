using Grpc.Core;
using Domic.Core.Common.ClassHelpers;
using Domic.Core.Category.Grpc;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.UseCase.CategoryUseCase.Commands.CheckExist;
using Domic.UseCase.CategoryUseCase.Commands.Create;
using Domic.UseCase.CategoryUseCase.Commands.Delete;
using Domic.UseCase.CategoryUseCase.Commands.Update;
using Domic.UseCase.CategoryUseCase.DTOs.ViewModels;
using Domic.UseCase.CategoryUseCase.Queries.ReadAllPaginated;
using Domic.UseCase.CategoryUseCase.Queries.ReadOne;
using Domic.WebAPI.Frameworks.Extensions.Mappers.CategoryMappers;

namespace Domic.WebAPI.EntryPoints.GRPCs;

public class CategoryRPC : CategoryService.CategoryServiceBase
{
    private readonly IMediator      _mediator;
    private readonly IConfiguration _configuration;

    public CategoryRPC(IMediator mediator, IConfiguration configuration)
    {
        _mediator      = mediator;
        _configuration = configuration;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<CheckExistResponse> CheckExist(CheckExistRequest request, ServerCallContext context)
    {
        var query = request.ToQuery<CheckExistCommand>();

        var result = await _mediator.DispatchAsync<bool>(query, context.CancellationToken);

        return result.ToRpcResponse<CheckExistResponse>(_configuration);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<ReadOneResponse> ReadOne(ReadOneRequest request, ServerCallContext context)
    {
        var query = request.ToQuery<ReadOneQuery>();
        
        var result = await _mediator.DispatchAsync<CategoriesViewModel>(query, context.CancellationToken);

        return result.ToRpcResponse<ReadOneResponse>(_configuration);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<ReadAllPaginatedResponse> ReadAllPaginated(ReadAllPaginatedRequest request, 
        ServerCallContext context
    )
    {
        var query = request.ToQuery<ReadAllPaginatedQuery>();
        
        var result =
            await _mediator.DispatchAsync<PaginatedCollection<CategoriesViewModel>>(query, context.CancellationToken);

        return result.ToRpcResponse<ReadAllPaginatedResponse>(_configuration);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<CreateResponse> Create(CreateRequest request, ServerCallContext context)
    {
        var command = request.ToCommand<CreateCommand>();
        
        var result = await _mediator.DispatchAsync<string>(command, context.CancellationToken);

        return result.ToRpcResponse<CreateResponse>(_configuration);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<UpdateResponse> Update(UpdateRequest request, ServerCallContext context)
    {
        var command = request.ToCommand<UpdateCommand>();
        
        var result = await _mediator.DispatchAsync<string>(command, context.CancellationToken);

        return result.ToRpcResponse<UpdateResponse>(_configuration);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<DeleteResponse> Delete(DeleteRequest request, ServerCallContext context)
    {
        var command = request.ToCommand<DeleteCommand>();
        
        var result = await _mediator.DispatchAsync<string>(command, context.CancellationToken);

        return result.ToRpcResponse<DeleteResponse>(_configuration);
    }
}