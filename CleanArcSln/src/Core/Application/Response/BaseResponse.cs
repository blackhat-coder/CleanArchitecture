using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response;

#nullable disable
public class BaseResponse
{
    public string status { get; set; }
    public string message { get; set; }
    public DateTime ServerTime { get; set; }
}

public class BaseResponse<T>
{
    public string status { get; set; }
    public string message { get; set; }
    public DateTime ServerTime { get; set; }
    public T data { get; set; }
}