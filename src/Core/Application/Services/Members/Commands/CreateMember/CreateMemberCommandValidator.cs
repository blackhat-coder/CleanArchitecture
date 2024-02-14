using Domain.Entities.Members;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Members.Commands.CreateMember;

public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
{
    private readonly IApplicationDbContext _context;
    public CreateMemberCommandValidator(IApplicationDbContext context)
    {
        RuleFor(x => x.FirstName).MaximumLength(20).WithMessage(MemberMessages.FirstNameLength);
        RuleFor(x => x.FirstName).NotEmpty().WithMessage(MemberMessages.FirstNameEmpty);
        RuleFor(x => x.LastName).MaximumLength(20).WithMessage(MemberMessages.LastNameLength);
        RuleFor(x => x.LastName).NotEmpty().WithMessage(MemberMessages.LastNameEmpty);
        RuleFor(x => x.Email).EmailAddress().WithMessage(MemberMessages.InvalidEmail);
        RuleFor(x => x.Email).MustAsync(ValidateUniqueEmail).WithMessage(MemberMessages.InvalidUniqueEmail);

        _context = context;
    }

    public async Task<bool> ValidateUniqueEmail(string email, CancellationToken cancellationToken)
    {
        var resp = await _context.Members.AnyAsync(x => x.Email == email, cancellationToken);
        return !resp;
    }
}
