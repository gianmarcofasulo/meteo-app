namespace MeteoApp;

public partial class CityList : ContentPage
{

    private MyDaLocationDatabase database;

   
    public CityList()
    {
        InitializeComponent();
        database = new MyDaLocationDatabase();
        OnAppearing();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        GetEntries();
    }

    private void GetEntries()
    {
        // Svuota il contenitore dei pulsanti prima di aggiungere nuovi pulsanti
        EntryButtonsContainer.Children.Clear();

        var entries = database.GetEntries();

        foreach (var entry in entries)
        {
            var button = new Button
            {
                Text = entry.Name,
            };

            button.Clicked += async (s, args) =>
            {
                var cityName = ((Button)s).Text;
                // await GetWeatherByCity(cityName);
            };

            // Aggiungi il pulsante al contenitore
            EntryButtonsContainer.Children.Add(button);
        }
    }

}


