using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MembersProject.Extensions
{
    public class CustomRoleProvider : RoleProvider
    {
        private readonly MembersRepository _membersRepository;

        public CustomRoleProvider()
        {
            _membersRepository = new MembersRepository();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {

        }

        public override string ApplicationName
        {
            get { return "/"; }
            set
            {

            }
        }

        public override void CreateRole(string roleName)
        {

        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            return false;
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            return new string[] { "Frontend Devs", "Backend Devs" };
        }

        public override string[] GetRolesForUser(string username)
        {
            var member = _membersRepository.GetMemberByUsername(username);
            var memberRoles = new List<string>();
            if (member != null)
            {
                if (member.IsBackendDev)
                {
                    memberRoles.Add("Backend Devs");
                }
                if (member.IsFrontendDev)
                {
                    memberRoles.Add("Frontend Devs");
                }
            }
            return memberRoles.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var member = _membersRepository.GetMemberByUsername(username);
            switch (roleName)
            {
                case "Backend Devs":
                    return member.IsBackendDev;
                case "Frontend Devs":
                    return member.IsFrontendDev;
            }
            return false;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}