using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Web.Inputs;

public class OrderInput
{
    [JsonIgnore]
    public Guid Id { get; set; } = Guid.Empty;
    
    [Display(Name = "Name")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Whitespaces are not allowed")]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "{0} has to be {2} characters minimum and {1} characters maximum")]
    public string Name { get; set; } = String.Empty;
}