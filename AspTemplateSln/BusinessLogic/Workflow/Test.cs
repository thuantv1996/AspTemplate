using DataAccess.Contexts;
using DataAccess.Models.Identity;

namespace BusinessLogic.Workflow
{
    public class Test
    {
        private readonly WebTemplateContext _context;
        public Test(WebTemplateContext context)
        {
            _context = context;
        }


        public void Add(string username, string password)
        {
            _context.Users.Add(new User
            {
                Username = username,
                PasswordHash = password
            });
            _context.SaveChanges();
        }

    }
}
