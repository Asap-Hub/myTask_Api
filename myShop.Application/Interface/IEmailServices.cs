namespace myShop.Infrastructure.Services
{
    public interface IEmailServices
    {
        Task sendEmailAsync(string from, string to, string subject, string body);
    }
}