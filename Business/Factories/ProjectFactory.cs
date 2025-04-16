using Business.Dtos;
using Data.Entities;

namespace Business.Factories
{
    public static class ProjectFactory
    {
        public static ProjectEntity CreateProjectEntity(AddProjectForm projectForm)
        {
            return new ProjectEntity
            {
                ImageUrl = projectForm.ImageUrl,
                Name = projectForm.Name,
                Description = projectForm.Description,
                Budget = projectForm.Budget,
                StartDate = projectForm.StartDate,
                EndDate = projectForm.EndDate,
                Created = DateTime.Now,
                UserId = projectForm.UserId,
                ClientId = projectForm.ClientId,
                StatusId = 1
            };
        }

        public static Project CreateProject(ProjectEntity projectEntity)
        {
            return new Project
            {
                Id = projectEntity.Id,
                ImageUrl = projectEntity.ImageUrl,
                Name = projectEntity.Name,
                Description = projectEntity.Description,
                StartDate = projectEntity.StartDate,
                EndDate = projectEntity.EndDate,
                Budget = projectEntity.Budget,
                Created = projectEntity.Created,
                Client = new Client
                {
                    Id = projectEntity.Client.Id,
                    Name = projectEntity.Client.Name
                },
                User = new User
                {
                    Id = projectEntity.User.Id,
                    FirstName = projectEntity.User.FirstName,
                    LastName = projectEntity.User.LastName,
                },
                Status = new Status
                {
                    Id = projectEntity.Status.Id,
                    Name = projectEntity.Status.Name
                }
            };
        }
    }
}