using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace ShortnerUrl.Models
{
    [Collection("UrlTags")]
    public class UrlTag
    {
        public ObjectId Id { get; set; }
        public string ShortenCode { get; set; }
        public string DestinationUrl { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
