using System.Collections.Generic;
using Shop.Domain;
using System.Data;
using Shop.DAL;
using System.Linq;
using System;
using System.Data.Entity;
using Shop.Service.Cache;

namespace Shop.Service
{
    public class UserBLL : BLLBase
    {

        private ItblUserDAL _userDAL;

        public UserBLL(string connectionString = "") : base(connectionString)
        {
            _userDAL = new tblUserDAL(this.DatabaseFactory);
        }
        public UserModel GetUserByUserName(string Username)
        {
            var query = _userDAL.GetAll().Where(k => k.Username == Username).FirstOrDefault();
            var data = mapper.Map<tblUser, UserModel>(query);
            return data;
        }
        public void RegisterUser(UserModel data)
        {
            var user = mapper.Map<UserModel, tblUser>(data);
            _userDAL.Add(user);
            this.SaveChanges();
        }
        public int ValidateUser(string Username, string Password)
        {
            var query = _userDAL.GetAll().Where(e => e.Username == Username).Select(k => new { k.Status, k.Password }).FirstOrDefault();
            if (query == null) return -3;
            else if (query.Password != Password) return -2;
            else if (query.Status == 1) return 1;
            else return (int)query.Status;
        }

    }
}