

namespace GeoLocationMAUITallerGrupal
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Views.NotesView), typeof(Views.NotesView));
        }
    }
}
