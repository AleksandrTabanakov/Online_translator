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
       
        public PageTwo()
        {
            InitializeComponent();
            text.Text = string.Join("\n", MainPage.translator2);
    //      await Clipboard.SetTextAsync();
            MainPage.translator2.Clear();
        }
        private async void Button_Clicked2(object sender, EventArgs e)
        {
            await  Clipboard.SetTextAsync(text.Text);
        }

    }
}