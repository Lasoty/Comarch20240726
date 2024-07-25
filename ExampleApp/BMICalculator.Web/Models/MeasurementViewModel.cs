using BMICalculator.Model.DTO;
using BMICalculator.Services.Enums;
using System.ComponentModel.DataAnnotations;

namespace BMICalculator.Web.Models
{
    public class MeasurementViewModel
    {
        [Range(1, 400)]
        public double Weight { get; set; }

        [Range(1, 250)]
        public double Height { get; set; }

        public UnitSystem Unit { get; set; }

        public double Bmi { get; set; }
        public BmiClassification BmiClassification { get; set; }
        public string? Summary { get; set; }
    }
}
