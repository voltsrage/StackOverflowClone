using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface IUsersRepository
    {
        void InsertUser(User u);
        void UpdateUser(User u);
        void UpdatePassword(User u);
        void DeleteUser(int UserID);

        List<User> GetUsers();
        List<User> GetUsersByEmailAndPasswords(string Email, string Password);
        List<User> GetUsersByEmail(string Email);
        List<User> GetUsersByUserID(int UserID);
        int GetLatestUserID();
    }

    public class UsersRepository : IUsersRepository
    {
        StackOverflowDBContext db = new StackOverflowDBContext();

        public void DeleteUser(int UserID)
        {
            User userToDelete = db.Users.Where(u => u.UserID == UserID).FirstOrDefault();
            if(userToDelete != null)
            {
                db.Users.Remove(userToDelete);
                db.SaveChanges();
            }            
        }

        public int GetLatestUserID()
        {
            int uid = db.Users.Select(u => u.UserID).Max();
            return uid;
        }

        public List<User> GetUsers()
        {
            List<User> Users = db.Users.Where(u => u.IsAdmin.Equals(false)).OrderBy(u => u.Name).ToList();
            return Users;
        }

        public List<User> GetUsersByEmail(string Email)
        {
            List<User> Users = db.Users.Where(u => u.Email == Email).OrderBy(u => u.Name).ToList();
            return Users;
        }

        public List<User> GetUsersByEmailAndPasswords(string Email, string Password)
        {
            List<User> Users = db.Users.Where(u => u.Email == Email && u.PasswordHash == Password).OrderBy(u => u.Name).ToList();
            return Users;
        }

        public List<User> GetUsersByUserID(int UserID)
        {
            List<User> Users = db.Users.Where(u => u.UserID == UserID).ToList();
            return Users;
        }

        public void InsertUser(User u)
        {
            db.Users.Add(u);
            db.SaveChanges();
        }

        public void UpdatePassword(User u)
        {
            User userToUpdate = db.Users.Where(temp => temp.UserID == u.UserID).FirstOrDefault();
            if (userToUpdate != null)
            {
                userToUpdate.PasswordHash = u.PasswordHash;
                db.SaveChanges();
            }
        }

        public void UpdateUser(User u)
        {
            User userToUpdate = db.Users.Where(temp => temp.UserID == u.UserID).FirstOrDefault();
            if(userToUpdate!=null)
            {
                userToUpdate.Name = u.Name;
                userToUpdate.Mobile = u.Mobile;
                db.SaveChanges();
            }
        }
    }
}
