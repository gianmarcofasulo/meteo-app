using System.Collections.ObjectModel;
namespace MeteoApp
{
	public class MeteoListViewModel : BaseViewModel
	{
		ObservableCollection<Entry> _entries;

		public ObservableCollection<Entry> Entries
		{
			get { return _entries; }
			set
			{
				_entries = value;
				OnPropertyChanged();
			}
		}

		public MeteoListViewModel()
		{
			Entries = new ObservableCollection<Entry>();
		}

		public void AddEntry(string name)
		{
			var entry = new Entry
			{
				Id = Entries.Count,
				Name = name
			};

			Entries.Add(entry);

			// Notifica che la proprietà Entries è stata modificata
			OnPropertyChanged(nameof(Entries));
		}
	}
}
