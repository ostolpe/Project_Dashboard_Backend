using Business.Dtos;
using Business.Factories;
using Business.Models;
using Data.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Business.Services
{
    public interface IProjectService
    {
        Task<bool> CreateProjectAsync(AddProjectForm projectForm);
        Task<bool> DeleteProjectAsync(string projectId);
        Task<Project?> GetProjectByIdAsync(string projectId);
        Task<IEnumerable<Project>?> GetProjectsAsync();
        Task<bool> UpdateProjectAsync(UpdateProjectForm projectForm);
    }

    public class ProjectService(IProjectRepository projectRepository, IMemoryCache cache) : IProjectService
    {
        private readonly IProjectRepository _projectRepository = projectRepository;
        private readonly IMemoryCache _cache = cache;
        private const string _cacheKey_All = "Project_All";

        public async Task<bool> CreateProjectAsync(AddProjectForm projectForm)
        {
            if (projectForm == null)
                return false;

            var entity = ProjectFactory.CreateProjectEntity(projectForm);

            var result = await _projectRepository.AddAsync(entity);
            if (result)
            {
                _cache.Remove(_cacheKey_All);
                return true;
            }

            return result;
        }

        public async Task<IEnumerable<Project>?> GetProjectsAsync()
        {
            if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<Project>? cachedItems))
                return cachedItems;

            var projects = await SetCache();
            return projects;
        }

        public async Task<Project?> GetProjectByIdAsync(string projectId)
        {
            var project = new Project();

            if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<Project>? cachedItems))
            {
                project = cachedItems?.FirstOrDefault(x => x.Id == projectId);
                if (project != null)
                    return project;
            }

            var entity = await _projectRepository.GetAsync(x => x.Id == projectId);
            if (entity == null)
                return null;

            await SetCache();

            project = ProjectFactory.CreateProject(entity);
            return project;
        }

        public async Task<bool> UpdateProjectAsync(UpdateProjectForm projectForm)
        {
            if (projectForm == null)
                return false;

            var project = await _projectRepository.GetAsync(x => x.Id == projectForm.Id);
            if (project == null)
                return false;

            ProjectFactory.UpdateProject(project, projectForm);

            var result = await _projectRepository.UpdateAsync(project);
            if (result)
                _cache.Remove(_cacheKey_All);

            return result;
        }

        public async Task<bool> DeleteProjectAsync(string projectId)
        {
            if (string.IsNullOrEmpty(projectId))
                return false;

            var result = await _projectRepository.DeleteAsync(x => x.Id == projectId);

            if (result)
                _cache.Remove(_cacheKey_All);

            return result;
        }

        public async Task<IEnumerable<Project>?> SetCache()
        {
            _cache.Remove(_cacheKey_All);
            var entities = await _projectRepository.GetAllAsync();
            var projects = entities.Select(ProjectFactory.CreateProject);

            projects = projects.OrderByDescending(x => x.Created);
            _cache.Set(_cacheKey_All, projects, TimeSpan.FromMinutes(10));

            return projects;
        }
    }
}
