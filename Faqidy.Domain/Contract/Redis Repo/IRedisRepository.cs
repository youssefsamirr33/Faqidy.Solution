using Faqidy.Domain.Entities.Otp;
namespace Faqidy.Domain.Contract.Redis_Repo
{
    public interface IRedisRepository
    {
        Task AddOrUpdateAsyn(string user_id , OtpPayload otpPayload , TimeSpan timeToLive);
        Task<OtpPayload> GetAsync(string user_id);
        Task RemoveAsync(string user_id);   
    }
}
