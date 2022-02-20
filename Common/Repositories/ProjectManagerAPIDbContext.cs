using Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.Repositories
{
    public class ProjectManagerAPIDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<ProjectToMember> ProjectToMembers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<AssignmentComment> AssignmentComments { get; set; }
        public DbSet<HoursLog> HoursLogs { get; set; }

        public ProjectManagerAPIDbContext()
        {
            this.Users = this.Set<User>();
            this.Projects = this.Set<Project>();
            this.Assignments = this.Set<Assignment>();
            this.ProjectToMembers = this.Set<ProjectToMember>();
            this.RefreshTokens = this.Set<RefreshToken>();
            this.AssignmentComments = this.Set<AssignmentComment>();
            this.HoursLogs = this.Set<HoursLog>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-5C79GI6\\SQLEXPRESS;Database=ProjectManagerDb;User Id=kolev;Password=kolevpass;")
                            .UseLazyLoadingProxies();
        }
    }
}
