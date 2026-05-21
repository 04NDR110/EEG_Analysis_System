using Microsoft.AspNetCore.Mvc;
using EEG_Analysis_System.Models;
using EEG_Analysis_System.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;

namespace EEG_Analysis_System.Controllers
{
    public class MonitorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MonitorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Головна сторінка моніторингу
        public IActionResult Index()
        {
            return View();
        }

        // 2. Збереження результатів (викликається з JavaScript автоматично)
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> SaveResult([FromBody] AnalysisResult data)
        {
            if (data == null)
            {
                return BadRequest(new { success = false, message = "JSON data is null" });
            }

            try
            {
                var entity = new AnalysisResult
                {
                    ResultType = data.ResultType ?? "Unknown",
                    Confidence = data.Confidence,
                    CreatedAt = DateTime.Now,
                    // Тут ми записуємо потрібний текст для колонки Notatka
                    Notes = "Stan zarejestrowany przez system WebNN"
                };

                _context.AnalysisResults.Add(entity);
                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DB ERROR: {ex.Message}");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }
        

        // 3. Архів з фільтрацією (Пункт 5 вимог)
        public async Task<IActionResult> History(string searchType, DateTime? searchDate)
        {
            var query = _context.AnalysisResults.AsQueryable();

            if (!string.IsNullOrEmpty(searchType))
            {
                query = query.Where(r => r.ResultType == searchType);
            }

            if (searchDate.HasValue)
            {
                query = query.Where(r => r.CreatedAt.Date == searchDate.Value.Date);
            }

            var results = await query
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();

            ViewBag.SelectedType = searchType;
            ViewBag.SelectedDate = searchDate?.ToString("yyyy-MM-dd");

            return View(results);
        }

        // 4. Очищення історії
        [HttpPost]
        public async Task<IActionResult> ClearHistory()
        {
            var allResults = await _context.AnalysisResults.ToListAsync();
            _context.AnalysisResults.RemoveRange(allResults);
            await _context.SaveChangesAsync();
            return RedirectToAction("History");
        }

        // 5. Експорт CSV
        public IActionResult ExportCSV()
        {
            var data = _context.AnalysisResults.OrderByDescending(a => a.CreatedAt).ToList();
            var csv = new StringBuilder();
            csv.AppendLine("Data;Stan;Pewnosc;Notatki");

            foreach (var item in data)
            {
                csv.AppendLine($"{item.CreatedAt:dd.MM.yyyy HH:mm:ss};{item.ResultType};{item.Confidence:P1};{item.Notes}");
            }

            var encoding = new UTF8Encoding(true);
            return File(encoding.GetPreamble().Concat(encoding.GetBytes(csv.ToString())).ToArray(),
                        "text/csv", "EEG_Analysis_Report.csv");
        }

        // 6. Експорт JSON
        public IActionResult ExportJSON()
        {
            var data = _context.AnalysisResults.OrderByDescending(a => a.CreatedAt).ToList();
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            return File(Encoding.UTF8.GetBytes(json), "application/json", "EEG_Report.json");
        }

        // 7. Експорт PDF (через HTML)
        public IActionResult ExportPDF()
        {
            var data = _context.AnalysisResults.OrderByDescending(a => a.CreatedAt).ToList();
            var html = new StringBuilder();
            html.Append("<html><head><meta charset='utf-8'><style>table{width:100%;border-collapse:collapse;} th,td{border:1px solid black;padding:8px;}</style></head><body>");
            html.Append("<h2>Raport Analizy EEG</h2><table><tr><th>Data</th><th>Stan</th><th>Pewnosc</th></tr>");

            foreach (var item in data)
            {
                html.Append($"<tr><td>{item.CreatedAt:dd.MM.yyyy HH:mm:ss}</td><td>{item.ResultType}</td><td>{item.Confidence:P1}</td></tr>");
            }

            html.Append("</table></body></html>");
            return File(Encoding.UTF8.GetBytes(html.ToString()), "text/html", "EEG_Report.html");
        }
    }
}