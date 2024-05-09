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

        // Verifica la validit� del nome della citt�
        if (IsValidCity(cityName))
        {
            // Salva la citt� solo se � valida
            SaveCity(cityName);
            CityEntry.Unfocus(); // Chiude la tastiera
            GoBack(); // Torna alla pagina precedente
        }
        else
        {
            // Mostra un messaggio di errore se il nome della citt� non � valido
            DisplayAlert("Errore", "Inserisci un nome di citt� valido.", "OK");
        }
    }

    private bool IsValidCity(string cityName)
    {
        // Controllo se il nome della citt� non � vuoto
        if (string.IsNullOrWhiteSpace(cityName))
        {
            return false;
        }

        // Controllo se il nome della citt� contiene solo caratteri alfabetici e spazi
        foreach (char c in cityName)
        {
            if (!char.IsLetter(c) && c != ' ')
            {
                return false;
            }
        }

        // Il nome della citt� � valido
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
