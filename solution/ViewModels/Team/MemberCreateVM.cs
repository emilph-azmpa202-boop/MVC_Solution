using System.ComponentModel.DataAnnotations;

public class MemberCreateVM
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Job { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public IFormFile Photo { get; set; }
}