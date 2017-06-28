using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEnPA
{
    public class Globals
    {
        public static string authorizationKey = "";
        public static string accessToken = "";
        public static string userLogged = "";
        public static List<string> userRoles = new List<string>();
        public static List<string> userGroupRoles = new List<string>();
    }

    public class Utilities
    {
        public static string GenerateReferenceNumber()
        {
            return DateTime.Now.ToString("yMdHms");
        }
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }

    }
}
