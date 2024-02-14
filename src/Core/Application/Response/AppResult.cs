using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response;

public class AppResult : Result
{
    public int StatusCode { get; set; }

    public static new async Task<AppResult> FailAsync(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return await Task.FromResult(new AppResult { message = message, StatusCode = (int)statusCode });
    }

    public static new async Task<AppResult> SuccessAsync(string message)
    {
        return await Task.FromResult(new AppResult { message = message, IsSuccess = true });
    }

    public static new async Task<AppResult> ExceptionAsync(string message)
    {
        return await Task.FromResult(new AppResult { message = message, IsException = true, StatusCode = 500 });
    }
}

public class AppResult<T> : Result<T>
{
    public int StatusCode { get; set; }
    public List<string> ErrorList { get; set; }

    public static async Task<AppResult<T>> FailAsync(string message, List<string> error, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return await Task.FromResult(new AppResult<T> { message = message, ErrorList = error, StatusCode = (int)statusCode });
    }
    public static new async Task<AppResult<T>> SuccessAsync(string message, T data)
    {
        return await Task.FromResult(new AppResult<T> { message = message, data = data, IsSuccess = true });
    }

    public static new async Task<AppResult<T>> ExceptionAsync(string message)
    {
        return await Task.FromResult(new AppResult<T> { message = message, IsException = true, StatusCode = 500 });
    }
}

