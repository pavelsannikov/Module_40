using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Module_40
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            object pincode = "";
            if (App.Current.Properties.TryGetValue("pincode", out pincode))
            {
                PincodeLabel.Text = "Введите ПИН-код";
            }
            else
            {

            }
        }
        void PincodeHandler(object sender, EventArgs args)
        {
            object pincode = "";
            string pincode_string = PasswordEntry.Text;
            if (App.Current.Properties.TryGetValue("pincode", out pincode))
            {
                if (App.Current.Properties["pincode"].ToString() == pincode_string)
                {
                    Navigation.PushAsync(new ImagesPage());
                }
                else
                {
                    PincodeLabel.Text = "Неверный ПИН-код.\nПопробуйте снова";
                }
            }
            else
            {
                App.Current.Properties["pincode"] = pincode_string;
                Navigation.PushAsync(new ImagesPage());
            }
        }
    }
}
