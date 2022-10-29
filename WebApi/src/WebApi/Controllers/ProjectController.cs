using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IRepository<Project> repository;

        public ProjectController(ILogger<ProjectController> logger, IRepository<Project> repository)
        {
            _logger = logger;
            this.repository = repository;
        }

        [HttpGet]
        public Project Get(int id)
        {
            return repository.Find(id);
        }
    }
}