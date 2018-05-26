using System.Threading.Tasks;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Phoneword.iOS;

[assembly: Dependency(typeof(PhoneDialerIOS))]
namespace Phoneword.iOS
{
    public class PhoneDialerIOS : IDialer
    {
        public Task<bool> DialAsync(string number)
        {
            return Task.FromResult(
                UIApplication.SharedApplication.OpenUrl(
                new NSUrl("tel:" + number))
            );
        }
    }
}