namespace sk.Core.Models.EventModels
{
    public class CreateEventModel
    {
        public string NameOfEvent { get; set; }
        public string Description { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
