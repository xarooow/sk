using sk.Core.Interfaces;
using sk.Core.Models;
using sk.Core.Models.EventModels;
using sk.Core.Models.UserModels;

namespace sk.Infrastructure
{
    public class MockEventDB: IDataBase<EventModel>
    {
        private readonly List<EventModel> _events = new List<EventModel>();
        public async Task<bool> IsExist(EventModel eventModel)
        {
            bool flag = _events.Any(x => x.Id == eventModel.Id);

            return flag;
        }

        public async Task Save(EventModel eventModel)
        {
            if (eventModel.Id == 0 || eventModel.Id == null)
            {
                eventModel.Id = _events.Any() ? _events.Max(e => e.Id) + 1 : 1;
            }

            _events.Add(eventModel);

            foreach(var x in _events)
            {
                Console.WriteLine(x.NameOfEvent);
            }

            return;
        }

        public async Task Delete(EventModel eventModel)
        {
            var eventToDelete = _events.FirstOrDefault(x => x.Id == eventModel.Id);
            if (eventToDelete != null)
            {
                _events.Remove(eventToDelete);
            }
        }

        public async Task<List<EventModel>> GetList(int quant = default)
        {
            if (!(quant == default))
            {
                return _events.Take(quant).ToList();
            }
            else { return _events.ToList(); }
        }
    }
}
