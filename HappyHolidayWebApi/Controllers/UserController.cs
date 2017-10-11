using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HappyHolidayWebApi.Models.Repositories;
using SharedModels.ViewModels;
using SharedModels.ViewModels.Extensions;
using Microsoft.AspNetCore.JsonPatch;
using HappyHolidayWebApi.Models.Services;
using Microsoft.AspNetCore.Authorization;
using HappyHolidayWebApi.Models.Entities;
using System.Collections.Generic;

namespace HappyHolidayWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        public readonly AbstractDbcontext db;

        public UsersController(DefaultDbContext db)
        {
            this.db = db;
        }

        [HttpOptions]
        public IActionResult Documentation()
        {
            var options = new List<ApiOption>()
            {
                new ApiOption()
                {
                    Name = "Get Users",
                    Uri = "http://happy-holiday.azurewebsites.net/api/Users",
                    HttpMethod = HttpMethod.GET,
                    Inputs = null,
                    Responses = new List<Reponse>()
                    {
                        new Reponse()
                        {
                            Case = "Success",
                            StatusCode = 200,
                        },
                    },
                    Description = null,
                },
                new ApiOption()
                {
                    Name = "Get User",
                    Uri = "http://happy-holiday.azurewebsites.net/api/Users/{userId}",
                    HttpMethod = HttpMethod.GET,
                    Inputs = null,
                    Responses = new List<Reponse>()
                    {
                        new Reponse()
                        {
                            Case = "User Not Found",
                            StatusCode = 404,
                        },
                        new Reponse()
                        {
                            Case = "Success",
                            StatusCode = 200,
                        },
                    },
                    Description = null,
                },
                new ApiOption()
                {
                    Name = "Create User",
                    Uri = "http://happy-holiday.azurewebsites.net/api/Users",
                    HttpMethod = HttpMethod.POST,
                    Inputs = new List<Input>()
                    {
                        new Input()
                        {
                            Parameter = "NewUser",
                            Type = "UserVM",
                        },
                    },
                    Responses = new List<Reponse>()
                    {
                        new Reponse()
                        {
                            Case = "Invalid Input",
                            StatusCode = 400,
                        },
                        new Reponse
                        {
                            Case = "User Exists",
                            StatusCode = 409,
                        },
                        new Reponse
                        {
                            Case = "User Created",
                            StatusCode = 201,
                        }
                    },
                    Description = "Resource Location in Header",
                },
                new ApiOption()
                {
                    Name = "Update User",
                    Uri = "http://happy-holiday.azurewebsites.net/api/Users/{userId}",
                    HttpMethod = HttpMethod.PUT,
                    Inputs = new List<Input>()
                    {
                        new Input()
                        {
                            Parameter = "UserToBeUpdated",
                            Type = "UserVM",
                        },
                    },
                    Responses = new List<Reponse>()
                    {
                        new Reponse()
                        {
                            Case = "Invalid Input",
                            StatusCode = 400,
                        },
                        new Reponse()
                        {
                            Case = "User Not Found",
                            StatusCode = 404,
                        },
                        new Reponse()
                        {
                            Case = "User Updated",
                            StatusCode = 200,
                        },
                    },
                    Description = "Fully Update, Allow field [Password][DisplayAs][AvatarUrl]",
                },
                new ApiOption()
                {
                    Name = "Update User",
                    Uri = "http://happy-holiday.azurewebsites.net/api/Users/{userId}",
                    HttpMethod = HttpMethod.PATCH,
                    Inputs = new List<Input>()
                    {
                        new Input()
                        {
                            Parameter = "patchDocument",
                            Type = "JsonPatchDocument<UserVM>",
                        }
                    },
                    Responses = new List<Reponse>()
                    {
                        new Reponse()
                        {
                            Case = "Invalid Input",
                            StatusCode = 400,
                        },
                        new Reponse()
                        {
                            Case = "User Not Found",
                            StatusCode = 404,
                        },
                        new Reponse()
                        {
                            Case = "User Updated",
                            StatusCode = 200,
                        },
                    },
                    Description = "Partially Update User"
                },
                new ApiOption()
                {
                    Name = "Delete User",
                    Uri = "http://happy-holiday.azurewebsites.net/api/Users/{userId}",
                    HttpMethod = HttpMethod.DELETE,
                    Inputs = null,
                    Responses = new List<Reponse>()
                    {
                        new Reponse()
                        {
                            Case = "User Not Found",
                            StatusCode = 404,
                        },
                        new Reponse()
                        {
                            Case = "User Deleted",
                            StatusCode = 204,
                        },
                    },
                    Description = "Related Friendship Will be Cascade Deleted",
                },
                new ApiOption()
                {
                    Name = "Get User's Friends",
                    Uri = "http://happy-holiday.azurewebsites.net/api/Users/{userId}/Friends",
                    HttpMethod = HttpMethod.GET,
                    Inputs = null,
                    Responses = new List<Reponse>()
                    {
                        new Reponse()
                        {
                            Case = "Success",
                            StatusCode = 200,
                        },
                    },
                    Description = null,
                },
                new ApiOption()
                {
                    Name = "Create Friendship between user and friend",
                    Uri = "http://happy-holiday.azurewebsites.net/api/Users/{userId}/Friends/{friendId}",
                    HttpMethod = HttpMethod.POST,
                    Inputs = null,
                    Responses = new List<Reponse>()
                    {
                        new Reponse()
                        {
                            Case = "User Not Found",
                            StatusCode = 404,
                        },
                        new Reponse
                        {
                            Case = "Already Friends",
                            StatusCode = 409,
                        },
                        new Reponse
                        {
                            Case = "Friendship Created",
                            StatusCode = 201,
                        }
                    },
                    Description = null,
                },
                new ApiOption()
                {
                    Name = "Delete Friendship",
                    Uri = "http://happy-holiday.azurewebsites.net/api/Users/{userId}/Friends/{friendId}",
                    HttpMethod = HttpMethod.DELETE,
                    Inputs = null,
                    Responses = new List<Reponse>()
                    {
                        new Reponse()
                        {
                            Case = "User Not Found",
                            StatusCode = 404,
                        },
                        new Reponse()
                        {
                            Case = "Friendship Deleted",
                            StatusCode = 204,
                        },
                    },
                    Description = null,
                },
                new ApiOption()
                {
                    Name = "Get User's Own Events",
                    Uri = "http://happy-holiday.azurewebsites.net/api/Users/{userId}/OwnEvents",
                    HttpMethod = HttpMethod.GET,
                    Inputs = null,
                    Responses = new List<Reponse>()
                    {
                        new Reponse()
                        {
                            Case = "Success",
                            StatusCode = 200,
                        },
                    },
                    Description = null,
                },
                new ApiOption()
                {
                    Name = "Get User's Enrolling Events",
                    Uri = "http://happy-holiday.azurewebsites.net/api/Users/{userId}/EnrollEvents",
                    HttpMethod = HttpMethod.GET,
                    Inputs = null,
                    Responses = new List<Reponse>()
                    {
                        new Reponse()
                        {
                            Case = "Success",
                            StatusCode = 200,
                        },
                    },
                    Description = null,
                },
            };
            var samples = new List<object>()
            {
                new UserVM()
                {
                    Id = 0,
                    Email = "boyeewoods@gmail.com",
                    Password = "useless",
                    DisplayAs = "Baoyu",
                    AvatarUrl = "not yet",
                },
            };
            var documentation = new Documentation()
            {
                Title = "Happy Holiday User Api",
                Summary = "",
                ApiOptions = options,
                SampleModels = samples,
            };

            return Ok(new JsonResult(new { Documentation = documentation}));
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var userVMs = db.Users.Select(x => x.ToUserVM()).ToList();

            return Ok(new JsonResult(userVMs));
        }

        [HttpGet("({id})", Name = "GetUsersWithIds")]
        public IActionResult GetUsersByIds()
        {
            throw new NotSupportedException();
        }

        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get(int id)
        {
            var user = db.Users.Find(id);

            if (user == null) return NotFound();

            return Ok(new JsonResult(user.ToUserVM()));
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody]UserVM userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var usernameExists = db.Users.Where(u => u.Email == userModel.Email).Count() > 0;

            if (usernameExists)
            {
                return StatusCode(409, "Username Exists");
            }

            var user = userModel.ToUser();

            db.Users.Add(user);

            db.SaveChanges();

            return CreatedAtRoute("GetUser", new { id = user.Id }, user.ToUserVM());
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UserVM userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var user = db.Users.Find(id);

            if (user == null)
            {
                return NotFound("User Not Found");
            }

            user.Password = userModel.Password;
            user.DisplayAs = userModel.DisplayAs;
            user.AvatarUrl = userModel.AvatarUrl;

            db.Users.Update(user);

            db.SaveChanges();

            return StatusCode(200, "User Updated");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody]JsonPatchDocument<UserVM> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var selectedUser = db.Users.Find(id);

            if (selectedUser == null)
            {
                return NotFound("User Not Found");
            }

            var userModel = selectedUser.ToUserVM();

            patchDocument.ApplyTo(userModel);

            selectedUser = userModel.ToUser();

            db.Users.Update(selectedUser);

            db.SaveChanges();

            return StatusCode(200, "User Updated");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var targetUser = db.Users.Find(id);

            if (targetUser == null)
            {
                return NotFound("User Not Found");
            }

            var friendShips = db.FriendShips
                                .Where(f => f.SelfId == id || f.FriendId == id)
                                .ToList();

            db.RemoveRange(friendShips);

            db.Users.Remove(targetUser);

            db.SaveChanges();

            return NoContent();
        }

        [HttpGet("{id}/Friends", Name = "GetFriendships")]
        public IActionResult GetFriends(int id)
        {
            var userManager = new UserManager(db);

            var friends = userManager.GetUsersFriends(id);

            return Ok(new JsonResult(friends.Select(x => x.ToUserVM())));
        }

        [HttpPost("{id}/Friends/{friendId}")]
        public IActionResult CreateFriendShip(int id, int friendId)
        {
            if (id == friendId)
            {
                return BadRequest("UserIds cannot be the Same");
            }

            var self = db.Users.Find(id);
            if (self == null)
            {
                return NotFound($"User with Id {id} Not Found");
            }

            var friend = db.Users.Find(friendId);
            if (friend == null)
            {
                return NotFound($"User with Id {friendId} Not Found");
            }

            if (new UserManager(db).hasFriendshipBetween(id, friendId))
            {
                return StatusCode(409, $"User {id} and User {friendId} are friends already");
            }

            var friendship = new FriendShip()
            {
                Self = self,
                Friend = friend,
            };

            db.FriendShips.Add(friendship);

            db.SaveChanges();

            return CreatedAtRoute("GetFriendships", new { id = id }, $"User {id} and User {friendId} now are friends");
        }

        [HttpDelete("{id}/Friends/{friendId}")]
        public IActionResult RemoveFriendShip(int id, int friendId)
        {
            var friendships = db.FriendShips
                .Where(f => (f.SelfId == id && f.FriendId == friendId) || (f.FriendId == id && f.SelfId == friendId))
                .ToList();

            db.FriendShips.RemoveRange(friendships);

            db.SaveChanges();

            return NoContent();
        }

        [HttpGet("{id}/OwnEvents")]
        public IActionResult GetOwnEvents(int id)
        {
            var ownEvents = db.Events.Where(e => e.OwnerUserId == id).ToList();

            return Ok(new JsonResult(ownEvents));
        }

        [HttpGet("{id}/EnrollEvents")]
        public IActionResult GetEnrollEvents(int id)
        {
            var eventEnrolled = db.Enrollments.Where(e => e.UserId == id)
                                               .Select(e => e.EventEnrolled)
                                               .ToList();

            return Ok(new JsonResult(eventEnrolled));
        }


    }
}