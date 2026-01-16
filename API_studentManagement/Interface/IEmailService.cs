using API_studentManagement.Models;
using AutoMapper.Internal;

namespace API_studentManagement.Interface
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailrequest);
    }
}
