// Ansvarlig: Abraham
namespace CirkusLuna.ClassLibrary.Models
{
    public class NewsPost
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; } = DateTime.Now;
        public bool IsPublished { get; set; } = true;
        public string? ImageUrl { get; set; }

        public string PublishedDateDisplay
        {
            get { return PublishedDate.ToString("dd. MMMM yyyy", new System.Globalization.CultureInfo("da-DK")); }
        }

        public string Summary
        {
            get
            {
                if (Content.Length > 200)
                    return Content.Substring(0, 200) + "...";
                return Content;
            }
        }
    }
}
