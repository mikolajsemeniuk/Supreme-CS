using Data.Enums;

namespace Data.Entities;

public class Account
{
    public Guid AccountId { get; set; } = Guid.NewGuid();
    public string FullName { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string PersonalUrl { get; set; }
    public Byte YearsOfAge { get; set; }
    public bool IsExternalContractor { get; set; }
    public RelationshipStatus RelationshipStatus { get; set; }
    public string Note { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();

    public Account(string fullName,
        string emailAddress, string phoneNumber, 
        string personalUrl, Byte yearsOfAge,
        bool isExternalContractor, RelationshipStatus relationshipStatus,
        string note)
    {
        FullName = fullName;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        PersonalUrl = personalUrl;
        YearsOfAge = yearsOfAge;
        IsExternalContractor = isExternalContractor;
        RelationshipStatus = relationshipStatus;
        Note = note;
    }
}