using System.ComponentModel.DataAnnotations;
using Data.Enums;

namespace Web.Inputs;

public class AccountInput
{
    [Display(Name = "First Name")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Whitespaces are not allowed")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "First name has to be {2} characters minimum and {1} characters maximum")]
    public string FirstName { get; set; } = String.Empty;

    [Display(Name = "Last Name")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Whitespaces are not allowed")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name has to be {2} characters minimum and {1} characters maximum")]
    public string LastName { get; set; } = String.Empty;

    [Display(Name = "Email Address")]
    [EmailAddress(ErrorMessage = "Email address is invalid")]
    public string EmailAddress { get; set; } = String.Empty;

    [Display(Name = "Phone Number")]
    [Phone(ErrorMessage = "Phone number is invalid")]
    public string PhoneNumber { get; set; } = String.Empty;

    [Display(Name = "Personal Url")]
    [Url(ErrorMessage = "Url is invalid")]
    public string PersonalUrl { get; set; } = String.Empty;

    [Display(Name = "Years Of Age")]
    [Range(18, 100, ErrorMessage = "Age has to be in range between {1} and {2}")]
    public int YearsOfAge { get; set; } = 0;

    [Display(Name = "Relationship Status")]
    [EnumDataType(typeof(RelationshipStatus), ErrorMessage = "Invalid status")]
    public RelationshipStatus RelationshipStatus { get; set; } = RelationshipStatus.Single;
}