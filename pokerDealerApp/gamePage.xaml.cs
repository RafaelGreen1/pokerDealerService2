

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

using System.Threading.Tasks;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace pokerDealerApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class gamePage : Page
    {
        public gamePage()
        {
            this.InitializeComponent();
            this.txtMyName.Text = "Welcome " + App.username + "!";

            System.TimeSpan period = System.TimeSpan.FromMilliseconds(500);

            ThreadPoolTimer PeriodicTimer = ThreadPoolTimer.CreatePeriodicTimer((source) =>
            {
                updateGameTable();
            }, period);

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {

        }



        public async void updateGameTable()
        {
            GameTable gameTable = await PokerDealerProxy.GetGameTable();
            if (gameTable.Id1 != 0)
            {
                string s = await PokerDealerProxy.GetUsernameById(gameTable.Id1);
                Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    this.txtName1.Text = s;
                }
                );
            }
        }
    }
}
