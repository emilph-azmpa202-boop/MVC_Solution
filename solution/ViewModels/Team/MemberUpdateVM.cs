namespace solution.ViewModels.Team
{
    public class MemberUpdateVM
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Job { get; set; }
        public string Description { get; set; }

        public string Image { get; set; }

        public IFormFile? Photo { get; set; }
    }
}