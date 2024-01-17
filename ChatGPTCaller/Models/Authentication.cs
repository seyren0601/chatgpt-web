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
        public int mssv {  get; set; }
        public string full_name {  get; set; }
        public string gender { get; set; }
        public DateTime birthday { get; set; }
        public string email { get; set; }
        public string address {  get; set; }
        public string nationality {  get; set; }
        public string religion {  get; set; }
        public DateTime entry_date { get; set; }   
        public string faculty {  get; set; }
        public string major { get; set; }
        public string class_sv { get; set; }
        public string password { get; set; }

        public User() { }
        public User(int Mssv, string Full_name,string Gender,DateTime Birthday,string Email,
            string Address,string Nationality,string Religion,DateTime Entry_date,string Faculty,
            string Major,string Class_sv,string Password)
        {
            this.mssv = Mssv;
            this.full_name = Full_name;
            this.gender = Gender;
            this.birthday = Birthday;
            this.email = Email;
            this.address = Address;
            this.nationality = Nationality;
            this.religion = Religion;
            this.entry_date = Entry_date;
            this.faculty = Faculty;
            this.major = Major;
            this.class_sv = Class_sv;
            this.password = Password;
        }
    }

    public class UserInfo:User
    {
        public byte[] hashSalt { get; set; } = new byte[16];
        public UserInfo() { }
        public UserInfo(User user):base(user.mssv,user.full_name,user.gender,user.birthday,user.email,
            user.address,user.nationality,user.religion,user.entry_date,user.faculty,user.major,user.class_sv, user.password)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(hashSalt);
        }

        public UserInfo(int Mssv, string Full_name, string Gender, DateTime Birthday, string Email,
            string Address, string Nationality, string Religion, DateTime Entry_date, string Faculty,
            string Major, string Class_sv, string Password) :base(Mssv,Full_name, Gender, Birthday, Email,
            Address, Nationality, Religion, Entry_date, Faculty, Major, Class_sv, Password)
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
