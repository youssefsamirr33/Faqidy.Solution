using Faqidy.Domain.Contract.Redis_Repo;
using Faqidy.Domain.Entities.Otp;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Faqidy.Infrastructure.Redis_Repository
{
    public class RedisRepository : IRedisRepository
    {
        private readonly IDatabase _database;
        public RedisRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task AddOrUpdateAsyn(string user_id, object otpPayload, TimeSpan timeToLive)
        {
            var json = JsonSerializer.Serialize(otpPayload);
            await _database.StringSetAsync(user_id, json , timeToLive);
            
        }

        public async Task<OtpPayload> GetAsync(string user_id)
        {
            var json = await _database.StringGetAsync(user_id);
            if (json.IsNullOrEmpty) return null!;
            return JsonSerializer.Deserialize<OtpPayload>(json!)!;
        }

        public async Task RemoveAsync(string user_id)
            => await _database.KeyDeleteAsync(user_id);
    }
}
