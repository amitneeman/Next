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

        public static bool isPermitted(dynamic currentUser, string creatorID, NextContext _context)
        {
            string currentUserName = currentUser.Identity.Name;
            if (currentUser.Identity.Name == null)
            {
                return false;
            }

            var user = _context.Users.SingleOrDefault(u => u.UserName == currentUserName);

            if (user != null && (isAdmin(currentUser, _context) || creatorID == user.Id))
            {
                return true;
            }

            return false;
        }
    }
}
