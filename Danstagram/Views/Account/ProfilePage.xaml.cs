using Danstagram.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Danstagram.Views.Account
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		#region Constructors
		public ProfilePage ()
		{
			InitializeComponent ();
			this.BindingContext = new ProfileViewModel();
		}
		#endregion
	}
}