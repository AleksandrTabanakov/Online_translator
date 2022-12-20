using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
namespace Translate_program
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageTwo : ContentPage
    {
        bool flag = false;
        public PageTwo()
        {
            InitializeComponent();
           
            //      await Clipboard.SetTextAsync();
            if (MainPage.translator2.Count > 1 && MainPage.translator2[1] == "Rules")
            {
                Button2.Text = "Понятно";
                flag = true;
                MainPage.translator2.RemoveAt(1);
            } 
            text.Text = string.Join("\n", MainPage.translator2);
            MainPage.translator2.Clear();
        }
        private async void Button_Clicked2(object sender, EventArgs e)
        {
          
            if (!flag)
            {
                await Clipboard.SetTextAsync(text.Text);
            }
            else
            {
                await Navigation.PopAsync();
            }
            
        }

    }
}