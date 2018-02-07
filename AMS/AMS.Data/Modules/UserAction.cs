using AMS.Data.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Data.Modules
{
    public class UserAction : BaseActions<User>
    {
        public override void Add(User t)
        {
            ctx.Users.Add(t);
            Save();
        }

        public override void Change(User t)
        {
            Save();
        }

        //Action Not in Use For Users 
        public override void Delete(int id)
        {
            ctx.Users.Where(x => x.UserId == id).Delete();
            Save();
        }

        //Action Not in User For Users User GetASP
        public override User Get(int id)
        {
            return ctx.Users.First(x => x.UserId == id);
        }

        public UserDto GetUserInfo(int userId)
        {
            var info = ctx.Users.Where(x => x.UserId == userId).First();
            UserDto user = new UserDto();
            user.UserId = userId;
            user.FirstName = info.FirstName;
            user.LastName = info.LastName;
            user.Password = info.Password;
            user.Email = info.Email;
            user.ProfileRating = GetRating(userId);
            return user;
        }

        private List<Rating> GetRating(int userId)
        {
            List<Rating> TotalCounts = new List<Rating>();

            Rating satisfied = new Rating();
            satisfied.CommentStatus = "satisfied";
            satisfied.Total = ctx.Comments.Where(x => x.CommentBy == userId && x.CommentStatus == "satisfied").Count();
            TotalCounts.Add(satisfied);

            Rating satisfied2 = new Rating();
            satisfied2.CommentStatus = "relevant";
            satisfied2.Total = ctx.Comments.Where(x => x.CommentBy == userId && x.CommentStatus == "relevant").Count();
            TotalCounts.Add(satisfied2);

            Rating satisfied3 = new Rating();
            satisfied3.CommentStatus = "irrelevant";
            satisfied3.Total = ctx.Comments.Where(x => x.CommentBy == userId && x.CommentStatus == "irrelevant").Count();
            TotalCounts.Add(satisfied3);

            return TotalCounts;
        }

        public string AccountStatus(UserDto user)
        {
            var Data = ctx.Users.Where(x => x.Email == user.UserName).FirstOrDefault();
            if (Data != null)
            {
                if (Data.Password == user.Password)
                {
                    if (Data.UserIsActive == true || Data.UserIsActive == null)
                    {
                        return "success";
                    }
                    else
                    {
                        return "Inactive";
                    }
                }
                else
                {
                    return "WrongPassword";
                }
            }
            else
            {
                return "NotFound";
            }
        }

        public void UpdateUserInfo(string firstName, string lastName, string email, int userId)
        {
            User user = ctx.Users.Where(x => x.UserId == userId).First();
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;
            Change(user);
        }

        public override IQueryable<User> GetAll()
        {
            return ctx.Users;
        }

        public override User GetAsp(string id)
        {
            return ctx.Users.First(x => x.UserId == int.Parse(id));
        }

        public int CreateUser(UserDto model)
        {
            if (model.UserId != 0)
            {
                User user = Get(model.UserId);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.UserName;
                user.Password = model.Password;
                Change(user);
            }
            else
            {
                User user = new User();
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.UserName;
                user.Password = model.Password;
                user.IsAdmin = false;
                user.UserIsActive = true;
                user.CreatedDate = System.DateTime.Now;
                Add(user);
                model.UserId = user.UserId;
                string path = HttpContext.Current.Server.MapPath("~/ProfileImages/");
                File.Copy(path + "common.jpg", path + model.UserId + ".jpg");
            }
            return model.UserId;
        }


        public User UserLogin(UserDto user)
        {
            User UserData = ctx.Users.Where(x => x.Email == user.UserName && x.Password == user.Password).FirstOrDefault();
            if (UserData != null)
            {
                return UserData;
            }
            else
            {
                return null;
            }

        }


        public bool ValidateEmail(string userName, int UserId)
        {
            bool IsAvailable = true;
            if (UserId == 0)
            {
                var data = ctx.Users.Where(x => x.Email == userName).FirstOrDefault();
                if (data != null)
                {
                    IsAvailable = false;
                }
            }
            else
            {
                var data = ctx.Users.Where(x => x.Email == userName && x.UserId != UserId).FirstOrDefault();
                if (data != null)
                {
                    IsAvailable = false;
                }
            }

            return IsAvailable;
        }

    }
}