using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudyHub.Domain.Models;
using StudyHub.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyHub.API.Helpers
{
    public class Gadget
    {
        private ApplicationDbContext dbContext;
        public IServiceProvider Services { get; }

        public Gadget()
        {
            dbContext = new ApplicationDbContext(
                  Services.GetRequiredService<
                     DbContextOptions<ApplicationDbContext>>());
        }
        public  void UpdateResponse (string refId, string fullresponse)
        {
            var model = new ResponseRequestModel()
            {
                RefId = refId,
                ResponseTime = DateTime.Now,
                FullResponse = fullresponse,
            };
            var response = dbContext.ResponseRequestModels.FirstOrDefault(x => x.RefId == refId);
            dbContext.ResponseRequestModels.Update(model);
            save();


        }

        public ApplicationDbContext GetContextExtention ()
        {
           return  new ApplicationDbContext(
                  Services.GetRequiredService<
                     DbContextOptions<ApplicationDbContext>>());
        }

        public void UpdateRequest(string refId, string fullresponse)
        {
            var model = new ResponseRequestModel()
            {
                RefId = refId,
                ResponseTime = DateTime.Now,
                FullResponse = fullresponse,
            };
            var request = dbContext.ResponseRequestModels.FirstOrDefault(x => x.RefId == refId);
            dbContext.ResponseRequestModels.Update(request);
            save();

        }

        public void save()
        {
            dbContext.SaveChanges();
        }
    }
}
