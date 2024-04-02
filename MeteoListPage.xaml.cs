using MeteoApp.Services;

namespace MeteoApp;

public partial class MeteoListPage : Shell
{
    public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();
    public List<Models.List> WeatherList;

    public MeteoListPage()
	{
		InitializeComponent();
        RegisterRoutes();

        BindingContext = new MeteoListViewModel();
        WeatherList = new List<Models.List>();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var result = await ApiService.GetWeatherData(45.820599, 8.825058);
        
        foreach (var item in result.list)
        {
            WeatherList.Add(item);
        }
        CvWeather.ItemsSource = WeatherList;
        
        LblCity.Text = result.city.name;
        LblWheatherDesc.Text = result.list[0].weather[0].description;
        LblTemperature.Text  = result.list[0].main.temperature + "°";
        LblHumidity.Text  = result.list[0].main.humidity + "%";
        LblWind.Text  = result.list[0].wind.speed + "km/h";
        ImgWeatherIcon.Source  = result.list[0].weather[0].customIcon;
    }

    public async getLocation

    private void RegisterRoutes()
    {
        Routes.Add("entrydetails", typeof(MeteoItemPage));

        foreach (var item in Routes)
            Routing.RegisterRoute(item.Key, item.Value);
    }

    private void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            Entry entry = e.SelectedItem as Entry;

            var navigationParameter = new Dictionary<string, object>
            {
                { "Entry", entry }
            };

            Shell.Current.GoToAsync($"entrydetails", navigationParameter);
        }
    }

    private void OnItemAdded(object sender, EventArgs e)
    {
         _ = ShowPrompt();
    }

    private async Task ShowPrompt()
    {
        await DisplayAlert("Add City", "To Be Implemented", "OK");
    }
}