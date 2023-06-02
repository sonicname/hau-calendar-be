using server.Models;

namespace server.Repository.Interface;

public interface IUserRepository
{
    ICollection<UserModel> GetUsers();

    UserModel? GetUserByID(int id);

    bool UserIsExists(string username);

    void AddUserToDb(UserAuthParams signUpParams);
}