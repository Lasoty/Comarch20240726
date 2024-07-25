using BMICalculator.Model.DTO;
using BMICalculator.Model.Repositories;
using BMICalculator.Services.Enums;
using BMICalculator.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace BMICalculator.Services.Tests
{
    public class Tests
    {
        Mock<IBmiDeterminator> bmiDeterminatorMock;
        Mock<IBmiCalculatorFactory> calculatorFactoryMock;

        IBmiCalculatorFacade bmiCalculatorFacade;

        [SetUp]
        public void Setup()
        {
            bmiDeterminatorMock = new Mock<IBmiDeterminator>();
            IBmiDeterminator bmiDeterminator = bmiDeterminatorMock.Object;

            calculatorFactoryMock = new Mock<IBmiCalculatorFactory>();
            IBmiCalculatorFactory calculatorFactory = calculatorFactoryMock.Object;

            var resultRepositoryMock = new Mock<IResultRepository>();

            bmiCalculatorFacade = new BmiCalculatorFacade(bmiDeterminator, calculatorFactory, resultRepositoryMock.Object);

        }

        [Test]
        public void GetResultShouldCallCreateCalculatorOnlyOnes()
        {
            calculatorFactoryMock.Setup(x => x.CreateCalculator(It.IsAny<UnitSystem>()))
                .Returns(new MetricBmiCalculator());
            var result = bmiCalculatorFacade.GetResult(100, 200, UnitSystem.Metric);
            calculatorFactoryMock.Verify(x => x.CreateCalculator(It.IsAny<UnitSystem>()), Times.Once());
        }

        [Test]
        public void GetResultShouldResultUnderweightWhenWeightIsToLow()
        {
            // Arrange
            calculatorFactoryMock.Setup(m => m.CreateCalculator(It.IsAny<UnitSystem>()))
                .Returns(new MetricBmiCalculator());

            // Act
            var actual = bmiCalculatorFacade.GetResult(50, 200, UnitSystem.Metric);

            // Assert
            Assert.NotNull(actual);
            Assert.AreEqual(BmiClassification.Underweight, actual.BmiClassification);
        }
    }
}