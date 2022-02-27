using System;

namespace Shop.Domain
{
    public class UserModel
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Address { get; set; }

        public Nullable<System.DateTime> CreateDate { get; set; }

        public Nullable<long> CreateBy { get; set; }

        public Nullable<int> Status { get; set; }

        public Nullable<int> UserType { get; set; }

        public string Roles { get; set; }

        public string Email { get; set; }
    }
}
