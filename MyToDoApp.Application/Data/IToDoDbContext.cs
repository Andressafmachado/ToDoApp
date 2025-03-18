using AIHR.LMS.Domain.Models.Journey;
using Microsoft.EntityFrameworkCore;

namespace AIHR.LMS.Application.Data;

public interface IToDoDbContext
{
    DbSet<CategoryEntity> Categories { get; }
    
    DbSet<ContentEntity> Contents { get; }
    
    DbSet<LessonEntity> Lessons { get; }
    
    DbSet<RatingEntity> Ratings { get; }
    
    DbSet<LabelEntity> Labels { get; }
    
    DbSet<JourneyEntity> Journeys { get; }
    
    DbSet<Module> Modules { get; }
    
    DbSet<Course> Courses { get; }
    
    DbSet<CertificateProgram> CertificatePrograms { get; }
    
    DbSet<LearningProgressEntity> LearningProgresses { get; }
    
    DbSet<BadgeEntity> Badges { get; }   
    
    DbSet<AchievementEntity> Achievements { get; }
    
    DbSet<DashboardEntity> Dashboards { get; }    
    
    DbSet<WeeklyProgressEntity> WeeklyProgresses { get; } 

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}