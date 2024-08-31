using Microsoft.EntityFrameworkCore;
using ShortnerUrl.Infrastructures;
using ShortnerUrl.Models;
using System.Security.Cryptography;
using System.Text;

namespace ShortnerUrl.Services
{
    public class ShortenService(ShortnerUrlDbContext dbContext)
    {
        private readonly ShortnerUrlDbContext _dbContext = dbContext;
        public async Task<string> ShortenUrlAsync(string longUrl ,CancellationToken cancellationToken)
        {
            var url =await _dbContext.urlTags.FirstOrDefaultAsync(x => x.DestinationUrl == longUrl,cancellationToken);
            if (url is not null)
                return url.ShortenCode;

            var urlTag = new UrlTag
            { 
                CreatedOn = DateTime.UtcNow,
                DestinationUrl = longUrl,
                ShortenCode = await GenerateShortenCode(longUrl),
            };

            _dbContext.urlTags.Add(urlTag);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return urlTag.ShortenCode;
        }

        private async Task<string> GenerateShortenCode(string longUrl)
        {
            using MD5 mD5 = MD5.Create();

            byte[] hashBytes = mD5.ComputeHash(Encoding.UTF8.GetBytes(longUrl));
            string hashCode = BitConverter.ToString(hashBytes)
                                          .Replace(oldValue: "_", newValue: "")
                                          .ToLower();

            for (int i = 0; i < 7; i++)
            {
                string candidateCode = hashCode.Substring(i, 7);

                if (await _dbContext.urlTags.AnyAsync(x => x.ShortenCode == candidateCode))
                    continue;

                return candidateCode;
            }

            throw new Exception("Invalid Shorten code generate");
        }
    }
}
