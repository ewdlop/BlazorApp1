// UserData.cs - Example domain model
using BlazorApp1.Interfaces;

namespace BlazorApp1.Models;

public class UserData : IUserData
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
}
