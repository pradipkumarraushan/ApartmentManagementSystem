using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ApartmentManagementSystem.Models
{
    
    public class LoginModel
    {
        [Required(ErrorMessage ="email is Required")]
        [EmailAddress(ErrorMessage ="not in format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "password is Required")]
        //[Range(4,84,ErrorMessage ="range should be in 6 to 18")]
        public string Password { get; set; }
        public bool Remember_me { get; set; }
        public string role { get; set; }
        public string Message { get; set; }
        public string Cell_no { get; set; }
    }
    public static class Role
    {
        public const string ASSOC_MANAGER = "Association Manager";
        public const string ASSOC_MEMBER = "Association Member";
        public const string ASSOC_HEAD = "Association Head";
        public const string OWNER = "Owner";
        public const string TENANT = "Tenant";
        public const string SECURITY = "Security";
        public const string BLOCK_INCHARGE = "Block Incharge";
    }
        

   
}