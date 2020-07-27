using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfControls;

namespace OneHub360.WPF.Register.App.Controls.General
{
    public class InternalUnitSuggestionProvider : ISuggestionProvider
    {
        public IEnumerable GetSuggestions(string filter)
        {
            var results = ((App)Application.Current).InternalUsersSource.Where(P => P.About.IndexOf(filter, StringComparison.InvariantCulture) != -1 || P.FullName.IndexOf(filter, StringComparison.InvariantCulture) != -1);
            return results;
        }
    }
}
