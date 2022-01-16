using System.ComponentModel.DataAnnotations;
using Data.Enums;

namespace Web.Inputs;

public class AccountInput
{
    public Guid Id { get; set; } = Guid.Empty;

    [Display(Name = "First Name")]
    [Required(AllowEmptyStrings = false)]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "First name has to be {2} characters minimum and {1} characters maximum")]
    public string FirstName { get; set; } = String.Empty;

    [Display(Name = "Last Name")]
    [Required(AllowEmptyStrings = false)]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "Last name has to be {2} characters minimum and {1} characters maximum")]
    public string LastName { get; set; } = String.Empty;

    [Display(Name = "Email Address")]
    [EmailAddress]
    public string EmailAddress { get; set; } = String.Empty;

    [Display(Name = "Phone Number")]
    [Phone]
    public string PhoneNumber { get; set; } = String.Empty;

    [Display(Name = "Personal Url")]
    [Url]
    public string PersonalUrl { get; set; } = String.Empty;

    [Display(Name = "Years Of Age")]
    [Range(0, 100)]
    public int YearsOfAge { get; set; } = 0;

    [Display(Name = "Relationship Status")]
    [EnumDataType(typeof(RelationshipStatus))]
    public RelationshipStatus RelationshipStatus { get; set; } = RelationshipStatus.Single;
}