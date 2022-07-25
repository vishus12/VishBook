using System.Threading.Tasks;
using VishBook.Auth.Models;

namespace VishBook.Auth
{
    public interface IFirebaseAuthService
    {
        Task<FirebaseUser> Login(Credentials credentials);
        Task<FirebaseUser> Register(Registration registration);
    }
}