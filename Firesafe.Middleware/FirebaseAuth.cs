namespace Application.Middlewares;

public interface IFirebaseAuth
{
    public Task<string?> SignInWithEmailAndPassword(string email, string password);
}