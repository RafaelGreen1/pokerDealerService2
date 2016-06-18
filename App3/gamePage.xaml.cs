

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

using System;
using System.Threading.Tasks;
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
            this.Frame.Navigate(typeof(LogInPage));
        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            GameTable gameTable = await PokerDealerProxy.GetGameTable();
            gameTable.active = 1;
            if (gameTable.Id4 != 0)
            {
                gameTable.current_id = 4;
                gameTable.active4 = 1;
            } else
            {
                gameTable.active4 = 0;
            }
            if (gameTable.Id3 != 0)
            {
                gameTable.current_id = 3;
                gameTable.active3 = 1;
            } else
            {
                gameTable.active3 = 0;
            }
            if (gameTable.Id2 != 0)
            {
                gameTable.current_id = 2;
                gameTable.active2 = 1;
            } else
            {
                gameTable.active2 = 0;
            }
            if (gameTable.Id1 != 0)
            {
                gameTable.current_id = 1;
                gameTable.active1 = 1;
            } else
            {
                gameTable.active1 = 0;
            }
            gameTable.state = "dealed";
            await PokerDealerProxy.SetGameTable(gameTable);
            await PokerDealerProxy.MoveFirstPlayer();
            return;
        }



        public async void updateGameTable()
        {
            while (true)
            {
                try
                {
                    await Task.Delay(500);
                    GameTable gameTable = await PokerDealerProxy.GetGameTable();

                    this.gameTable = gameTable;
                    string username1 = "", username2 = "", username3 = "", username4 = "";
                    string dollars1 = "", dollars2 = "", dollars3 = "", dollars4 = "";
                    string pot1 = "", pot2 = "", pot3 = "", pot4 = "";
                    if (gameTable.Id1 != 0)
                    {
                        username1 = await PokerDealerProxy.GetUsernameById(gameTable.Id1);
                        dollars1 = await PokerDealerProxy.GetDollarsById(gameTable.Id1);
                        pot1 = gameTable.pot1.ToString();

                    }
                    if (gameTable.Id2 != 0)
                    {
                        username2 = await PokerDealerProxy.GetUsernameById(gameTable.Id2);
                        dollars2 = await PokerDealerProxy.GetDollarsById(gameTable.Id2);
                        pot2 = gameTable.pot2.ToString();
                    }
                    if (gameTable.Id3 != 0)
                    {
                        username3 = await PokerDealerProxy.GetUsernameById(gameTable.Id3);
                        dollars3 = await PokerDealerProxy.GetDollarsById(gameTable.Id3);
                        pot3 = gameTable.pot3.ToString();
                    }
                    if (gameTable.Id4 != 0)
                    {
                        username4 = await PokerDealerProxy.GetUsernameById(gameTable.Id4);
                        dollars4 = await PokerDealerProxy.GetDollarsById(gameTable.Id4);
                        pot4 = gameTable.pot4.ToString();
                    }




                    if (!username1.Equals(this.txtName1.Text) || !username2.Equals(this.txtName2.Text)
                         || !username3.Equals(this.txtName3.Text) || !username4.Equals(this.txtName4.Text))
                    {
                        this.cboWinner.Items.Clear();
                        if (username1 != "") this.cboWinner.Items.Add(username1);
                        if (username2 != "") this.cboWinner.Items.Add(username2);
                        if (username3 != "") this.cboWinner.Items.Add(username3);
                        if (username4 != "") this.cboWinner.Items.Add(username4);
                    }

                    updateTextBox(this.txtName1, username1);
                    updateTextBox(this.txtName2, username2);
                    updateTextBox(this.txtName3, username3);
                    updateTextBox(this.txtName4, username4);

                    updateTextBox(this.txtTotal1, dollars1);
                    updateTextBox(this.txtTotal2, dollars2);
                    updateTextBox(this.txtTotal3, dollars3);
                    updateTextBox(this.txtTotal4, dollars4);

                    updateTextBox(this.txtPot1, pot1);
                    updateTextBox(this.txtPot2, pot2);
                    updateTextBox(this.txtPot3, pot3);
                    updateTextBox(this.txtPot4, pot4);

                    Int32 totalPot = (this.txtPot1.Text.Equals("")) ? 0 : Int32.Parse(this.txtPot1.Text);
                    totalPot += (this.txtPot2.Text.Equals("")) ? 0 : Int32.Parse(this.txtPot2.Text);
                    totalPot += (this.txtPot3.Text.Equals("")) ? 0 : Int32.Parse(this.txtPot3.Text);
                    totalPot += (this.txtPot4.Text.Equals("")) ? 0 : Int32.Parse(this.txtPot4.Text);
                    updateTextBox(this.txtTotalPot, totalPot.ToString());

                    /* update cards */
                    if (gameTable.state.Trim().Equals("clear"))
                    {
                        clearCards();
                    }

                    if (gameTable.state.Trim().Equals("dealed"))
                    {
                        dealCards(gameTable);
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

                    invertVisibility(this.imgFace1, 1);
                    invertVisibility(this.imgFace2, 2);
                    invertVisibility(this.imgFace3, 3);
                    invertVisibility(this.imgFace4, 4);


                    clearInactiveCards(gameTable);
                    clearNoUsers(gameTable);
                } catch {

                }
                
            }

        }

        public void updateTextBox(TextBox textBox, string s)
        {
            textBox.Text = s;
        }

        private void clearInactiveCards(GameTable gameTable)
        {
            if (gameTable.active1 == 0) this.imgCard11.Visibility = Visibility.Collapsed;
            if (gameTable.active2 == 0) this.imgCard12.Visibility = Visibility.Collapsed;
            if (gameTable.active3 == 0) this.imgCard13.Visibility = Visibility.Collapsed;
            if (gameTable.active4 == 0) this.imgCard14.Visibility = Visibility.Collapsed;
            if (gameTable.active1 == 0) this.imgCard21.Visibility = Visibility.Collapsed;
            if (gameTable.active2 == 0) this.imgCard22.Visibility = Visibility.Collapsed;
            if (gameTable.active3 == 0) this.imgCard23.Visibility = Visibility.Collapsed;
            if (gameTable.active4 == 0) this.imgCard24.Visibility = Visibility.Collapsed;
        }

        private void clearNoUsers(GameTable gameTable)
        {
            if (gameTable.Id1 == 0) this.imgFace1.Visibility = Visibility.Collapsed;
            if (gameTable.Id2 == 0) this.imgFace2.Visibility = Visibility.Collapsed;
            if (gameTable.Id3 == 0) this.imgFace3.Visibility = Visibility.Collapsed;
            if (gameTable.Id4 == 0) this.imgFace4.Visibility = Visibility.Collapsed;
        }

        public void clearCards()
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

        private void dealCards(GameTable gameTable)
        {

            if (gameTable.active1 == 1) this.imgCard11.Visibility = Visibility.Visible;
            if (gameTable.active2 == 1) this.imgCard12.Visibility = Visibility.Visible;
            if (gameTable.active3 == 1) this.imgCard13.Visibility = Visibility.Visible;
            if (gameTable.active4 == 1) this.imgCard14.Visibility = Visibility.Visible;
            if (gameTable.active1 == 1) this.imgCard21.Visibility = Visibility.Visible;
            if (gameTable.active2 == 1) this.imgCard22.Visibility = Visibility.Visible;
            if (gameTable.active3 == 1) this.imgCard23.Visibility = Visibility.Visible;
            if (gameTable.active4 == 1) this.imgCard24.Visibility = Visibility.Visible;
            this.imgFlop1.Visibility = Visibility.Collapsed;
            this.imgFlop2.Visibility = Visibility.Collapsed;
            this.imgFlop3.Visibility = Visibility.Collapsed;
            this.imgTurn.Visibility  = Visibility.Collapsed;
            this.imgRiver.Visibility = Visibility.Collapsed;
        }

        public void flopCards()
        {
            this.imgFlop1.Visibility = Visibility.Visible;
            this.imgFlop2.Visibility = Visibility.Visible;
            this.imgFlop3.Visibility = Visibility.Visible;
        }

        public void turnCard()
        {
            this.imgTurn.Visibility = Visibility.Visible;
        }

        public void riverCard()
        {
            this.imgRiver.Visibility = Visibility.Visible;
        }

        public async void invertVisibility(Image img, int id)
        {

            if (gameTable.current_id != 0 && await PokerDealerProxy.GetIdByLocation(gameTable.current_id) == App.Id
                && !gameTable.state.Trim().Equals("river"))
            {
                this.btnBet.IsEnabled = true;
                this.btnCall.IsEnabled = true;
                this.btnCheck.IsEnabled = true;
                this.btnFold.IsEnabled = true;
                this.btnSetWinner.IsEnabled = true;
            } else
            {
                this.btnBet.IsEnabled = false;
                this.btnCall.IsEnabled = false;
                this.btnCheck.IsEnabled = false;
                this.btnFold.IsEnabled = false;
                if (gameTable.current_id != 0 && await PokerDealerProxy.GetIdByLocation(gameTable.current_id) == App.Id
                    && gameTable.state.Trim().Equals("river"))
                {
                    this.btnSetWinner.IsEnabled = true;
                } else
                {
                    this.btnSetWinner.IsEnabled = false;
                }

            }

                if (gameTable.current_id == id)
            {
                if (img.Visibility == Visibility.Visible)
                {
                    img.Visibility = Visibility.Collapsed;
                }
                else
                {
                    img.Visibility = Visibility.Visible;
                }
            } else
            {
                if ((gameTable.Id1 != 0 && id == 1) ||
                    (gameTable.Id2 != 0 && id == 2) ||
                    (gameTable.Id3 != 0 && id == 3) ||
                    (gameTable.Id4 != 0 && id == 4))
                {
                    img.Visibility = Visibility.Visible;
                }
            }
        }

        private async void btnLeave_Click(object sender, RoutedEventArgs e)
        {
            GameTable gameTable = await PokerDealerProxy.GetGameTable();
            if (!gameTable.state.Equals("clear") && !gameTable.state.Equals("river")) return;
            if (gameTable.Id1 == App.Id) { gameTable.Id1 = 0; gameTable.pot1 = 0; gameTable.active1 = 0; }
            if (gameTable.Id2 == App.Id) { gameTable.Id2 = 0; gameTable.pot2 = 0; gameTable.active2 = 0; }
            if (gameTable.Id3 == App.Id) { gameTable.Id3 = 0; gameTable.pot3 = 0; gameTable.active3 = 0; }
            if (gameTable.Id4 == App.Id) { gameTable.Id4 = 0; gameTable.pot4 = 0; gameTable.active4 = 0; }
            if (gameTable.Id1 + gameTable.Id2 + gameTable.Id2 + gameTable.Id3 == 0)
            {
                await PokerDealerProxy.GameReset();
                return;
            }
            

            await PokerDealerProxy.SetGameTable(gameTable);
            return;
        }

        private async void btnFold_Click(object sender, RoutedEventArgs e)
        {
            await PokerDealerProxy.SetInActive(App.Id);
            await PokerDealerProxy.MoveCurrent();
        }

        private async void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            await PokerDealerProxy.MoveCurrent();
        }

        private async void btnCall_Click(object sender, RoutedEventArgs e)
        {
            await PokerDealerProxy.Call(App.Id);
            await PokerDealerProxy.MoveCurrent();
        }

        private async void btnBet_Click(object sender, RoutedEventArgs e)
        {
            await PokerDealerProxy.Bet(App.Id, Int32.Parse(this.txtBet.Text.ToString()));
            await PokerDealerProxy.MoveCurrent();
            this.txtBet.Text = "";
        }

        private async void btnSetWinner_Click(object sender, RoutedEventArgs e)
        {
            if (this.cboWinner.SelectedItem == null) return;
            Int32 pot1 = (this.txtPot1.Text == "") ? 0 : Int32.Parse(this.txtPot1.Text);
            Int32 pot2 = (this.txtPot2.Text == "") ? 0 : Int32.Parse(this.txtPot2.Text);
            Int32 pot3 = (this.txtPot3.Text == "") ? 0 : Int32.Parse(this.txtPot3.Text);
            Int32 pot4 = (this.txtPot4.Text == "") ? 0 : Int32.Parse(this.txtPot4.Text);
            Int32 totalPot = pot1 + pot2 + pot3 + pot4;
            GameTable gameTable = await PokerDealerProxy.GetGameTable();
            if (pot1 > 0) await PokerDealerProxy.ReduceDollarsById(gameTable.Id1, pot1);
            if (pot2 > 0) await PokerDealerProxy.ReduceDollarsById(gameTable.Id2, pot2);
            if (pot3 > 0) await PokerDealerProxy.ReduceDollarsById(gameTable.Id3, pot3);
            if (pot4 > 0) await PokerDealerProxy.ReduceDollarsById(gameTable.Id4, pot4);
            await PokerDealerProxy.AddDollarsByUsername(this.cboWinner.SelectedItem.ToString(), totalPot);
            await PokerDealerProxy.ZeroAllPots();
            this.cboWinner.SelectedItem = null;
        }
    }
}
