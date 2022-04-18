using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;

namespace RestWithASPNETUdemy.Repository.Implementations
{
    public class BookRepositoyImplementation : IBookRepository
    {
        private MySQLContext _context;

        public BookRepositoyImplementation(MySQLContext context)
        {
            _context = context;
        }

        public Person Create(Person book)
        {
            try
            {
                _context.Add(book);
                _context.SaveChanges();

            } 
            catch (Exception ex)
            {
                throw ex;
            }
            return book;
        }

        public void Delete(long id)
        {
            var result = _context.Books.SingleOrDefault(x => x.Equals(id));
            if (result != null)
            {
                try
                {
                    _context.Books.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Exists(long id)
        {
            return _context.Books.Any(x => x.Equals(id));
        }

        public List<Person> FindAll()
        {
            return _context.Books.ToList();
        }

        public Person FindByID(long id)
        {
            return _context.Books.SingleOrDefault(_x => _x.Equals(id));
        }

        public Person Update(Person book)
        {
            if(!Exists(book.Id)) return null;

            var result = _context.Books.SingleOrDefault(b => b.Equals(book));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(book);
                    _context.SaveChanges();

                } catch (Exception ex)
                {
                    throw ex;
                }
            }

            return book;
        }
    }
}
