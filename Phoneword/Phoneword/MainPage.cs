using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Phoneword
{
    public class MainPage : ContentPage

    {
        // define UI elements
        Entry phoneNumberText;
        Button translateButton;
        Button callButton;

        string translatedNumber;
        public MainPage()
        {

            // define Page padding
            this.Padding = new Thickness(20, 20, 20, 20);

            // define StackLayout panel with Spacing
            StackLayout panel = new StackLayout
            {
                Spacing = 15
            };

            // create and add UI elements in layout

            panel.Children.Add(new Label

            {
                Text = "Enter a Phoneword:",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            });

            panel.Children.Add(phoneNumberText = new Entry
            {
                Text = "1-855-XAMARIN",
            });

            panel.Children.Add(translateButton = new Button
            {
                Text = "Translate"
            });

            panel.Children.Add(callButton = new Button
            {
                Text = "Call",
                IsEnabled = false,
            });

            // Asign StackLayout to the Page's Content
            this.Content = panel;

            // Assign clicked action to Button
            translateButton.Clicked += OnTranslate;

            // Subscribe a handler to the Clicked event of the Call Button
            callButton.Clicked += OnCall;


        }


        void OnTranslate(object obj, EventArgs args)
        {
            String phoneNumber = phoneNumberText.Text;

            translatedNumber = Core.PhonewordTranslator.ToNumber(phoneNumber);

            if (!string.IsNullOrEmpty(translatedNumber))
            {
                callButton.IsEnabled = true;
                callButton.Text = "Call " + translatedNumber;
            }
            else
            {
                callButton.IsEnabled = false;
                callButton.Text = "Call";
            }
        }

        async void OnCall(object obj, EventArgs args)
        {
            bool answer =await DisplayAlert("Dia; a Number",
                "Would you like to call " + translatedNumber+"?",
                "Yes",
                "No");

            if (answer)
            {
                // Get Implementation with Dependency Service
                IDialer dialer = DependencyService.Get<IDialer>();
                if (dialer != null)
                    // Call the methd in platform
                    await dialer.DialAsync(translatedNumber);
            }
        }
    }
}
