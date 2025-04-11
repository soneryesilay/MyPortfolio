namespace MyPortfolio.DAL.Entities
{
    public class SiteSettings
    {
        public int SiteSettingsId { get; set; } // Genellikle tek bir ayar seti olacağı için ID=1 olacak
        public string? SiteTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeywords { get; set; }
        public string? PublicContactEmail { get; set; }
        public string? FooterText { get; set; }
        // İhtiyaca göre başka ayarlar eklenebilir
    }
}