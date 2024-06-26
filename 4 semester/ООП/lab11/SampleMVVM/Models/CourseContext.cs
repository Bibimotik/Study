using System.Data.Entity;

namespace SampleMVVM.Models
{
    class CourseContext : DbContext
    {
        public DbSet<Course> Tickets { get; set; }

        public CourseContext() : base("name=CourseContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<CourseContext>());
        }
    }
}