namespace Filmotech.Entities
{
    public class Films
    {
        public Guid? Id{ get; set; }

        public string? Titre { get; set; }

        public string? Qualité { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }




    }
}
