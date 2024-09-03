namespace Course_Messenger.WEB.Models.Interfaces
{
    public interface IUserService
    {
        UserModel Get(string email, string password);

        UserModel Create(UserModel model);

        UserModel Update(UserModel model);

        void Delete(int id);
    }
}
