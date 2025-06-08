using UserAuthAPI.Models;

namespace UserAuthAPI.Services
{
    public static class UserService
    {
        public static List<User> Users = new();
        public static Dictionary<string, string> Tokens = new(); // email -> token

        public static bool IsValidEmail(string email) =>
            email.Contains("@") && email.Contains(".");

        public static string GenerateToken(string email) =>
            Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 16);
    }
}
