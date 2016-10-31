using CSVOnlineEditor.Models;
using Microsoft.EntityFrameworkCore;

namespace CSVOnlineEditor.Data.DBContexts
{
    public class Storage: DbContext
    {
        public Storage(DbContextOptions<Storage> options)
            : base(options)
        {
        }

        public DbSet<Applicant> Applicants { get; set; }
    }
}
