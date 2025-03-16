namespace widebot.DTOs
{
    public class ArticlesUpdateDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Tags { get; set; }

    }
}
