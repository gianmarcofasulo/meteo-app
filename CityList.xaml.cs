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
            var swipeView = new SwipeView();

            var button = new Button
            {
                Text = entry.Name,
            };

            // Gestisci l'evento SwipeEnded per eliminare la città
            swipeView.SwipeEnded += async (s, args) =>
            {
                database.DeleteEntry(entry);
                // Aggiorna la lista delle città dopo l'eliminazione
                GetEntries();
            };

            //onclicked event
            button.Clicked += async (s, args) =>
            {
                var cityName = ((Button)s).Text;
                await Navigation.PushAsync(new MainPage(cityName));
            };

            swipeView.Content = button;

            // Aggiungi il SwipeView al contenitore
            EntryButtonsContainer.Children.Add(swipeView);
        }
    }
}
