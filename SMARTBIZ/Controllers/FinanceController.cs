using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SmartBiz.Application.DTO;
using Microsoft.AspNetCore.Authorization;

namespace SmartBiz.Web.Controllers
{
    [Authorize]
    public class FinanceController : Controller
    {
        private readonly IFinancialRecordService _financialRecordRepository;

        public FinanceController(IFinancialRecordService financialRecordService)
        {
            _financialRecordRepository = financialRecordService;
        }

        public IActionResult Index()
        {
            
            var records = _financialRecordRepository.GetAllRecords().ToList();

            
            var sumByPurpose = records
                .GroupBy(r => r.Purpose)
                .Select(g => new
                {
                    Purpose = g.Key,
                    Total = g.First().Sum 
                })
                .ToList();

            ViewBag.SumByPurpose = sumByPurpose;

            return View(records);
        }

        [HttpPost]
        public IActionResult AddInline(FinanceDTO model)
        {
            _financialRecordRepository.AddFinancialRecord(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateInline(FinanceDTO model)
        {
            _financialRecordRepository.UpdateRecord(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _financialRecordRepository.DeleteRecord(id);
            return RedirectToAction("Index");
        }
    }
}
