using MilanSzDemo.Model;
using MilanSzDemo.ViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MilanSzDemo.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailPage : Page
    {
        private DetailPageViewModel viewModel;

        public DetailPage()
        {
            this.InitializeComponent();
            viewModel = new DetailPageViewModel();
            this.DataContext = viewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var rss = e.Parameter as Rss;
            viewModel.SetRss(rss);
        }
    }
}
