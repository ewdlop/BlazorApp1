using BlazorApp1.Interfaces;

namespace BlazorApp1.Classes;

public class Person : IPerson
{
	private string _firstName;
	private string _lastName;

    public Person(string firstName, string lastName, int patientCount, int claimCount)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(lastName));

        _firstName = firstName;
        _lastName = lastName;

        PatientCount = patientCount;
        ClaimCount = claimCount;
    }

    public Guid Id { get; } = Guid.NewGuid();

	public string FirstName
	{
		get => _firstName;
		set
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
			
			_firstName = value;
		}
	}

	public string LastName
	{
		get => _lastName;
		set
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
			
			_lastName = value;
		}
	}

    public int PatientCount { get; set; }
    public int ClaimCount { get; set; }

    public string DisplayName => $"{FirstName} {LastName}";
}