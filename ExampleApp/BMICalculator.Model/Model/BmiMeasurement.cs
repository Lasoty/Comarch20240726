using BMICalculator.Model.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace BMICalculator.Model.Model
{
    public class BmiMeasurement
    {
        public BmiMeasurement()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
        }

        public Guid Id { get; set; }

        public double Bmi { get; set; }
        
        public BmiClassification BmiClassification { get; set; }

        public DateTime Date { get; set; }

        [MaxLength(100)]
        public string Summary { get; set; }

        public IdentityUser User { get; set; }

        public Guid UserId { get; set; }
    }
}
