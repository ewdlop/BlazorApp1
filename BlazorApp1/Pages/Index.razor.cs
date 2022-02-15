using BlazorApp1.Classes;
using BlazorApp1.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Charts.SVG.Models;

namespace BlazorApp1.Pages;

public partial class Index : ComponentBase
{
	[Inject]
	private IPersonRepository PersonRepository { get; set; } = null!;
    
    private int _selectedIndex;

    private DonutDisplayProperty _donutDisplayProperty = 
		DonutDisplayProperty.PatientCount;

    private double[] GetInputData() => _donutDisplayProperty switch
    {
        DonutDisplayProperty.ClaimCount => PersonRepository
                            .GetPeople()
                            .Select(p => (double)p.ClaimCount)
                            .ToArray(),
        _ => PersonRepository
                 .GetPeople()
                 .Select(p => (double)p.PatientCount)
                 .ToArray()
    };
    
    private string[] GetInputLabels() => PersonRepository
                            .GetPeople()
                            .Select(p => p.DisplayName)
                            .ToArray();

    private int SelectedIndex
	{
		get => _selectedIndex;
		set
		{
			_selectedIndex = value;

			FireOnSelectedIndexChanged();
		}
	}

	private void FireOnSelectedIndexChanged()
	{
		Console.WriteLine($"SelectedIndex: {SelectedIndex}");
	}

    private void OnSectionMouseOverEventCallback(
        (MouseEventArgs mouseEventArgs, SvgCircle svgCircle) callbackTuple)
    {
        Console.WriteLine($"{nameof(OnSectionMouseOverEventCallback)} was fired on index: {callbackTuple.svgCircle.Index}");
    }
}