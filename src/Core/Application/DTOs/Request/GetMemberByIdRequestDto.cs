using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Request;

public class GetMemberByIdRequestDto
{
    public int PageSize { get; set; } = 10;
    public int PageNum { get; set; } = 0;
}
