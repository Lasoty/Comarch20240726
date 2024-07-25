using BMICalculator.Model.DTO;
using BMICalculator.Model.Repositories;
using BMICalculator.Services;
using BMICalculator.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using BMICalculator.Model.Data;
using BMICalculator.Model.Model;

namespace BMI.Calculator.Service.Integration.Tests
{
    public class BmiCalculatorFacadeTests
    {
        IResultRepository resultRepository;
        IBmiCalculatorFacade bmiCalculatorFacade;
        ApplicationDbContext dbContext;

        [SetUp]
        public void Setup()
        {


            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("BmiDb")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            resultRepository = new ResultRepository(dbContext);

            var bmiDeterminatorMock = new Mock<IBmiDeterminator>();
            var bmiCalculatorFactoryMock = new Mock<IBmiCalculatorFactory>();

            bmiCalculatorFacade =
                new BmiCalculatorFacade(bmiDeterminatorMock.Object, bmiCalculatorFactoryMock.Object, resultRepository);
        }

        [Test]
        public async Task SaveResultShouldSaveBmiRecordInDb()
        {
            Guid id = Guid.NewGuid();
            // Arrange
            BmiMeasurement bmi = new BmiMeasurement()
            {
                Id = id,
                Bmi = 15,
                BmiClassification = BmiClassification.Normal,
                Summary = "Jest OK"
            };

            // Act
            await bmiCalculatorFacade.SaveResult(bmi);

            // Assert
            Assert.IsTrue(dbContext.BmiMeasurements.Any(x => x.Id == bmi.Id && x.BmiClassification == BmiClassification.Normal));
        }
    }
}