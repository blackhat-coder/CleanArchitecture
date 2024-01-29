using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared;

public class Result
{
    public string message { get; set; }
    public bool IsException { get; set; } = false;
    public bool IsSuccess { get; set; } = false;
    public ErrorDetails Error { get; set; }

    public static Result Success(string message)
    {
        return new Result { message = message, IsSuccess = true };
    }

    public static Result Fail(string message, ErrorDetails error)
    {
        return new Result { message = message, Error = error };
    }

    public static Result Exception(string message)
    {
        return new Result { message = message, IsException = true };
    }

    public static async Task<Result> SuccessAsync(string message)
    {
        return await Task.FromResult(new Result { message = message, IsSuccess = true });
    }

    public static async Task<Result> FailAsync(string message, ErrorDetails error)
    {
        return await Task.FromResult(new Result { message = message, Error = error });
    }

    public static async Task<Result> ExceptionAsync(string message)
    {
        return await Task.FromResult(new Result { message = message, IsException = true });
    }
}


public class Result<T>
{
#nullable disable
    public string message { get; set; }
    public bool IsException { get; set; } = false;
    public T data { get; set; }
    public bool IsSuccess { get; set; } = false;
    public ErrorDetails Error { get; set; }


    public static Result<T> Success(string message, T data)
    {
        return new Result<T> { message = message, IsSuccess = true, data = data };
    }

    public static Result<T> Fail(string message, ErrorDetails error)
    {
        return new Result<T> { message = message, Error = error };
    }

    public static Result<T> Exception(string message)
    {
        return new Result<T> { message = message, IsException = true };
    }

    public static async Task<Result<T>> SuccessAsync(string message, T data)
    {
        return await Task.FromResult(new Result<T> { message = message, data = data, IsSuccess = true });
    }

    public static async Task<Result<T>> FailAsync(string message, ErrorDetails error)
    {
        return await Task.FromResult(new Result<T> { message = message, Error = error });
    }

    public static async Task<Result<T>> ExceptionAsync(string message)
    {
        return await Task.FromResult(new Result<T> { message = message, IsException = true });
    }
}
