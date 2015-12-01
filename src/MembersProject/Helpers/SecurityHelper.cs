using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MembersProject.Helpers
{
    public static class SecurityHelper
    {
        private static readonly Random Rand = new Random();

        public static string GeneratePinNumber()
        {
            return Rand.Next(1000, 9999).ToString();
        }
    }
}