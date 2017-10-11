using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SharedModels.ViewModels;
using SharedModels.ViewModels.Extensions;
using Microsoft.AspNetCore.JsonPatch;
using HappyHolidayWebApi.Models.Repositories;
using HappyHolidayWebApi.Models.Services;
using HappyHolidayWebApi.Models.Entities;

namespace HappyHolidayWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Events")]
    public class EventsController : Controller
    {
        public readonly AbstractDbcontext db;

        public EventsController(DefaultDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult GetEvents()
        {
            var events = db.Events.ToList();

            return Ok(new JsonResult(events.Select(x => x.ToEventVM())));
        }

        [HttpGet("{id}", Name = "GetEvent")]
        public IActionResult Get(int id)
        {
            var targetEvent = db.Events.Find(id);

            if (targetEvent == null)
            {
                return NotFound();
            }

            return Ok(new JsonResult(targetEvent.ToEventVM()));
        }

        [HttpPost]
        public IActionResult Post([FromBody]EventVM eventModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                var eventOwner = db.Users.Find(eventModel.Id);
                if (eventOwner == null)
                {
                    return BadRequest("Invalid User Id");
                }

                var eventManager = new EventManager(db);

                var newEvent = eventManager.CreatEvent(eventModel, eventOwner);

                return CreatedAtRoute("GetEvent", new { id = newEvent.Id }, newEvent.ToEventVM());
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]EventVM eventVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                var targetEvent = db.Events.Find(id);

                if (targetEvent == null)
                {
                    return NotFound("Event Not Found");
                }

                targetEvent.Name = eventVM.Name;
                targetEvent.Description = eventVM.Description;
                targetEvent.TargetGroupSize = eventVM.TargetGroupSize ?? 0;

                db.Update(targetEvent);

                db.SaveChanges();

                return Ok("Event Update");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody]JsonPatchDocument<EventVM> patchDocument)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var targetEvent = db.Events.Find(id);

            if (targetEvent == null)
            {
                return NotFound("Event Not Found");
            }

            db.Events.Remove(targetEvent);

            db.SaveChanges();

            return NoContent();
        }

        [HttpGet("{id}/Activities")]
        public IActionResult GetActivities(int id)
        {
            var activities = db.Activities.Where(a => a.BaseEventId == id).ToList();

            return Ok(new JsonResult(activities.Select(a => a.ToActivityVM())));
        }

        [HttpGet("{id}/Enrollments", Name = "GetEnrolledUsers")]
        public IActionResult GetEnrolledUsers(int id)
        {
            var usersEnrolled = db.Enrollments
                                  .Where(e => e.EventId == id)
                                  .Select(e => e.UserEnrolled)
                                  .ToList();

            return Ok(new JsonResult(usersEnrolled.Select(x => x.ToUserVM())));
        }

        [HttpPost("{id}/Enrollments/{userId}")]
        public IActionResult EnrollUser(int id, int userId)
        {
            // var _id = claim => id

            var targetEvent = db.Events.Find(id);

            if (targetEvent == null)
            {
                return NotFound("Target Event Not Found");
            }

            var targetUser = db.Users.Find(userId);

            if (targetUser == null)
            {
                return NotFound("Target User Not Found");
            }

            var newEnrollment = new Enrollment()
            {
                UserEnrolled = targetUser,
                EventEnrolled = targetEvent,
            };

            db.Enrollments.Add(newEnrollment);

            db.SaveChanges();

            return CreatedAtRoute("GetEnrolledUsers", new { id = targetEvent.Id }, $"User {targetUser.DisplayAs} is Enrolled in Event {targetEvent.Name}");
        }

        [HttpDelete("{id}/Enrollments/{userId}")]
        public IActionResult DisenrollUser(int id, int userId)
        {
            var targetEvent = db.Events.Find(id);

            if (targetEvent == null)
            {
                return NotFound("Target Event Not Found");
            }

            var enrollment = db.Enrollments
                               .Where(e => e.EventId == id && e.UserId == userId)
                               .SingleOrDefault();

            if (enrollment == null)
            {
                return NotFound("User Not Found in the Enrollment List");
            }

            db.Enrollments.Remove(enrollment);

            db.SaveChanges();

            return NoContent();
        }



    }
}