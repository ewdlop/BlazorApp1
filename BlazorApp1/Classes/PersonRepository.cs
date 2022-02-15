using BlazorApp1.Interfaces;

namespace BlazorApp1.Classes;

public class PersonRepository : IPersonRepository
{
	private List<IPerson> _people = new();

    public PersonRepository()
    {
        _people.Add(new Person("Hunter", "Freeman", 3, 7));
        _people.Add(new Person("Raymond", "Lei", 3, 1));
        _people.Add(new Person("Brian", "Freeman", 2, 6));
        _people.Add(new Person("Isabella", "Chiaravalloti", 4, 6));

		// TODO: Test bad data like this next one
        // _people.Add(new Person("Mike", "Parrilla", -1, 6));
    }

    public bool TryAddPerson(IPerson person)
	{
		var personInRepository = _people
			.FirstOrDefault(p => p.Id == person.Id);

		if (personInRepository is not null)
			return false;
		
		_people.Add(person);
		return true;
	}

	public IEnumerable<IPerson> GetPeople() => _people;

	public bool TryRemovePerson(Guid personId)
	{
		var personInRepository = _people
			.FirstOrDefault(p => p.Id == personId);

		if (personInRepository is null)
			return false;
		
		_people.Remove(personInRepository);
		return true;
	}
}