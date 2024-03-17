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
        public string full_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public User() { }
        public User(string full_name, string email, string password)
        {
            this.full_name = full_name;
            this.email = email;
            this.password = password;
        }
    }

    public class UserInfo:User
    {
        public byte[] hashSalt { get; set; } = new byte[16];
        public int mssv { get; set; }
        public string gender { get; set; }
        public DateTime birthday { get; set; }
        public string faculty { get; set; }
        public string major { get; set; }
        public string nationality { get; set; }
        public string religion { get; set; }
        public int idcard {  get; set; }
        public DateTime dateofissue { get; set; }
        public string placeofissue { get; set; }
        public int myphone {  get; set; }
        public int parentphone {  get; set; }
        public string address { get; set; }
        public string aboutstudent { get; set; }

        public UserInfo() { }

        public UserInfo(User user):base(user.full_name, user.email, user.password)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(hashSalt);
        }
        public UserInfo(User user,byte[] hashSalt, int mssv, string gender, DateTime birthday, string faculty, string major, string nationality, string religion, int idcard, DateTime dateofissue, string placeofissue, int myphone, int parentphone, string address, string aboutstudent) : base(user.full_name, user.email, user.password)
        {
            this.hashSalt = hashSalt;
            this.mssv = mssv;
            this.gender = gender;
            this.birthday = birthday;
            this.faculty = faculty;
            this.major = major;
            this.nationality = nationality;
            this.religion = religion;
            this.idcard = idcard;
            this.dateofissue = dateofissue;
            this.placeofissue = placeofissue;
            this.myphone = myphone;
            this.parentphone = parentphone;
            this.address = address;
            this.aboutstudent = aboutstudent;
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
