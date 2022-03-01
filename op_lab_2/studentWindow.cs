using System;
using System.Linq;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace op_lab_2
{
    class studentWindow
    {
        static string inputLine = String.Empty;
        static string path = @"C:\temp_op\opLab2textFile.txt";
        static int counter;

        public studentWindow()
        {
            initControls();

            if (File.Exists(path))
                counter = Int32.Parse(File.ReadLines(path).Last().Split(' ')[0]) + 1;
        }

        private void initControls()
        {
            Window wn = new Window();
            wn.Title = "Student Window";

            Grid mainGrid = new Grid();

            GridLengthConverter glConverter = new GridLengthConverter();

            RowDefinition[] rows = new RowDefinition[3]; 
            for (int i = 0; i < 3; i++)
            {
                rows[i] = new RowDefinition();
                rows[i].Height = (GridLength)glConverter.ConvertFrom("*");
                mainGrid.RowDefinitions.Add(rows[i]);
            }

            //sub-grid, скобки для того чтобы сабгрид существовал только в их контексте и не путался
            {
                Grid r2Grid = new Grid();

                ColumnDefinition[] cols_r2 = new ColumnDefinition[2];
                for (int i = 0; i < 2; i++)
                {
                    cols_r2[i] = new ColumnDefinition();
                    cols_r2[i].Width = (GridLength)glConverter.ConvertFrom("*");
                    r2Grid.ColumnDefinitions.Add(cols_r2[i]);
                }

                Button loadButton = new Button(); loadButton.Click += loadButton_Click;
                loadButton.FontSize = 40; loadButton.Content = "Load Data"; loadButton.Margin = new Thickness(15); loadButton.Background = new SolidColorBrush(Colors.LightGreen);
                Grid.SetColumn(loadButton, 0);            
                r2Grid.Children.Add(loadButton);

                Button deleteButton = new Button(); deleteButton.Click += deleteButton_Click;
                deleteButton.FontSize = 40; deleteButton.Content = "Delete Data"; deleteButton.Margin = new Thickness(15); deleteButton.Background = new SolidColorBrush(Colors.BlueViolet);
                Grid.SetColumn(deleteButton, 1);
                r2Grid.Children.Add(deleteButton);

                Grid.SetRow(r2Grid, 2);
                mainGrid.Children.Add(r2Grid);             
            }

            TextBox inputBox = new TextBox();
            inputBox.FontSize = 40; inputBox.Margin = new Thickness(15); inputBox.TextChanged += inputBox_TextChanged;
            Grid.SetRow(inputBox, 1);
            mainGrid.Children.Add(inputBox);

            TextBlock mainText = new TextBlock(); mainText.Text = "Student Manager";
            mainText.FontSize = 40; mainText.HorizontalAlignment = HorizontalAlignment.Center; mainText.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(mainText, 0);
            mainGrid.Children.Add(mainText);
            
            wn.Content = mainGrid;
            wn.Show();
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(inputLine))
            {
                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        counter = 1;
                        sw.WriteLine(counter + " " + inputLine);
                        counter++;
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(counter + " " + inputLine);
                        counter++;
                    }
                }
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(path))
            {
                //записываем в другой файл все строки, кроме той, которая начинается с удаляемого номера
                string tempFile = System.IO.Path.GetTempFileName();

                var linesToKeep = File.ReadLines(path).Where(line => line.Split(' ')[0] != inputLine);

                File.WriteAllLines(tempFile, linesToKeep);

                //удаляем основной файл и заменяем его другим
                File.Delete(path);
                File.Move(tempFile, path);
            }
        }

        private void inputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            inputLine = txtBox.Text;
        }
    }
}
