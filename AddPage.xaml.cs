namespace MeteoApp;

public partial class AddPage : ContentPage
{
    private readonly MyDaLocationDatabase _database;

    public AddPage()
    {
        InitializeComponent();
        _database = new MyDaLocationDatabase();
    }
    private void OnAddCityClicked(object sender, EventArgs e)
    {
        string cityName = CityEntry.Text;
        SaveCity(cityName);
        GoBack();
    }

    private async void GoBack()
    {
        await Shell.Current.Navigation.PopAsync();
    }

    private void SaveCity(string cityName)
    {
        var entry = new Entry { Name = cityName };
        _database.SaveEntry(entry);
    }

}
