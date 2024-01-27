using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public sealed class Member : BaseEntity
{
    public Member(Guid id, string firstName, string lastName, string email)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}
