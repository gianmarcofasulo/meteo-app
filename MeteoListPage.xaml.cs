using MeteoApp.Services;

namespace MeteoApp;

public partial class MeteoListPage : Shell
{
    public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();
    public List<Models.List> WeatherList;
    private double latitude;
    private double longitude;

    public MeteoListPage()
	{
		InitializeComponent();

        BindingContext = new MeteoListViewModel();
        WeatherList = new List<Models.List>();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await GetLocation();
        await GetWeatherDataByLocation(latitude,longitude);
    }

    public async Task GetLocation()
    {
        var location = await Geolocation.GetLocationAsync();
        latitude = location.Latitude;
        longitude = location.Longitude;
    }
    
    private async void OnItemLocation(object sender, EventArgs e)
    {
        await GetLocation();
        await GetWeatherDataByLocation(latitude, longitude);
    }

    public async Task GetWeatherDataByLocation(double latitude, double longitude)
    {
        var result = await ApiService.GetWeatherData(latitude, longitude);

        foreach (var item in result.list)
        {
            WeatherList.Add(item);
        }
        CvWeather.ItemsSource = WeatherList;

        LblCity.Text = result.city.name;
        LblWheatherDesc.Text = result.list[0].weather[0].description;
        LblTemperature.Text = result.list[0].main.temperature + "°";
        LblHumidity.Text = result.list[0].main.humidity + "%";
        LblWind.Text = result.list[0].wind.speed + "km/h";
        ImgWeatherIcon.Source = result.list[0].weather[0].customIcon;
    }
    private async void OnItemAdded(object sender, EventArgs e)
    {
        // Creare una nuova pagina per l'aggiunta della città
        var addCityPage = new AddCityPage();

    private void OnItemAdded(object sender, EventArgs e)
    {
        _ = ShowPrompt();
    }

    private async Task ShowPrompt()
    {
        await DisplayAlert("Feature", "To Be Implemented", "OK");
    }
   
}