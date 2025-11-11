using System.ComponentModel.DataAnnotations;

namespace AMI_ProjectAPI.Data
{
    public class UserDTO
    {
       
        public int UserId { get; set; }


        public string UserName { get; set; } 

       

       
        public string Email { get; set; } 

       
        public string Role { get; set; }

        public string? DisplayName { get; set; }
        public string? Phone { get; set; }
        public bool Active { get; set; }
        //public DateTime? LastLogin { get; set; }




    }
}
