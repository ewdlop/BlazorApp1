using BlazorApp1.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Components;

public partial class PersonRenderer : ComponentBase
{
	[Parameter]
	public IPerson Person { get; set; } = null!;
}