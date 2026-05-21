using EEG_Analysis_System.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EEG_Analysis_System.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<EEGSession> EEGSessions { get; set; }

        public DbSet<EEGSignal> EEGSignals { get; set; }

        public DbSet<AnalysisResult> AnalysisResults { get; set; }
    }
}