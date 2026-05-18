namespace sk.Samples
{
    // --- Модели для Гражданина (Понятого) ---
    public class CitizenViewModel
    {
        public int MonthlyParticipationCount { get; set; }
        public List<NearbyRequest> NearbyRequests { get; set; } = new List<NearbyRequest>();
    }

    public class NearbyRequest
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public Coordinates Location { get; set; }

        public int DistanceMeter(Coordinates eventLocation)
        {
            const double EarthRadiusMeters = 6371000;

            // Convert latitude and longitude from degrees to radians
            double lat1 = Math.PI * this.Location.Latitude / 180.0;
            double lat2 = Math.PI * eventLocation.Latitude / 180.0;

            double deltaLat = Math.PI * (eventLocation.Latitude - this.Location.Latitude) / 180.0;
            double deltaLon = Math.PI * (eventLocation.Longitude - this.Location.Longitude) / 180.0;

            // Haversine formula
            double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            // Calculate final distance and round to the nearest integer meter
            double distance = EarthRadiusMeters * c;

            return (int)Math.Round(distance);
        }
    }

    // --- Модели для Следователя ---
    public class InvestigatorViewModel
    {
        public List<ActiveRequest> ActiveRequests { get; set; } = new List<ActiveRequest>();
    }

    public class ActiveRequest
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int RespondedCount { get; set; }
        public int RequiredCount { get; set; }
        public Coordinates Location { get; set; }
    }

    public struct Coordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}