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

        // Verifica la validità del nome della città
        if (IsValidCity(cityName))
        {
            // Salva la città solo se è valida
            SaveCity(cityName);
            CityEntry.Unfocus(); // Chiude la tastiera
            GoBack(); // Torna alla pagina precedente
        }
        else
        {
            // Mostra un messaggio di errore se il nome della città non è valido
            DisplayAlert("Errore", "Inserisci un nome di città valido.", "OK");
        }
    }

    private bool IsValidCity(string cityName)
    {
        // Controllo se il nome della città non è vuoto
        if (string.IsNullOrWhiteSpace(cityName))
        {
            return false;
        }

        // Controllo se il nome della città contiene solo caratteri alfabetici e spazi
        foreach (char c in cityName)
        {
            if (!char.IsLetter(c) && c != ' ')
            {
                return false;
            }
        }

        // Il nome della città è valido
        return true;
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
