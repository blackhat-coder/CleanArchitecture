using Application.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public class EmailService(ILogger<EmailService> _logger) : IEmailService
{
    public Task SendMailAsync(string reciever, string body)
    {
        _logger.LogInformation($"MAIL SENT TO {reciever}... {body}");
        return Task.CompletedTask;
    }
}
