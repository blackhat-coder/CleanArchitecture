using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Members;

public static class MemberMessages
{
    public const string FirstNameLength = "First Name should be less than 20";
    public const string FirstNameEmpty = "First Name cannot be empty";
    public const string LastNameLength = "Last Name should be less than 20";
    public const string LastNameEmpty = "last Name cannot be empty";
    public const string InvalidEmail = "Not a valid email address";
    public const string InvalidUniqueEmail = "This email is already taken";
}
