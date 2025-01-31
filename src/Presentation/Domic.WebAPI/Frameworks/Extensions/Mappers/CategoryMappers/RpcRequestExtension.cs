using Domic.Core.Category.Grpc;
using Domic.UseCase.CategoryUseCase.Commands.CheckExist;
using Domic.UseCase.CategoryUseCase.Commands.Create;
using Domic.UseCase.CategoryUseCase.Commands.Delete;
using Domic.UseCase.CategoryUseCase.Commands.Update;
using Domic.UseCase.CategoryUseCase.Queries.ReadAllPaginated;
using Domic.UseCase.CategoryUseCase.Queries.ReadOne;

namespace Domic.WebAPI.Frameworks.Extensions.Mappers.CategoryMappers;

//Query
public static partial class RpcRequestExtension
{
    public static T ToQuery<T>(this CheckExistRequest request)
    {
        object Request = null;

        if (typeof(T) == typeof(CheckExistCommand))
        {
            Request = new CheckExistCommand {
                CategoryId = request.TargetId?.Value
            };
        }

        return (T)Request;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ToQuery<T>(this ReadOneRequest request)
    {
        object Request = null;

        if (typeof(T) == typeof(ReadOneQuery))
        {
            Request = new ReadOneQuery {
                CategoryId = request.TargetId?.Value
            };
        }

        return (T)Request;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ToQuery<T>(this ReadAllPaginatedRequest request)
    {
        object Request = null;

        if (typeof(T) == typeof(ReadAllPaginatedQuery))
        {
            Request = new ReadAllPaginatedQuery {
                PageNumber   = request.PageNumber?.Value ,
                CountPerPage = request.CountPerPage?.Value 
            };
        }
        
        return (T)Request;
    }
}

//Command
public partial class RpcRequestExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ToCommand<T>(this CreateRequest request)
    {
        object Request = null;

        if (typeof(T) == typeof(CreateCommand))
        {
            Request = new CreateCommand {
                Name  = request.Name?.Value 
            };
        }

        return (T)Request;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ToCommand<T>(this UpdateRequest request)
    {
        object Request = null;

        if (typeof(T) == typeof(UpdateCommand))
        {
            Request = new UpdateCommand {
                Id    = request.TargetId?.Value ,
                Name  = request.Name?.Value
            };
        }

        return (T)Request;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ToCommand<T>(this DeleteRequest request)
    {
        object Request = null;

        if (typeof(T) == typeof(DeleteCommand))
        {
            Request = new DeleteCommand {
                Id    = request.TargetId?.Value
            };
        }

        return (T)Request;
    }
}