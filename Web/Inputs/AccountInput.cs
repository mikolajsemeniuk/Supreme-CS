using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Data.Enums;

namespace Web.Inputs;

public class AccountInput
{
    [JsonIgnore]
    public Guid Id { get; set; } = Guid.Empty;

    [Display(Name = "Full Name")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Whitespaces are not allowed")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "{0} has to be {2} characters minimum and {1} characters maximum")]
    public string FullName { get; set; } = String.Empty;

    [Display(Name = "Email Address")]
    [EmailAddress(ErrorMessage = "{0} is invalid")]
    public string EmailAddress { get; set; } = String.Empty;

    [Display(Name = "Phone Number")]
    [Phone(ErrorMessage = "{0} is invalid")]
    public string PhoneNumber { get; set; } = String.Empty;

    [Display(Name = "Personal Url")]
    [Url(ErrorMessage = "{0} is invalid")]
    public string PersonalUrl { get; set; } = String.Empty;

    [Display(Name = "Years Of Age")]
    [Range(18, 100, ErrorMessage = "{0} has to be in range between {1} and {2}")]
    public Byte YearsOfAge { get; set; } = 0;

    [Display(Name = "Note")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Whitespaces are not allowed")]
    [StringLength(500, MinimumLength = 5, ErrorMessage = "{0} has to be {2} characters minimum and {1} characters maximum")]
    public string Note { get; set; } = String.Empty;

    [Display(Name = "Is External Contractor")]
    public bool IsExternalContractor { get; set; } = false;

    [Display(Name = "Relationship Status")]
    [EnumDataType(typeof(RelationshipStatus), ErrorMessage = "{0} is invalid")]
    public RelationshipStatus RelationshipStatus { get; set; } = RelationshipStatus.Single;
}