using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace ChatGPTCaller.Models
{
    public class RegisterResponse
    {
        public bool RegisterResult { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class LoginResponse
    {
        public bool LogInResult { get; set; }
        public int? UserID { get; set; }
        public string? UserRole { get; set; }
        public string ErrorMessage { get; set; }
        public LoginResponse() { }
    }

    public class User
    {
        public string email { get; set; }
        public string password { get; set; }
        public User() { }
        public User(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }

    public class UserInfo:User
    {
        public byte[] hashSalt { get; set; } = new byte[16];
        public UserInfo() { }
        public UserInfo(User user):base(user.email, user.password)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(hashSalt);
        }

        public UserInfo(string email, string password):base(email, password)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(hashSalt);
        }

        public string GetHashValue()
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password!,
                    salt: hashSalt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));
        }
    }
}
