using Microsoft.EntityFrameworkCore;
using sk.Samples;
using System.Collections.Generic;
using System.Linq;

namespace sk.Services
{
    public interface IDBService
    {
        object GetAllEvents();
        bool SubscribeUser(int eventId, string username);
    }

    public class PostgreSQLService : IDBService
    {
        public object GetAllEvents()
        {
            return null;
        }

        public bool SubscribeUser(int eventId, string username)
        {
            return false;
        }
    }

    public class MockDB : IDBService
    {
        private InvestigatorViewModel investigatorModel;
        private CitizenViewModel citizenModel;

        public MockDB()
        {
            investigatorModel = new InvestigatorViewModel
            {
                ActiveRequests = new List<ActiveRequest>
                {
                    new ActiveRequest {
                        Id = 101,
                        Address = "ул. Ленина, д. 15",
                        RequiredCount = 2,
                        RespondedCount = 1,
                        Location = new Coordinates{ Latitude = 120, Longitude = 90}
                    },
                    new ActiveRequest { 
                        Id = 102,
                        Address = "пр. Мира, д. 42",
                        RequiredCount = 2, 
                        RespondedCount = 0,
                        Location = new Coordinates{ Latitude = 121, Longitude = 86}
                    }
                }
            };

            citizenModel = new CitizenViewModel
            {
                MonthlyParticipationCount = 2,
                NearbyRequests =  new List<NearbyRequest>
                {
                    new NearbyRequest
                    {
                        Id = 1,
                        Address = "ул. Ленина, д. 12",
                        Location = new Coordinates{ Latitude = 1, Longitude = 6}
                    },

                    new NearbyRequest
                    {
                        Id = 2,
                        Address = "ул. Болдинаская, д. 28А",
                        Location = new Coordinates { Latitude =2, Longitude = 6}
                    }
                }
            };
        }

        public object GetAllEvents()
        {
            return investigatorModel;
        }

        public bool SubscribeUser(int eventId, string username)
        {
            var request = investigatorModel.ActiveRequests.FirstOrDefault(r => r.Id == eventId);

            if (request != null && request.RespondedCount < request.RequiredCount)
            {
                request.RespondedCount++;
                return true;
            }

            return false;
        }
    }
}