namespace BeerApplication
{
    public class Beer
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public decimal AlcoholPercentage { get; set; }

        public int Rating { get; set; }
    }
}
