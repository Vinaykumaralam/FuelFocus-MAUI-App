using FuelFocus.ViewModel;

namespace FuelFocus.View;

public partial class RoutePage : ContentPage
{
    private bool _isDisposed = false;
    private RouteViewModel viewModel;
    public RoutePage()
    {
        InitializeComponent();
        viewModel = BindingContext as RouteViewModel;
        viewModel?.SetWebView(MapWebView);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        MapWebView.Source = "MapLeaf.html";
        MapWebView.Navigated += MapWebView_Navigated;
        ClearEntryFields();
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _isDisposed = true;
        MapWebView.Navigated -= MapWebView_Navigated;
        MapWebView.Source = null;
        MapWebView.Handler?.DisconnectHandler();
    }
    private void ClearEntryFields()
    {
        SourceEntry.Text = string.Empty;
        DestinationEntry.Text = string.Empty;
    }
    private void MapWebView_Navigated(object sender, WebNavigatedEventArgs e)
    {
        if (_isDisposed)
            return;

        // Your existing navigated logic
    }

    private void OnEntryFocused(object sender, FocusEventArgs e)
    {
        SourcePicker.IsVisible = true;
    }

    private void OnEntryUnfocused(object sender, FocusEventArgs e)
    {
        if (SourcePicker.SelectedIndex == -1)
        {
            SourcePicker.IsVisible = false;
        }
    }

    private void OnSourcePickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        if (picker.SelectedItem != null && picker.SelectedItem.ToString() == "Current Location")
        {
            var viewModel = (RouteViewModel)BindingContext;
            viewModel.SetSourceToCurrentLocationCommand.Execute(null);
            SourceEntry.Text = "Current Location";
        }
        SourcePicker.IsVisible = false;
    }

}