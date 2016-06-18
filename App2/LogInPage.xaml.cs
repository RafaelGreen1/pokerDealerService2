using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace pokerDealerApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LogInPage : Page
    {
        public LogInPage()
        {
            this.InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            string res = await PokerDealerProxy.logIn(this.txtUsermame.Text, this.txtPassword.Text);
            Int32 resInt = Int32.Parse(res);
            if (resInt == 0)
            {
                this.txtResult.Text = "login failed, wrong credentials!";
                return;
            }
            App.username = this.txtUsermame.Text;
            App.Id = resInt;

            res = await PokerDealerProxy.IsFreePlace();
            resInt = Int32.Parse(res);
            if (resInt == 0)
            {
                this.txtResult.Text = "Sorry, no free place.";
                return;
            }

            res = await PokerDealerProxy.IsActiveGame();
            resInt = Int32.Parse(res);
            if (resInt == 1)
            {
                this.txtResult.Text = "Sorry, the game has already started.";
                return;
            }


            await PokerDealerProxy.FillFirstAvailableId(App.Id);
            this.Frame.Navigate(typeof(gamePage));
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            this.txtUsermame.Text = "";
            this.txtPassword.Text = "";
        }

        private async void btnReset_Click(object sender, RoutedEventArgs e)
        {
            await PokerDealerProxy.GameReset();
            this.txtReset.Text = "Game data has been reset successfully!";
        }
    }
}
