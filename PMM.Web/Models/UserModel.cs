using System;
using System.ComponentModel.DataAnnotations;

namespace PMM.Web.Models
{
    public class UserModel:BaseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? LastLoggedIn { get; set; }
    }
}