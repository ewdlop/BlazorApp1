namespace BlazorApp1.Interfaces;

public interface IPersonRepository
{
	bool TryAddPerson(IPerson person);
	IEnumerable<IPerson> GetPeople();
	bool TryRemovePerson(Guid personId);
}