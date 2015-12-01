using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MembersProject.Extensions
{
    public class CustomMember
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Comment { get; set; }
        public bool IsApproved { get; set; }
        public bool IsLockedOut { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime LastLockoutDate { get; set; }
        public bool IsFrontendDev { get; set; }
        public bool IsBackendDev { get; set; }
    }
}
