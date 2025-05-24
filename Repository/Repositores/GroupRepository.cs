using SocialMediaApp.Models;
using SocialMediaApp.Repository.Interfaces;
using System.Text.RegularExpressions;

namespace SocialMediaApp.Repository.Repositores
{
    public class GroupRepository : IGroupRepository
    {
        SocialContext context;
        public GroupRepository(SocialContext context)
        {
            this.context = context;
        }
        public void Add(Models.Group entity)
        {
            context.Groups.Add(entity);
        }

        public void Delete(long Id)
        {
            throw new NotImplementedException();
        }

        public List<Models.Group> GetAll(string includes = null)
        {
            throw new NotImplementedException();
        }

        public Models.Group GetById(long id)
        {
            return context.Groups.FirstOrDefault(i => i.Id == id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Models.Group entity)
        {
            throw new NotImplementedException();
        }
    }
}
