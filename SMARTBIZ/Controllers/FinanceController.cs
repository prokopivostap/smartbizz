using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
            return View(_financialRecordRepository.GetAllRecords());
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
