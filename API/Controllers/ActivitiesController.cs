using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        // insert data context in order to call the Model queries directly
        private readonly DataContext _context;
        public ActivitiesController(DataContext context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task<ActionResult<List<Domain.Activity>>> GetActivities() // when specifying list we have to also indicate what type is in the list (Activity)
        {
            return await _context.Activities.ToListAsync();
        }

        [HttpGet("{id}")] // passing a parameter to controller action is done with "{param_name}"  maps to activities/5
        public async Task<ActionResult<Domain.Activity>> GetActivity(Guid id)
        {
            return await _context.Activities.FindAsync(id);
        }
    }
}