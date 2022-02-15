using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Shared;

public partial class MainLayout : LayoutComponentBase
{
	bool _drawerOpen = false;

	void DrawerToggle()
	{
		_drawerOpen = !_drawerOpen;
	}
}