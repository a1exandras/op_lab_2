using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace op_lab_2
{
    class gameWindow
    {
        static string currentPlayer = "X";
        static string[,] Board = new string[5, 5];

        TextBlock winWindow = new TextBlock();

        public gameWindow()
        {
            initControls();
        }

        private void initControls()
        {
            Window wn = new Window();
            wn.Title = "Tic Tac Toe";

            Grid mainGrid = new Grid();
            GridLengthConverter glConverter = new GridLengthConverter();

            RowDefinition rd0 = new RowDefinition();
            rd0.Height = (GridLength)glConverter.ConvertFrom("4*");
            mainGrid.RowDefinitions.Add(rd0);

            RowDefinition rd1 = new RowDefinition();
            rd1.Height = (GridLength)glConverter.ConvertFrom("*");
            mainGrid.RowDefinitions.Add(rd1);

            {
                Grid r2Grid = new Grid();

                ColumnDefinition[] cols_r0 = new ColumnDefinition[5];
                for (int i = 0; i < 5; i++)
                {
                    cols_r0[i] = new ColumnDefinition();
                    cols_r0[i].Width = (GridLength)glConverter.ConvertFrom("*");
                    r2Grid.ColumnDefinitions.Add(cols_r0[i]);
                }

                RowDefinition[] rows_r0 = new RowDefinition[5];
                for (int i = 0; i < 5; i++)
                {
                    rows_r0[i] = new RowDefinition();
                    rows_r0[i].Height = (GridLength)glConverter.ConvertFrom("*");
                    r2Grid.RowDefinitions.Add(rows_r0[i]);
                }

                Button[,] btnArr = new Button[5, 5];
                for(int i = 0; i < 5; i++)
                    for(int j = 0; j < 5; j++)
                    {
                        btnArr[i, j] = new Button();
                        btnArr[i, j].Click += playerClicked;
                        btnArr[i, j].Tag = i.ToString() + j.ToString();
                        //btnArr[i, j].Content = i.ToString() + j.ToString();

                        Grid.SetRow(btnArr[i, j], i);
                        Grid.SetColumn(btnArr[i, j], j);

                        r2Grid.Children.Add(btnArr[i, j]);
                    }

                Grid.SetRow(r2Grid, 0);
                mainGrid.Children.Add(r2Grid);
            }

            Button restartButton = new Button(); restartButton.Click += restartButton_Click;
            restartButton.FontSize = 30; restartButton.Content = "Restart"; restartButton.Background = new SolidColorBrush(Colors.AliceBlue); restartButton.Margin = new Thickness(15);
            Grid.SetRow(restartButton, 1);
            mainGrid.Children.Add(restartButton);

            winWindow.Visibility = Visibility.Collapsed; winWindow.FontSize = 30; winWindow.Background = new SolidColorBrush(Colors.White); winWindow.HorizontalAlignment = HorizontalAlignment.Center; winWindow.VerticalAlignment = VerticalAlignment.Center;
            mainGrid.Children.Add(winWindow);

            wn.Content = mainGrid;
            wn.Show();
        }

        private void playerClicked(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (!String.IsNullOrWhiteSpace(btn.Content?.ToString()))
                return;

            btn.Content = currentPlayer;
            int btnTag = Int32.Parse(btn.Tag.ToString());
            Board[btnTag / 10, btnTag % 10] = currentPlayer;

            checkWin();

            switchPlayer();
        }

        private void restartButton_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer = "X";
            initControls();

            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    Board[i, j] = String.Empty;
        }

        private void switchPlayer()
        {
            if (currentPlayer == "X")
                currentPlayer = "O";
            else
                currentPlayer = "X";
        }

        private void checkWin()
        {
            for (int i = 0; i <= 1; i++)
                for (int j = 0; j <= 4; j++)
                {
                    if (!String.IsNullOrWhiteSpace(Board[i, j]))
                        if (Board[i, j] == Board[i + 1, j] && Board[i, j] == Board[i + 2, j] && Board[i, j] == Board[i + 3, j])
                            playerWin(Board[i, j]);
                }

            for (int i = 0; i <= 4; i++)
                for (int j = 0; j <= 1; j++)
                {
                    if (!String.IsNullOrWhiteSpace(Board[i, j]))
                        if (Board[i, j] == Board[i, j + 1] && Board[i, j] == Board[i, j + 2] && Board[i, j] == Board[i, j + 3])
                            playerWin(Board[i, j]);
                }

            for (int i = 0; i <= 1; i++)
                for (int j = 0; j <= 1; j++)
                {
                    if (!String.IsNullOrWhiteSpace(Board[i, j]))
                        if (Board[i, j] == Board[i + 1, j + 1] && Board[i, j] == Board[i + 2, j + 2] && Board[i, j] == Board[i + 3, j + 3])
                            playerWin(Board[i, j]);
                }

            for (int i = 3; i <= 4; i++)
                for (int j = 0; j <= 1; j++)
                {
                    if (!String.IsNullOrWhiteSpace(Board[i, j]))
                        if (Board[i, j] == Board[i - 1, j + 1] && Board[i, j] == Board[i - 2, j + 2] && Board[i, j] == Board[i - 3, j + 3])
                            playerWin(Board[i, j]);
                }
        }

        private void playerWin(string player)
        {
            winWindow.Text = "Player " + player + " Wins!";
            winWindow.Visibility = Visibility.Visible;
        }
    }
}