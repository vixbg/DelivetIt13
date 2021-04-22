using DeliverIt13.Data;
using Microsoft.EntityFrameworkCore;

namespace DeliverIt13.Tests
{
    public class Utils
    {
        public static DbContextOptions<DeliverItContext> GetOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<DeliverItContext>().
                UseInMemoryDatabase(databaseName).
                Options;
        }
    }
}
