using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Common;

public class BaseQueryDto : BaseDto
{
    public int pageSize { get; set; } = 10;
    public int pageNum { get; set; } = 0;
}
