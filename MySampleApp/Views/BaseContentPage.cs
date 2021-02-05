using MySampleApp.ViewModels;
using Syncfusion.SfBusyIndicator.XForms;
using Syncfusion.XForms.PopupLayout;
using Xamarin.Forms;

namespace MySampleApp
{
    public class BaseContentPage : ContentPage
    {
        private SfPopupLayout _loadingPopup;
        private SfPopupLayout _errorPopup;
        private Label _errorLabel;

        internal void InitErrorPopup(BaseViewModel viewModel)
        {
            DataTemplate contentTemplateView = new DataTemplate(() =>
            {
                //Using a stack layout here breaks stuff....
                // StackLayout stack = new StackLayout();
                // stack.Padding = new Thickness(15);
                // stack.WidthRequest = 100;

                _errorLabel = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    Margin = new Thickness(15)
                };
                _errorLabel.SetBinding(Label.TextProperty, "ErrorMessage", BindingMode.TwoWay);
                _errorLabel.BindingContext = viewModel;
                
                //stack.Children.Add(_errorPopup);
                return _errorLabel;
            });

            _errorPopup = new SfPopupLayout
            {
                PopupView =
                {
                    AutoSizeMode = AutoSizeMode.Height,
                    ContentTemplate = contentTemplateView,
                    HeaderTitle = "Error"
                }
            };
            _errorPopup.PopupView.ShowHeader = false;
            _errorPopup.SetBinding(SfPopupLayout.IsOpenProperty, "ShowError", BindingMode.TwoWay);
            _errorPopup.BindingContext = viewModel;
        }

        internal void InitLoadingPopup(BaseViewModel viewModel)
        {
            DataTemplate contentTemplateView = new DataTemplate(() =>
            {
                StackLayout stack = new StackLayout();
                stack.Padding = new Thickness(15);
                var busyIndicator = new SfBusyIndicator();
                busyIndicator.AnimationType = AnimationTypes.Cupertino;
                busyIndicator.Title = "Working...";
                busyIndicator.TextColor = Color.Gray;
                busyIndicator.ViewBoxHeight = 80;
                busyIndicator.ViewBoxWidth = 80;
                busyIndicator.IsBusy = true;
                stack.Children.Add(busyIndicator);
                return stack;
            });

            _loadingPopup = new SfPopupLayout {PopupView = {ShowFooter = false}, StaysOpen = true};
            _loadingPopup.PopupView.ShowHeader = false;
            _loadingPopup.PopupView.AutoSizeMode = AutoSizeMode.Both;
            _loadingPopup.PopupView.ContentTemplate = contentTemplateView;

            _loadingPopup.SetBinding(SfPopupLayout.IsOpenProperty, "IsLoading");
            _loadingPopup.BindingContext = viewModel;
        }
    }
}