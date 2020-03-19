namespace PPR.App.DTOs.User
{
    public class UserUpdateDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string[] Roles { get; set; }
    }
}