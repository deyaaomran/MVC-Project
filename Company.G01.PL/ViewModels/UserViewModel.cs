namespace Company.G01.PL.ViewModels
{
    public class UserViewModel
    {
        public string id { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }


    }
}
