namespace Model
{
    public class UserSaveModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
    }

    public class UserViewModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int IsApproved { get; set; }
        public int DomainId { get; set; }
        public int UserId { get; set; }
        public string DomainTitle { get; set; }
    }
}
