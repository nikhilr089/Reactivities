using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        private readonly AppDbContext context;

        public ActivitiesController(AppDbContext appDbContext)
        {
            this.context = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            return await this.context.Activities.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivityDetail(string id)
        {
            var activity = await this.context.Activities.FindAsync(id);
            if (activity == null)
            {
                return BadRequest();
            }

            return activity;
        }
    }
}
