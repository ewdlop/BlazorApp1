namespace BlazorApp1.Interfaces;

public interface IPerson
{
	Guid Id { get; }
	string FirstName { get; set; }
	string LastName { get; set; }
	string DisplayName { get; }
    int PatientCount { get; set; }
    int ClaimCount { get; set; }
}