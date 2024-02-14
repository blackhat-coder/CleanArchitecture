using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utils;

public static class ValidationResultConverter
{
    public static List<string> TransformToList(this ValidationResult result)
    {
        var list = new List<string>();
        foreach (var error in result.Errors)
        {
            list.Add(error.ErrorMessage);
        }

        return list;
    }
}
