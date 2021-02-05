using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySampleApp.ViewModels;
using Xamarin.Forms;

namespace MySampleApp
{
    public partial class MainPage : BaseContentPage
    {
        private readonly MainPageViewModel _viewModel;
        public MainPage()
        {
            InitializeComponent();
            _viewModel = new MainPageViewModel();
            this.BindingContext = _viewModel;
            this.Appearing += OnAppearing;
            NavigationPage.SetHasNavigationBar(this,false);
            LoadData();
            
        }

        void LoadData()
        {
           
            Task.Run(async () => await _viewModel.LoadItems().ConfigureAwait(false));
        }
        
        private  void OnAppearing(object sender, EventArgs e)
        {
            DisplayAlert("Warning!","OnAppearing", "OK");
            LoadData();
        }
        
        private  void OnLayoutChanging(object sender, EventArgs e)
        {
            // DisplayAlert("Warning!","OnLayoutChanging", "OK");
            // LoadData();
        }

        private async void ShowButton_OnClicked(object sender, EventArgs e)
        {
            // var b = (Button) sender;
            // var item = (ListItem) b.CommandParameter;
            await DisplayAlert("Clicked",  "Button was cliked", "OK");
        }
    }
}
