using Application.Response;
using Application.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("")]
public class BaseController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public ActionResult ApiResponse(AppResult response)
    {
        if (response.IsSuccess)
        {
            return Ok(new BaseResponse
            {
                message = response.message,
                status = AppConstants.Success,
                ServerTime = DateTime.UtcNow
            });
        }

        if (!response.IsSuccess)
        {
            return StatusCode(response.StatusCode, new BaseResponse
            {
                message = response.message,
                status = AppConstants.Failuer,
                ServerTime = DateTime.UtcNow
            });
        }

        if (response.IsException)
        {
            return StatusCode(500, new BaseResponse
            {
                message = response.message,
                status = AppConstants.Exception,
                ServerTime = DateTime.UtcNow
            });
        }

        return Ok(new BaseResponse
        {
            message = response.message,
            status = AppConstants.Success,
        });
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public ActionResult ApiResponse<T>(AppResult<T> response)
    {
        if (response.IsSuccess)
        {
            return Ok(new BaseResponse<T>
            {
                message = response.message,
                status = AppConstants.Success,
                ServerTime = DateTime.UtcNow,
                data = response.data
            });
        }

        if (!response.IsSuccess)
        {
            return StatusCode(response.StatusCode, new BaseResponse<List<string>>
            {
                message = response.message,
                status = AppConstants.Failuer,
                ServerTime = DateTime.UtcNow,
                data = response.ErrorList
            });
        }

        if (response.IsException)
        {
            return StatusCode(response.StatusCode, new BaseResponse<T>
            {
                message = response.message,
                status = AppConstants.Exception,
                ServerTime = DateTime.UtcNow,
                data = response.data
            });
        }

        return Ok(new BaseResponse
        {
            message = response.message,
            status = AppConstants.Success,
        });
    }
}
