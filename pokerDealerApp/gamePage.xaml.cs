﻿

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

using System.Collections.Generic;
using System.Threading;
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
        private GameTable gameTable = new GameTable();
        public gamePage()
        {
            this.InitializeComponent();
            this.txtMyName.Text = "Welcome " + App.username + "!";
            updateGameTable();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            GameTable gameTable = await PokerDealerProxy.GetGameTable();
            gameTable.active = 1;
            gameTable.state = "dealed";
            await PokerDealerProxy.SetGameTable(gameTable);
            return;
        }



        public async void updateGameTable()
        {
            while(true)
            {
                await Task.Delay(2000);
                GameTable gameTable = await PokerDealerProxy.GetGameTable();
                if (this.gameTable.Equals(gameTable)) return;

                this.gameTable = gameTable;
                string username1 = "", username2 = "", username3 = "", username4 = "";
                string dollars1 = "", dollars2 = "", dollars3 = "", dollars4 = "";
                if (gameTable.Id1 != 0)
                {
                    username1 = await PokerDealerProxy.GetUsernameById(gameTable.Id1);
                    dollars1 = await PokerDealerProxy.GetDollarsById(gameTable.Id1);

                }
                if (gameTable.Id2 != 0)
                {
                    username2 = await PokerDealerProxy.GetUsernameById(gameTable.Id2);
                    dollars2 = await PokerDealerProxy.GetDollarsById(gameTable.Id2);
                }
                if (gameTable.Id3 != 0)
                {
                    username3 = await PokerDealerProxy.GetUsernameById(gameTable.Id3);
                    dollars3 = await PokerDealerProxy.GetDollarsById(gameTable.Id3);
                }
                if (gameTable.Id4 != 0)
                {
                    username4 = await PokerDealerProxy.GetUsernameById(gameTable.Id4);
                    dollars4 = await PokerDealerProxy.GetDollarsById(gameTable.Id4);
                }



                Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    () =>
                    {
                        if (!username1.Equals(this.txtName1.Text) || !username2.Equals(this.txtName2.Text)
                             || !username3.Equals(this.txtName3.Text) || !username4.Equals(this.txtName4.Text))
                        {
                            this.cboWinner.Items.Clear();
                            if (username1 != "") this.cboWinner.Items.Add(username1);
                            if (username2 != "") this.cboWinner.Items.Add(username2);
                            if (username3 != "") this.cboWinner.Items.Add(username3);
                            if (username4 != "") this.cboWinner.Items.Add(username4);
                        }

                    }
                    );
                updateTextBox(this.txtName1, username1);
                updateTextBox(this.txtName2, username2);
                updateTextBox(this.txtName3, username3);
                updateTextBox(this.txtName4, username4);

                /* update cards */
                if (gameTable.state.Trim().Equals("clear"))
                {
                    clearCards();
                }

                if (gameTable.state.Trim().Equals("dealed"))
                {
                    dealCards();
                }


                if (gameTable.state.Trim().Equals("flop"))
                {
                    flopCards();
                }

                if (gameTable.state.Trim().Equals("turn"))
                {
                    turnCard();
                }

                if (gameTable.state.Trim().Equals("river"))
                {
                    riverCard();
                }
            }

        }

        public void updateTextBox(TextBox textBox, string s)
        {
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                            () =>
                            {
                                textBox.Text = s;
                            }
                            );
        }

        public void clearCards()
        {
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    this.imgCard11.Visibility = Visibility.Collapsed;
                    this.imgCard12.Visibility = Visibility.Collapsed;
                    this.imgCard13.Visibility = Visibility.Collapsed;
                    this.imgCard14.Visibility = Visibility.Collapsed;
                    this.imgCard21.Visibility = Visibility.Collapsed;
                    this.imgCard22.Visibility = Visibility.Collapsed;
                    this.imgCard23.Visibility = Visibility.Collapsed;
                    this.imgCard24.Visibility = Visibility.Collapsed;
                    this.imgFlop1.Visibility = Visibility.Collapsed;
                    this.imgFlop2.Visibility = Visibility.Collapsed;
                    this.imgFlop3.Visibility = Visibility.Collapsed;
                    this.imgTurn.Visibility = Visibility.Collapsed;
                    this.imgRiver.Visibility = Visibility.Collapsed;
                }
                );
        }

        public void dealCards()
        {
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    if (!this.txtName1.Text.Equals("")) this.imgCard11.Visibility = Visibility.Visible;
                    if (!this.txtName2.Text.Equals("")) this.imgCard12.Visibility = Visibility.Visible;
                    if (!this.txtName3.Text.Equals("")) this.imgCard13.Visibility = Visibility.Visible;
                    if (!this.txtName4.Text.Equals("")) this.imgCard14.Visibility = Visibility.Visible;
                    if (!this.txtName1.Text.Equals("")) this.imgCard21.Visibility = Visibility.Visible;
                    if (!this.txtName2.Text.Equals("")) this.imgCard22.Visibility = Visibility.Visible;
                    if (!this.txtName3.Text.Equals("")) this.imgCard23.Visibility = Visibility.Visible;
                    if (!this.txtName4.Text.Equals("")) this.imgCard24.Visibility = Visibility.Visible;
                }
                );
        }

        public void flopCards()
        {
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    this.imgFlop1.Visibility = Visibility.Visible;
                    this.imgFlop2.Visibility = Visibility.Visible;
                    this.imgFlop3.Visibility = Visibility.Visible;
                }
                );
        }

        public void turnCard()
        {
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    this.imgTurn.Visibility = Visibility.Visible;
                }
                );
        }

        public void riverCard()
        {
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    this.imgRiver.Visibility = Visibility.Visible;
                }
                );
        }

    }
}
