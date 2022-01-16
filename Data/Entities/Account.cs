using Data.Enums;

namespace Data.Entities;

public class Account : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string PersonalUrl { get; set; }
    public int YearsOfAge { get; set; }
    public RelationshipStatus RelationshipStatus { get; set; }

    public Account(string firstName, string lastName, 
        string emailAddress, string phoneNumber, 
        string personalUrl, int yearsOfAge,
        RelationshipStatus relationshipStatus)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        PersonalUrl = personalUrl;
        YearsOfAge = yearsOfAge;
        RelationshipStatus = relationshipStatus;
    }
}