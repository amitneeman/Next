using System.Linq;
using Next.Data;

namespace Next.Areas.Identity
{
    public class AuthHelper
    {
        public AuthHelper()
        {
            
        }

        public static bool isAdmin(dynamic currentUser, NextContext _context)
        {
            string currentUserName = currentUser.Identity.Name;
            if (currentUserName != null)
            {
                var user = _context.Users.SingleOrDefault(u => u.UserName == currentUserName || u.Email == currentUserName);
                if (user != null && user.isAdmin)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
