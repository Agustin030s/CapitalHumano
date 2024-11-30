using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq.Expressions;
using System.Reflection;

namespace Persistance.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTimeService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTimeService) : base(options) 
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTimeService = dateTimeService;
        }

        public DbSet<Departament> departaments { get; set; }
        public DbSet<Position> positions { get; set; }
        public DbSet<Domain.Entities.Tasks> tasks { get; set; }
        public DbSet<Skill> skills { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<PositionHistory> positionHistories { get; set; }
        public DbSet<PositionSkill> positionSkills { get; set; }
        public DbSet<EmployeeSkill> employeesSkill { get; set; }
        public DbSet<Salary> salaries { get; set; }
        public DbSet<Training> training { get; set; }
        public DbSet<EmployeeTraining> EmployeeTraining { get; set; }
        public DbSet<PerformanceEvaluation> performanceEvaluation { get; set; }
        public DbSet<EmployeePerformanceEvaluation> employeePerformanceEvaluations { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach(var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTimeService.NowUtc;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastUpdated = _dateTimeService.NowUtc;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.Deleted = _dateTimeService.NowUtc;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(AuditableBaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(GetDeletedFilter(entityType.ClrType));
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        private static LambdaExpression GetDeletedFilter(Type entityType)
        {
            var parameter = Expression.Parameter(entityType, "entity");
            var property = Expression.Property(parameter, "Deleted");
            var condition = Expression.Equal(property, Expression.Constant(null, typeof(DateTime?)));
            return Expression.Lambda(condition, parameter);
        }
    }
}
