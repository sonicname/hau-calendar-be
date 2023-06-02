using server.Models;
using server.Repository.Interface;

namespace server.Repository;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _databaseContext;

    public UserRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public ICollection<UserModel> GetUsers()
    {
        return _databaseContext.Users.ToList();
    }

    public UserModel? GetUserByID(int id)
    {
        return _databaseContext.Users.FirstOrDefault(user => user.UserID == id);
    }

    public bool UserIsExists(string username)
    {
        return _databaseContext.Users.Any(user => user.UserName == username);
    }

    public void AddUserToDb(UserAuthParams authParams)
    {
        var newUser = new UserModel()
        {
            UserName = authParams.UserName,
            UserPassword = authParams.UserPassword
        };

        _databaseContext.Users.Add(newUser);
        _databaseContext.SaveChanges();
    }
}