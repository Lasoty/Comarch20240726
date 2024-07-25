using BMICalculator.Model.Data;
using BMICalculator.Model.DTO;
using BMICalculator.Model.Model;
using System.Linq;
using System.Threading.Tasks;

namespace BMICalculator.Model.Repositories
{
    public interface IResultRepository
    {
        IQueryable<BmiMeasurement> GetAll();
        Task SaveResultAsync(BmiMeasurement result);
    }

    public class ResultRepository : IResultRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ResultRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<BmiMeasurement> GetAll() 
            => dbContext.BmiMeasurements;

        public async Task SaveResultAsync(BmiMeasurement result)
        {
            dbContext.BmiMeasurements.Add(result);
            await dbContext.SaveChangesAsync();
        }
    }
}
