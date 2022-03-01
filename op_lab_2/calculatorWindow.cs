using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace op_lab_2
{
    class calculatorWindow
    {
        TextBlock inputBox = new TextBlock();

        public calculatorWindow()
        {
            initControls();
        }

        private void initControls()
        {
            string[,] symbArr = new string[4,4] { { "1", "2", "3", "+" },
                                                  { "4", "5", "6", "-" },
                                                  { "7", "8", "9", "*" },
                                                  { "", "0", ".", "/"} };
            Window wn = new Window();
            wn.Title = "Calculus";
            wn.Height = 600; wn.Width = 450;

            Grid mainGrid = new Grid();
            GridLengthConverter glConverter = new GridLengthConverter();

            RowDefinition rd0 = new RowDefinition();
            rd0.Height = (GridLength)glConverter.ConvertFrom("*");
            mainGrid.RowDefinitions.Add(rd0);

            RowDefinition rd1 = new RowDefinition();
            rd1.Height = (GridLength)glConverter.ConvertFrom("4*");
            mainGrid.RowDefinitions.Add(rd1);

            {
                Grid subGrid = new Grid();

                RowDefinition[] rows = new RowDefinition[5];
                for (int i = 0; i < 5; i++)
                {
                    rows[i] = new RowDefinition();
                    rows[i].Height = (GridLength)glConverter.ConvertFrom("*");
                    subGrid.RowDefinitions.Add(rows[i]);
                }

                ColumnDefinition[] cols = new ColumnDefinition[4];
                for (int i = 0; i < 4; i++)
                {
                    cols[i] = new ColumnDefinition();
                    cols[i].Width = (GridLength)glConverter.ConvertFrom("*");
                    subGrid.ColumnDefinitions.Add(cols[i]);
                }

                Button equalsButton = new Button(); equalsButton.Click += equalsButton_Click;
                equalsButton.Content = "="; equalsButton.FontSize = 30; equalsButton.Background = new SolidColorBrush(Colors.Orange);
                Grid.SetRow(equalsButton, 0); Grid.SetColumn(equalsButton, 1);
                subGrid.Children.Add(equalsButton);

                Button deleteButton = new Button(); deleteButton.Click += deleteButton_Click;
                deleteButton.Content = "<"; deleteButton.FontSize = 30;
                Grid.SetRow(deleteButton, 0); Grid.SetColumn(deleteButton, 2);
                subGrid.Children.Add(deleteButton);

                Button clearButton = new Button(); clearButton.Click += clearButton_Click;
                clearButton.Content = "C"; clearButton.FontSize = 30;
                Grid.SetRow(clearButton, 0); Grid.SetColumn(clearButton, 3);
                subGrid.Children.Add(clearButton);

                Button[,] numbersArr = new Button[4, 4];
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                    {
                        numbersArr[i, j] = new Button();
                        numbersArr[i, j].Click += inputButton_Click;
                        numbersArr[i, j].Content = symbArr[i, j];
                        numbersArr[i, j].FontSize = 30;

                        Grid.SetRow(numbersArr[i, j], i + 1);
                        Grid.SetColumn(numbersArr[i, j], j);

                        subGrid.Children.Add(numbersArr[i, j]);
                    }
                subGrid.Children.Remove(numbersArr[3, 0]);

                Grid.SetRow(subGrid, 1);
                mainGrid.Children.Add(subGrid);
            }

            inputBox.FontSize = 45; inputBox.TextAlignment = TextAlignment.Right;
            Grid.SetRow(inputBox, 0);
            mainGrid.Children.Add(inputBox);

            wn.Content = mainGrid;
            wn.Show();
        }

        private void equalsButton_Click(object sender, RoutedEventArgs e)
        {
            var res = new DataTable().Compute(inputBox.Text, null);

            inputBox.Text = res.ToString().Replace(',', '.');
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            inputBox.Text = inputBox.Text.Remove(inputBox.Text.Length - 1);
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            inputBox.Text = String.Empty;
        }

        private void inputButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            inputBox.Text += btn.Content.ToString();
        }
    }
}
