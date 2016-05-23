using FailingVersionColumn.Models;

namespace FailingVersionColumn
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new DbFactory();
            using (var db = factory.GetDatabase())
            {
                // Here we will fail on version column if we use ([VersionColumn(VersionColumnType.Number)] on the VersionTest entity
                var list = db.Query<VersionTest>().ToList();
            }
        }
    }
}
