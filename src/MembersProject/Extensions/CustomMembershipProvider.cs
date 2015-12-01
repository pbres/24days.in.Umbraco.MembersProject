using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MembersProject.Extensions
{
    public class CustomMembershipProvider : MembershipProvider
    {
        private string _applicationName;
        private bool _enablePasswordReset;
        private bool _enablePasswordRetrieval;
        private bool _requiresQuestionAndAnswer;
        private bool _requiresUniqueEmail;
        private int _maxInvalidPasswordAttempts;
        private int _passwordAttemptWindow;
        private int _minRequiredNonAlphanumericCharacters;
        private int _minRequiredPasswordLength;
        private string _passwordStrengthRegularExpression;
        private MembershipPasswordFormat _passwordFormat;

        private readonly MembersRepository _membersRepository;

        public CustomMembershipProvider()
        {
            _membersRepository = new MembersRepository();
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            // 
            // Initialize values from web.config. 
            // 

            // Initialize the abstract base class. 
            base.Initialize(name, config);

            _applicationName = GetConfigValue(config["applicationName"],
                                            System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            _maxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            _passwordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
            _minRequiredNonAlphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredNonAlphanumericCharacters"], "1"));
            _minRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "7"));
            _passwordStrengthRegularExpression = Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], ""));
            _enablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
            _enablePasswordRetrieval = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "true"));
            _requiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "false"));
            _requiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "true"));

            string tempFormat = config["passwordFormat"];
            if (tempFormat == null)
            {
                tempFormat = "Hashed";
            }

            switch (tempFormat)
            {
                case "Hashed":
                    _passwordFormat = MembershipPasswordFormat.Hashed;
                    break;
                case "Encrypted":
                    _passwordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Clear":
                    _passwordFormat = MembershipPasswordFormat.Clear;
                    break;
                default:
                    throw new ProviderException("Password format not supported.");
            }

            // 
            // Initialize OdbcConnection. 
            //
        }

        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue))
                return defaultValue;

            return configValue;
        }

        public override string ApplicationName
        {
            get { return _applicationName; }
            set
            {
                _applicationName = string.IsNullOrWhiteSpace(value) ? "/" : value;
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { return _enablePasswordReset; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return _enablePasswordRetrieval; }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            var members = _membersRepository.GetMembersByEmail(emailToMatch, pageIndex, pageSize, out totalRecords);
            var membersCollection = new MembershipUserCollection();
            foreach (var customMember in members)
            {
                membersCollection.Add(ConvertToMembershipUser(customMember));
            }
            return membersCollection;
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            var members = _membersRepository.GetMembersByUsername(usernameToMatch, pageIndex, pageSize, out totalRecords);
            var membersCollection = new MembershipUserCollection();
            foreach (var customMember in members)
            {
                membersCollection.Add(ConvertToMembershipUser(customMember));
            }
            return membersCollection;
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {

            var members = _membersRepository.GetMembers(pageIndex, pageSize, out totalRecords);
            var membersCollection = new MembershipUserCollection();
            foreach (var customMember in members)
            {
                membersCollection.Add(ConvertToMembershipUser(customMember));
            }
            return membersCollection;

        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var member = _membersRepository.GetMemberByUsername(username);
            if (member != null)
            {
                return ConvertToMembershipUser(member);
            }
            return null;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            Guid id;
            if (Guid.TryParse(providerUserKey.ToString(), out id))
            {
                var member = _membersRepository.GetMemberById(id);
                if (member != null)
                {
                    return ConvertToMembershipUser(member);
                }
            }
            return null;
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return _maxInvalidPasswordAttempts; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return _minRequiredNonAlphanumericCharacters; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return _minRequiredPasswordLength; }
        }

        public override int PasswordAttemptWindow
        {
            get { return _passwordAttemptWindow; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return _passwordFormat; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return _passwordStrengthRegularExpression; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return _requiresQuestionAndAnswer; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return _requiresUniqueEmail; }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            if (user.ProviderUserKey != null)
                _membersRepository.UpdateMember((Guid)user.ProviderUserKey, user.Email, user.Comment, user.IsApproved);
        }

        public override bool ValidateUser(string username, string password)
        {
            var member = _membersRepository.GetMemberByUsername(username);
            if (member != null)
            {
                //This is only exmaple, avoid storing passwords in plain text :)
                return member.Password == password;
            }
            return false;
        }

        private MembershipUser ConvertToMembershipUser(CustomMember member)
        {
            return new MembershipUser("UmbracoMembershipProvider", member.Username, member.Id, member.Email, "", "", member.IsApproved, member.IsLockedOut, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.MinValue, DateTime.MinValue);
        }
    }
}