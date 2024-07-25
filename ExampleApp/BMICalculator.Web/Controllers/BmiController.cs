using BMICalculator.Model.DTO;
using BMICalculator.Model.Model;
using BMICalculator.Model.Repositories;
using BMICalculator.Services;
using BMICalculator.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BMICalculator.Web.Controllers
{
    [Route("[controller]")]
    public class BmiController : Controller
    {
        private readonly IBmiCalculatorFacade bmiCalculatorFacade;
        private readonly IResultRepository resultRepository;

        public BmiController(IBmiCalculatorFacade bmiCalculatorFacade, IResultRepository resultRepository)
        {
            this.bmiCalculatorFacade = bmiCalculatorFacade;
            this.resultRepository = resultRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            MeasurementListViewModel vm = new()
            {
                MeasurementList = resultRepository.GetAll().Where(x => x.UserId == Guid.Parse(userId)).ToList(),
            };

            return View(vm);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Measurement()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Measurement(MeasurementViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = bmiCalculatorFacade.GetResult(vm.Weight, vm.Height, vm.Unit);
                vm.Bmi = result.Bmi;
                vm.Summary = result.Summary;
                vm.BmiClassification = result.BmiClassification;
            }
            return View(vm);
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> SaveMes(MeasurementViewModel result)
        {
            BmiMeasurement model = new BmiMeasurement()
            {
                Bmi = result.Bmi,
                BmiClassification = result.BmiClassification,
                Summary = result.Summary,
                UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))
            };

            await resultRepository.SaveResultAsync(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
