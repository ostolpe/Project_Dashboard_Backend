using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories
{
    public static class ProjectFactory
    {
        public static ProjectEntity CreateProjectEntity(AddProjectForm projectForm, string? newImageUrl = null)
        {

            return new ProjectEntity
            {
                ImageUrl = newImageUrl,
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

        public static void UpdateProject(ProjectEntity entity, UpdateProjectForm form, string? newImageUrl = null)
        {
            if (entity.ImageUrl != newImageUrl)
                entity.ImageUrl = newImageUrl;

            if (entity.Name != form.Name)
                entity.Name = form.Name;

            if (entity.Description != form.Description)
                entity.Description = form.Description;

            if (entity.Budget != form.Budget)
                entity.Budget = form.Budget;

            if (entity.StartDate != form.StartDate)
                entity.StartDate = form.StartDate;

            if (entity.EndDate != form.EndDate)
                entity.EndDate = form.EndDate;

            if (entity.UserId != form.UserId)
                entity.UserId = form.UserId;

            if (entity.ClientId != form.ClientId)
                entity.ClientId = form.ClientId;

            if (entity.StatusId != form.StatusId)
                entity.StatusId = form.StatusId;
        }
    }
}