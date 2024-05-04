using MeteoApp.Services;
using Microsoft.Maui.Controls;

namespace MeteoApp;

public partial class MainPage : Shell
{
    public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();
    public List<Models.List> WeatherList;
    private double latitude;
    private double longitude;
    public static string CityName { get; set; }
    public static MainPage Current { get; private set; }

    public MainPage()
    {
        InitializeComponent();

        BindingContext = new MeteoListViewModel();
        WeatherList = new List<Models.List>();
        Current = this;
    }

    public MainPage(string cityName)
    {
        InitializeComponent();

        BindingContext = new MeteoListViewModel();
        WeatherList = new List<Models.List>();

        // Chiama GetWeatherByCity per ottenere i dati meteorologici per la città specificata
        GetWeatherByCity(cityName);
        Current = this;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await GetLocation();
        await GetWeatherDataByLocation(latitude, longitude);
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
    public void Update(string city)
    {
        if (city.Length > 0)
        {
            CityName = city;
            BindingContext = new MeteoListViewModel();
            WeatherList = new List<Models.List>();
            GetWeatherByCity(CityName);
        }
    }

    public async Task GetWeatherDataByLocation(double latitude, double longitude)
    {
        var result = await ApiService.GetWeatherData(latitude, longitude);

        foreach (var item in result.list)
        {
            WeatherList.Add(item);
        }
        CvWeather.ItemsSource = WeatherList;
        CityName = result.city.name;
        LblCity.Text = result.city.name;
        LblWheatherDesc.Text = result.list[0].weather[0].description;
        LblTemperature.Text = result.list[0].main.temperature + "°";
        LblHumidity.Text = result.list[0].main.humidity + "%";
        LblWind.Text = result.list[0].wind.speed + "km/h";
        ImgWeatherIcon.Source = result.list[0].weather[0].customIcon;
    }

    private async void GetWeatherByCity(string cityName)
    {
        if (cityName.Length == 0)
        {
            return;
        }
        var result = await ApiService.GetWeatherByCity(cityName);

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


    private async void OnShowList(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PushAsync(new CityList());
    }


    private void OnItemAdded(object sender, EventArgs e)
    {
        _ = ShowPrompt();
    }

    private async Task ShowPrompt()
    {
        await Shell.Current.Navigation.PushModalAsync(new AddPage());
    }

}