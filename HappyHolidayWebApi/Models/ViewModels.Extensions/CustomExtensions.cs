using HappyHolidayWebApi.Models.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedModels.ViewModels.Extensions
{
    public static class CustomExtensions
    {
        public static IEnumerable<string> GetErrorMessages(this ModelStateDictionary modelState)
        {
            var ErrorMessage = string.Empty;

            foreach (var stateValue in modelState.Values)
            {
                foreach (var error in stateValue.Errors)
                {                    
                    yield return error.ErrorMessage;
                }
            }            
        }        

        public static User ToUser(this UserVM model)
        {
            return new User()
            {
                Id = model.Id,
                Email = model.Email,
                Password = model.Password,
                DisplayAs = model.DisplayAs,
                AvatarUrl = model.AvatarUrl
            };
        }

        public static UserVM ToUserVM(this User user)
        {
            return new UserVM()
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                DisplayAs = user.DisplayAs,
                AvatarUrl = user.AvatarUrl,
            };
        }

        public static EventVM ToEventVM(this EventBase baseEvent)
        {
            return new EventVM()
            {
                Id = baseEvent.Id,
                Name = baseEvent.Name,
                TargetGroupSize = baseEvent.TargetGroupSize,
                Description = baseEvent.Description,
                StartDate = baseEvent.StartDate,
                EndDate = baseEvent.EndDate,
                Status = baseEvent.Status,
                CreatedAt = baseEvent.CreatedAt,
                OwnerUserId = baseEvent.OwnerUserId
            };
        }

        public static ActivityVM ToActivityVM(this Activity activity)
        {
            return new ActivityVM()
            {
                Id = activity.Id,
                Title = activity.Title,
                StartDateTime = activity.StartDateTime,
                EndDateTime = activity.EndDateTime,
                Description = activity.Description,
                EventId = activity.BaseEventId,
                ProposerId = activity.ProposerId,
            };
        }

        public static VoteVM ToVoteVM(this Vote vote)
        {
            return new VoteVM()
            {
                Id = vote.Id,
                IsNetural = vote.IsNetural,
                IsUpVote = vote.IsUpVote,                
                UserId = vote.UserId,
                UserDisplayAs = vote.User.DisplayAs,
            };
        }


    }
}
