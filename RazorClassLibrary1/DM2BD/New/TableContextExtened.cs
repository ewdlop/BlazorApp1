using MudBlazor;

namespace RazorClassLibrary1.DM2BD.New
{
    public partial class TableContextExtened<T> : TableContext<T>
    {
        public void SetSortFieldLabel(string sortFieldLabel)
        {

        }

        public void SetSortDirection(SortDirection sortDirection)
        {
            SortDirection = sortDirection;
        }
    }
}