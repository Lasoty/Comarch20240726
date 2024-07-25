using BMICalculator.Model.DTO;
using BMICalculator.Model.Model;

namespace BMICalculator.Web.Models
{
    public class MeasurementListViewModel
    {
        public ICollection<BmiMeasurement> MeasurementList { get; set; }
    }
}
