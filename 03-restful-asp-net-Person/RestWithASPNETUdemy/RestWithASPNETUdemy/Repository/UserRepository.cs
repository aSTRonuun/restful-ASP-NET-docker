using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using System.Security.Cryptography;
using System.Text;

namespace RestWithASPNETUdemy.Repository;

public class UserRepository : IUseRepository
{
    private readonly MySQLContext _context;

    public UserRepository(MySQLContext context)
    {
        _context = context;
    }

    public User ValidateCredentials(UserVO user)
    {
        var pass = ComputeHash(user.Password, SHA256.Create());
        var info = _context.Users.FirstOrDefault(u => u.UserName == user.UserName && u.Password == pass);

        return info;
    }

    private string ComputeHash(string input, SHA256 algorithm)
    {
        Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
        return BitConverter.ToString(hashedBytes);
    }

    public User ValidateCredentials(string userName)
    {
        return _context.Users.SingleOrDefault(u => u.UserName == userName);
    }

    public User RefreshUserInfo(User user)
    {
        if (_context.Users.Any(u => user.Id.Equals(user.Id))) return null;

        var result = _context.Users.SingleOrDefault(p => p.Id == user.Id);
        Console.WriteLine(result);
        if (result != null)
        {
            try
            {
                _context.Entry(result).CurrentValues.SetValues(user);
                _context.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        return result;
    }
}
