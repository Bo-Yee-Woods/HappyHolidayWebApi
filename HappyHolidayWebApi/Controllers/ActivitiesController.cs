using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HappyHolidayWebApi.Models.Repositories;
using SharedModels.ViewModels.Extensions;
using SharedModels.ViewModels;
using Microsoft.EntityFrameworkCore;
using HappyHolidayWebApi.Models.Entities;

namespace HappyHolidayWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Activities")]
    public class ActivitiesController : Controller
    {
        public readonly AbstractDbcontext db;

        public ActivitiesController(DefaultDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult GetActivities()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}", Name = "GetActivity")]
        public IActionResult GetActivity(int id)
        {
            var activity = db.Activities.Find(id);

            if (activity == null)
            {
                return NotFound("Activity Not Found");
            }

            return Ok(new JsonResult(activity.ToActivityVM()));
        }

        [HttpPost]
        public IActionResult CreateActivity([FromBody] ActivityVM activityVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var targetEvent = db.Events.Find(activityVM.EventId);

            if (targetEvent == null)
            {
                return NotFound("Event Not Found");
            }

            var proposer = db.Users.Find(activityVM.ProposerId);

            if (proposer == null)
            {
                return NotFound($"User {activityVM.ProposerId} Not Found");
            }

            var activity = new Activity()
            {
                Title = activityVM.Title,
                Description = activityVM.Description,
                StartDateTime = activityVM.StartDateTime,
                EndDateTime = activityVM.EndDateTime,
                BaseEvent = targetEvent,
                Proposer = proposer
            };

            db.Activities.Add(activity);

            db.SaveChanges();

            return CreatedAtRoute("GetActivity", new { id = activity.Id }, activity.ToActivityVM());
        }

        [HttpPut("{id}")]
        public IActionResult EditActivity([FromBody] ActivityVM activityVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var activity = db.Activities.Find(activityVM);

            if (activity == null)
            {
                return NotFound("Activity Not Found");
            }

            activity.Title = activity.Title;
            activity.StartDateTime = activityVM.StartDateTime;
            activity.EndDateTime = activityVM.EndDateTime;
            activity.Description = activityVM.Description;

            db.Activities.Update(activity);

            db.SaveChanges();

            return Ok("Activity Updated");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteActivity(int id)
        {
            var activity = db.Activities.Find(id);

            if (activity == null)
            {
                return NotFound("Activity Not Found");
            }

            db.Remove(activity);

            db.SaveChanges();

            return NoContent();
        }

        [HttpGet("{id}/Votes", Name = "GetVotes")]
        public IActionResult GetVotes(int id)
        {
            var votes = db.Votes.Where(v => v.ActivityId == id)
                                .Include(v => v.User)
                                .ToList();

            return Ok(new JsonResult(votes));
        }

        [HttpPost("{id}/Votes")]
        public IActionResult CreateVote(int id, [FromBody] VoteVM voteModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var voter = db.Users.Find(voteModel.UserId);

            if (voter == null)
            {
                return NotFound($"User {voteModel.Id} Not Found");
            }

            var activity = db.Activities.Find(voteModel.Id);

            if (activity == null)
            {
                return NotFound($"Activity {activity.Id} Not Found");
            }

            var newVote = new Vote()
            {
                IsNetural = voteModel.IsNetural,
                IsUpVote = voteModel.IsUpVote,
                User = voter,
                Activity = activity,
            };

            db.Votes.Add(newVote);

            db.SaveChanges();

            return CreatedAtRoute("GetVotes", id, newVote.ToVoteVM());
        }

        [HttpPut("{id}/Votes/{voteId}")]
        public IActionResult EditVote(int voteId, [FromBody] VoteVM voteModel)
        {
            var vote = db.Votes.Find(voteId);

            vote.IsNetural = voteModel.IsNetural;
            vote.IsUpVote = voteModel.IsUpVote;

            db.Votes.Update(vote);

            return Ok();
        }

        [HttpDelete("{id}/Votes/{voteId}")]
        public IActionResult DeleteVote(int voteId)
        {
            var vote = db.Votes.Find(voteId);

            if (vote == null)
            {
                return NotFound("Vote Not Found");
            }

            db.Votes.Remove(vote);

            db.SaveChanges();

            return NoContent();
        }

    }
}