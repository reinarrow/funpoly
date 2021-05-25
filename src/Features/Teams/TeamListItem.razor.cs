using Funpoly.Data.Models;
using Microsoft.AspNetCore.Components;

namespace Funpoly.Features.Teams
{
    public partial class TeamListItem : ComponentBase
    {
        [Parameter]
        public Team Team { get; set; }

        [CascadingParameter]
        protected bool IsBanker { get; set; }
    }
}