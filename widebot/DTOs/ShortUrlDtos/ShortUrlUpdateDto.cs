

namespace widebot.DTOs
{
    public class ShortUrlUpdateDto
    {
        public int Id { get; set; }

        public string LongUrl { get; set; }

        public string ShortUrl1 { get; set; }

        public string ShortCode { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
