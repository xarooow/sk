using Microsoft.AspNetCore.Mvc;
using sk.Services;
using System.Linq;
using System.Collections.Generic;

namespace sk.Samples
{
    public class CitizensController : Controller
    {
        private readonly IDBService _dbService;

        public CitizensController(IDBService dbService)
        {
            _dbService = dbService;
        }

        public IActionResult Index(double? userLat, double? userLng)
        {
            // 1. Извлекаем данные из MockDB.
            var dbData = _dbService.GetAllEvents() as InvestigatorViewModel;

            // Координаты гражданина по умолчанию (если браузер еще не передал геопозицию)
            // Для теста поставим точку, относительно которой будем считать (например, рядом с MockDB)
            var citizenCoords = new Coordinates
            {
                Latitude = userLat ?? 120.5,
                Longitude = userLng ?? 88.0
            };

            // 2. Строим модель
            var model = new CitizenViewModel
            {
                // В реальном приложении этот счетчик тоже должен идти из БД для конкретного юзера
                MonthlyParticipationCount = 1,
                NearbyRequests = dbData?.ActiveRequests.Select(r => new NearbyRequest
                {
                    Id = r.Id,
                    Address = r.Address,
                    Location = r.Location // ФИКС: Обязательно переносим координаты из БД!
                }).ToList() ?? new List<NearbyRequest>()
            };

            // Передаем координаты текущего пользователя во View через ViewData, 
            // чтобы внутри HTML вызвать метод подсчета расстояния.
            ViewData["UserCoordinates"] = citizenCoords;

            // 3. Возвращаем представление
            return View("~/Samples/Citizen.cshtml", model);
        }

        [HttpPost]
        public IActionResult RespondToRequest(int requestId)
        {
            // Метод успешно отработает в MockDB и увеличит счетчик RespondedCount
            _dbService.SubscribeUser(requestId, "Тестовый Понятой");

            // Перенаправляем на Index, чтобы страница перерисовала обновленные данные
            return RedirectToAction("Index");
        }
    }
}