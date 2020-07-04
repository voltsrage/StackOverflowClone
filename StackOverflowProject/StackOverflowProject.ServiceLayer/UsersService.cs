using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModels;
using StackOverflowProject.Repositories;
using StackOverflowProject.ViewModels;
using AutoMapper;
using AutoMapper.Configuration;

namespace StackOverflowProject.ServiceLayer
{
    public interface IUsersService
    {
        int InsertUser(RegisterViewModel uvm);
        void UpdateUser(EditUserDetailsViewModel uvm);
        void UpdatePassword(EditUserPasswordViewModel uvm);
        void DeleteUser(int UserID);

        List<UserViewModel> GetUsers();
        UserViewModel GetUsersByEmailAndPasswords(string Email, string Password);
        UserViewModel GetUsersByEmail(string Email);
        UserViewModel GetUsersByUserID(int UserID);
    }

    public class UsersService:IUsersService
    {
        IUsersRepository ur;

        public UsersService()
        {
            ur = new UsersRepository();
        }

        public int InsertUser(RegisterViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<RegisterViewModel, User>(); cfg.IgnoreUnmapped(); });

            IMapper mapper = config.CreateMapper();

            User u = mapper.Map<RegisterViewModel, User>(uvm);

            u.PasswordHash = SHA256Generator.GenerateHash(uvm.Password);

            ur.InsertUser(u);

            int uid = ur.GetLatestUserID();

            return uid;
        }

        public void UpdateUser(EditUserDetailsViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditUserDetailsViewModel, User>(); cfg.IgnoreUnmapped(); });

            IMapper mapper = config.CreateMapper();

            User u = mapper.Map<EditUserDetailsViewModel, User>(uvm);

            ur.UpdateUser(u);
        }

        public void UpdatePassword(EditUserPasswordViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditUserPasswordViewModel, User>(); cfg.IgnoreUnmapped(); });

            IMapper mapper = config.CreateMapper();

            User u = mapper.Map<EditUserPasswordViewModel, User>(uvm);

            u.PasswordHash = SHA256Generator.GenerateHash(uvm.Password);

            ur.UpdatePassword(u);
        }

        public void DeleteUser(int UserID)
        {
            ur.DeleteUser(UserID);
        }

        public List<UserViewModel> GetUsers()
        {
            List<User> u = ur.GetUsers();

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });

            IMapper mapper = config.CreateMapper();

            List<UserViewModel> uvm = mapper.Map<List<User>, List<UserViewModel>>(u);

            return uvm;
        }

        public UserViewModel GetUsersByEmailAndPasswords(string Email, string Password)
        {
            User u = ur.GetUsersByEmailAndPasswords(Email, SHA256Generator.GenerateHash(Password)).FirstOrDefault();

            UserViewModel uvm = null;
            if (u != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });

                IMapper mapper = config.CreateMapper();

                uvm = mapper.Map<User, UserViewModel>(u);

                return uvm;
            }
            return uvm;
        }

        public UserViewModel GetUsersByEmail(string Email)
        {
            User u = ur.GetUsersByEmail(Email).FirstOrDefault();

            UserViewModel uvm = null;
            if (u != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });

                IMapper mapper = config.CreateMapper();

                uvm = mapper.Map<User, UserViewModel>(u);

                return uvm;
            }
            return uvm;
        }

        public UserViewModel GetUsersByUserID(int UserID)
        {
            User u = ur.GetUsersByUserID(UserID).FirstOrDefault();

            UserViewModel uvm = null;
            if (u != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });

                IMapper mapper = config.CreateMapper();

                uvm = mapper.Map<User, UserViewModel>(u);

                return uvm;
            }
            return uvm;
        }
    }
}
