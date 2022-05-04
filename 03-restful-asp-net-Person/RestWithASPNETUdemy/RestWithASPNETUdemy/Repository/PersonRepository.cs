using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Repository.Generic;

namespace RestWithASPNETUdemy.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySQLContext context) : base (context) { }

        public Person Disable(long id)
        {
            if (!_context.Persons.Any(p => p.Id == id)) return null;
            
            var user = _context.Persons.SingleOrDefault(p => p.Id == id);
            if (user != null)
            {
                user.Enabled = false;
                try
                {
                    _context.Entry(user).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                } catch (Exception)
                {
                    throw;
                }
            }
            return user;
        }
    }
}
