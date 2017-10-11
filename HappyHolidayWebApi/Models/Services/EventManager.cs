using HappyHolidayWebApi.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedModels.ViewModels;
using HappyHolidayWebApi.Models.Entities;

namespace HappyHolidayWebApi.Models.Services
{
    public class EventManager
    {
        public readonly AbstractDbcontext db;

        public EventManager(AbstractDbcontext db)
        {
            this.db = db;
        }

        public EventBase CreatEvent(EventVM eventModel, User owner)
        {
            var newEvent = new EventBase()
            {
                Name = eventModel.Name,
                TargetGroupSize = eventModel.TargetGroupSize ?? 0,
                Description = eventModel.Description,
                Owner = owner,
            };

            db.Events.Add(newEvent);

            db.SaveChanges();

            return newEvent;
        }
    }
}
