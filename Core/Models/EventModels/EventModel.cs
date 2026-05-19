using Microsoft.EntityFrameworkCore;
using sk.Core.Models.UserModels;
using NetTopologySuite.Geometries;
using sk.Core.Interfaces;

namespace sk.Core.Models.EventModels
{
    [Owned]
    public class Location 
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
    public class EventModel: IModel
    {
        public int Id { get; set; }
        public string? NameOfEvent { get; set; }
        public string? Description { get; set; }

        public Point? Location { get; set; }
        public List<User>? Users { get; set; }
    }
}
