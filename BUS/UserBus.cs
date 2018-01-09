﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DAO;
using Entities;
namespace BUS
{
    public class UserBus
    {
        static UserBus instance;
        public static UserBus Instance
        {
            get
            {
                if (instance == null)
                    instance = new UserBus();
                return instance;
            }
        }
        public bool IsValidUser(string username, string password)
        {
            string sql = "select * from tb_user where username = @username, password = @password;";
            SqlParameter[] par = new SqlParameter[] {
                new SqlParameter("@username",username),
                new SqlParameter("@password",Hash.getHashSha256(password))
            };
            DataTable dt = DataConfig.Instance.GetTable(sql,par);
            return dt.Rows.Count > 0;
        }

        public bool HasUser(string username)
        {
            string sql = "select * from tb_user where username = @username";
            SqlParameter[] par = new SqlParameter[] {
                new SqlParameter("@username", username)
            };
            return DataConfig.Instance.GetTable(sql, par).Rows.Count > 0;
        }

        UserEntity GetUser(DataRow row)
        {
            if (row == null)
                return null;
            UserEntity user = new UserEntity();
            user.Username = row["username"].ToString();
            user.Firstname = row["firstname"].ToString();
            user.Lastname = row["lastname"].ToString();
            user.Email = row["email"].ToString();
            return user;
        }

        public UserEntity GetUser(string username, string password)
        {
            string sql = "select * from tb_user where username = @username and password = @password;";
            SqlParameter[] par = new SqlParameter[] {
                new SqlParameter("@username",username),
                new SqlParameter("@password",Hash.getHashSha256(password))
            };
            DataTable dt = DataConfig.Instance.GetTable(sql, par);
            if (dt.Rows.Count < 1)
                return null;
            DataRow row = dt.Rows[0];
            if (dt.Rows.Count == 0) return null;
            return GetUser(row);
        }

        public UserEntity GetUser(string username)
        {
            string sql = "select * from tb_user where username = @username ;";
            SqlParameter[] par = new SqlParameter[] {
                new SqlParameter("@username",username)
            };
            DataTable dt = DataConfig.Instance.GetTable(sql, par);
            if (dt.Rows.Count < 1)
                return null;
            DataRow row = dt.Rows[0];
            if (dt.Rows.Count == 0) return null;
            return GetUser(row);
        }

        public bool AddUser(UserEntity user)
        {
            string sql = "insert into tb_user values(@username, @password, @firstname, @lastname,@email);";
            SqlParameter[] par = new SqlParameter[] {
                new SqlParameter("@username",user.Username),
                new SqlParameter("@password",Hash.getHashSha256(user.Password)),
                new SqlParameter("@firstname",user.Firstname),
                new SqlParameter("@lastname",user.Lastname),
                new SqlParameter("@email",user.Email)
            };
            int n = DataConfig.Instance.ExecuteNonQuery(sql, par);
            return n == 1;
        }

        public bool DeleteUser(string username)
        {
            string sql = "delete from tb_user where username = @username;";
            SqlParameter[] par = new SqlParameter[]
            {
                new SqlParameter("@username",username)
            };
            return DataConfig.Instance.ExecuteNonQuery(sql, par) == 1;
        }

        public bool UpdateUser(UserEntity user)
        {
            string sql = "update tb_user set firstname = @firstname, lastname = @lastname, email = @email where username = @username";
            SqlParameter[] par = new SqlParameter[] {
                new SqlParameter("@username",user.Username),
                new SqlParameter("@firstname",user.Firstname),
                new SqlParameter("@lastname",user.Lastname),
                new SqlParameter("@email",user.Email)
            };
            return DataConfig.Instance.ExecuteNonQuery(sql, par) == 1;
        }

        public DataTable GetTable()
        {
            string sql = "select * from tb_user;";
            DataTable dt = DataConfig.Instance.GetTable(sql);
            return dt;
        }

        public List<UserEntity> GetUsers()
        {
            DataTable dt = GetTable();
            List<UserEntity> ls = new List<UserEntity>();
            foreach(DataRow row in dt.Rows)
            {
                ls.Add(GetUser(row));
            }
            return ls;
        }

        public string GetRole(string username)
        {
            return "";
        }

        public List<PermissionEntity> GetPermission(string username)
        {
            string sql = "select * from tb_permission_user where username=@username;";
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@username", username)
            };
            DataTable dt = DataConfig.Instance.GetTable(sql, pars);
            List<PermissionEntity> roles = new List<PermissionEntity>();
            foreach(DataRow row in dt.Rows)
            {
                var p = new PermissionEntity();
                p.ID = row["permissionid"].ToString();
                roles.Add(p);
            }
            return roles;
        }

        public string GetPermissionStr(string username)
        {
            var ls = GetPermission(username);
            var a = ls.Select(p => p.ID).ToArray();
            return String.Join(",", a);
        }
    }
}