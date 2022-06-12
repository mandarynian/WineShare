using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WineDocumentation.Core.Domain
{
    public class User 
    {   
        private static readonly Regex NameRegex = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");

        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Username { get; protected set; }
        public string FullName { get; protected set; }
        public string Role { get; protected set; }
        public string Salt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        protected User()
        {
        }

        public User(Guid userId, string email, string username, string role, 
            string password, string salt)
        {
            Id = userId;
            SetEmail(email);
            SetUsername(username);
            SetRole(role);
            SetPassword(password, salt);
            CreatedAt = DateTime.UtcNow;
        }
        
        public void SetUsername(string username) 
        {
            //if(!NameRegex.IsMatch(username))
            //{
            //    throw new Exception("Username is invalid.");
            //}

            if (String.IsNullOrEmpty(username))
            {
                throw new Exception("Username is invalid.");
            }

            Username = username.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetEmail(string email) 
        {
            if (string.IsNullOrWhiteSpace(email)) 
            {
                throw new Exception("Email can not be empty.");
            }
            if (Email == email) 
            {
                return;
            }

            Email = email.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new Exception("Role can not be empty.");
            }
            if (Role == role)
            {
                return;
            }
            Role = role;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, string salt)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password can not be empty.");
            }
            //if (string.IsNullOrWhiteSpace(salt))
            //{
            //    throw new Exception("Salt can not be empty.");
            //}
            //if (password.Length < 4) 
            //{
            //    throw new Exception("Password must contain at least 4 characters.");
            //}
            //if (password.Length > 100) 
            //{
            //    throw new Exception("Password can not contain more than 100 characters.");
            //}
            if (Password == password)
            {
                return;
            }
            Password = password;
            Salt = salt;
            UpdatedAt = DateTime.UtcNow;
        }

        public async Task UpdateAsync(
            string email = "", 
            string username = "", 
            string password = "", 
            string role = "")
        {
            await Task.Run(() => {
                if (email != "")
                {
                    this.Email = email;
                }

                if (username != "")
                {
                    this.Username = username;
                }

                if (password != "")
                {
                    this.Password = password;
                }

                if (role != "")
                {
                    this.Role = role;
                }
            });
            
        }
    }
}