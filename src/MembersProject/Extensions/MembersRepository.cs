using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Umbraco.Core;
using Umbraco.Core.Persistence;

namespace MembersProject.Extensions
{
    public class MembersRepository
    {
        private readonly UmbracoDatabase _db;
        public MembersRepository()
        {
            _db = ApplicationContext.Current.DatabaseContext.Database; 
        }

        public List<CustomMember> GetMembers(int pageIndex, int pageSize, out int totalRecords)
        {
            totalRecords = _db.ExecuteScalar<int>("SELECT Count(*) FROM CustomMembers");

            var members = _db.SkipTake<CustomMember>(pageIndex * pageSize, pageSize, "SELECT * FROM CustomMembers");
            return members;
        }

        public CustomMember GetMemberById(Guid id)
        {
            var member = _db.Single<CustomMember>("SELECT TOP 1 * FROM CustomMembers WHERE Id = @0", id);
            return member;
        }

        public List<CustomMember> GetMembersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            totalRecords = _db.ExecuteScalar<int>("SELECT Count(*) FROM CustomMembers WHERE Email = @0", emailToMatch);

            var members = _db.SkipTake<CustomMember>(pageIndex * pageSize, pageSize, "SELECT * FROM CustomMembers WHERE Email = @0", emailToMatch);
            return members;
        }

        public CustomMember GetMemberByUsername(string usernameToMatch)
        {
            var member = _db.Single<CustomMember>("SELECT TOP 1 * FROM CustomMembers WHERE Username = @0", usernameToMatch);
            return member;
        }

        public List<CustomMember> GetMembersByUsername(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            totalRecords = _db.ExecuteScalar<int>("SELECT Count(*) FROM CustomMembers WHERE Username = @0", usernameToMatch);

            var members = _db.SkipTake<CustomMember>(pageIndex * pageSize, pageSize, "SELECT * FROM CustomMembers WHERE Username = @0", usernameToMatch);
            return members;
        }

        public void UpdateMember(Guid id, string email, string comment, bool isApproved)
        {
            _db.Execute("UPDATE CustomMembers SET Email = @0, IsApproved = @1, Comment = @2 WHERE Id = @3", email,
                isApproved, comment, id);
        }
    }
}