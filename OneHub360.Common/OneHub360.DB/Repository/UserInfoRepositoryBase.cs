using NHibernate;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace OneHub360.DB
{
    /// <summary>
    /// User Logged In Event Delegate
    /// </summary>
    public delegate void UserLoggedInEventHandler(object sender, NHEntityEventArgs e); 
    public abstract class UserInfoRepositoryBase<T> : NHEntityRepository<T> where T:IUserInfo,INHEntity
    {
        #region UserInfoRepositoryBase Events
        /// <summary>
        /// After User logged in the system
        /// </summary>
        public event UserLoggedInEventHandler UserLoggedIn;
        protected virtual void OnUserLoggedIn(NHEntityEventArgs e)
        {
            if (UserLoggedIn != null)
                UserLoggedIn(this, e);
        }
        #endregion

        #region UserInfoRepositoryBase Constructors
        public UserInfoRepositoryBase(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public UserInfoRepositoryBase(ISession session) : base(session)
        {
        }
        #endregion

        #region UserInfoRepositoryBase Public Methods
        public virtual bool Login(string loginName, string password)
        {
            // TODO: as Enhancement Salt Value can be changed everytime the user is logged in
            bool validPassword = this.ValidatePassword(loginName, password);
            return validPassword;
        }
        public virtual bool ResetPassword(T user,string oldPassword, string newPassword)
        {
            // Check the Old Password
            if (this.ValidatePassword(user, oldPassword))
            {
                // Chnage the Old Password by the New one
                IUserInfo userInfo = this.ChangePassword(user, newPassword);
                using (ITransaction transaction = Session.BeginTransaction())
                {
                    Session.Update(userInfo);
                    transaction.Commit();
                }
                return true;
            }
            else
                return false;
        }
        #endregion

        #region UserInfoRepositoryBase Private Methods
        protected void SaltAndHashPassword(T userInfo)
        {
            // Generate New Salt Value and Update the UserInfo
            userInfo.PasswordSalt = this.GetNewSaltValue();

            // Hash the Password using the salt value and Update the UserInfo
            userInfo.Password = this.GetHashedPassword(userInfo.Password, userInfo.PasswordSalt);
        }
        protected bool ValidatePassword(T userInfo, string password)
        {
            // Get the Salt Value and calculate the hash value for the passed password
            string passwordHash = this.GetHashedPassword(password, userInfo.PasswordSalt);

            // Check if saved and generated password hashes are equal
            if (userInfo.Password == passwordHash)
                return true;
            else
                return false;
        }
        protected bool ValidatePassword(string loginName, string password)
        {
            // Build the Query to get the UserInfo Object by LoginName
            string query = string.Format("from {0} where IsDeleted = false AND LoginName='{1}'", typeof(T), loginName);

            // Execute the HQL and get the UserInfo Object
            T userInfo = Session.CreateQuery(query).UniqueResult<T>();

            return this.ValidatePassword(userInfo, password);
        }
        protected T ChangePassword(T user, string password)
        {
            // Generate New Salt Value and Update the UserInfo
            user.PasswordSalt = this.GetNewSaltValue();

            // Hash the Password using the salt value and Update the UserInfo
            user.Password = this.GetHashedPassword(password, user.PasswordSalt);

            // Return Updated UserInfo Object
            return user;
        }
        protected string GetNewSaltValue()
        {
            RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[64];
            rngCryptoServiceProvider.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }
        protected string GetHashedPassword(string password, string salt)
        {
            SHA512Cng algorithm = new SHA512Cng();
            MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(password + salt));

            string hashValue = Convert.ToBase64String(algorithm.ComputeHash(memoryStream));
            return hashValue.Substring(0, hashValue.Length - 2);
        }
        #endregion

        #region NHEntityRepository<T> Virtual Methods Overrides
        protected override void OnEntityInserting(NHEntityEventArgs e)
        {
            this.SaltAndHashPassword((T)e.Entity);

            base.OnEntityInserting(e);
        }

        protected override void OnEntityUpdating(NHEntityEventArgs e)
        {
            this.SaltAndHashPassword((T)e.Entity);

            base.OnEntityUpdating(e);
        }
        #endregion
    }
}
