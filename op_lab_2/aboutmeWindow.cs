using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace op_lab_2
{
    class aboutmeWindow
    {
        public aboutmeWindow()
        {
            initControls();
        }

        private void initControls()
        {
            Window wn = new Window();
            wn.Title = "About Me Window";

            Grid mainGrid = new Grid();
            GridLengthConverter glConverter = new GridLengthConverter();

            RowDefinition[] rows = new RowDefinition[2];
            for(int i = 0; i < 2; i++)
            {
                rows[i] = new RowDefinition();
                rows[i].Height = (GridLength)glConverter.ConvertFrom((i+1).ToString() + "*");
                mainGrid.RowDefinitions.Add(rows[i]);        
            }

            TextBlock mainText = new TextBlock(); mainText.Text = "About Me";
            mainText.FontSize = 40; mainText.HorizontalAlignment = HorizontalAlignment.Center; mainText.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(mainText, 0);
            mainGrid.Children.Add(mainText);

            TextBlock aboutMeText = new TextBlock(); aboutMeText.Text = "Dychka O.O., KP-12\nfeb.2022";
            aboutMeText.FontSize = 40; aboutMeText.HorizontalAlignment = HorizontalAlignment.Center; aboutMeText.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(aboutMeText, 1);
            mainGrid.Children.Add(aboutMeText);

            wn.Content = mainGrid;
            wn.Show();
        }
    }
}
