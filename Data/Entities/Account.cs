namespace Data.Entities;

public class Account
{
    public Guid AccountId { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string PersonalUrl { get; set; }
    public int YearsOfAge { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }

    public Account(string firstName, string lastName, 
        string emailAddress, string phoneNumber, 
        string personalUrl, int yearsOfAge)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        PersonalUrl = personalUrl;
        YearsOfAge = yearsOfAge;
    }
}