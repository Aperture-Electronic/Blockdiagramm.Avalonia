using Avalonia.Collections;
using Avalonia.Threading;
using DynamicData;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.ViewModels
{
    public class FilterListViewModel<T> : ViewModelBase
    {
        private string filterString;
        private readonly ReadOnlyObservableCollection<T> filteredItems;
        private readonly IObservable<IChangeSet<T>> items;
        private readonly IObservable<Func<T, bool>> filterObservable;
        private T? selectedItem = default;

        public string FilterString
        {
            get => filterString;
            set => this.RaiseAndSetIfChanged(ref filterString, value);
        }

        public T? SelectedItem
        {
            get => selectedItem;
            set => this.RaiseAndSetIfChanged(ref selectedItem, value);
        }

        public ReadOnlyObservableCollection<T> FilteredItems => filteredItems;

        public delegate bool FilterDelegate(T item, string searchText);

        public FilterDelegate Filter { get; }

        public FilterListViewModel(IObservable<IChangeSet<T>> items, FilterDelegate filter)
        {
            Filter = filter;

            filterString = string.Empty;
            filterObservable = this.WhenAnyValue(vm => vm.FilterString)
                                   .Throttle(TimeSpan.FromMilliseconds(150))
                                   .Select(BuildFilter);

            this.items = items;
            this.items.Filter(filterObservable).ObserveOn(AvaloniaScheduler.Instance)
                .Bind(out filteredItems).Subscribe();
        }

        private Func<T, bool> BuildFilter(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return t => true;
            }

            return t => Filter(t, searchText);
        }
    }
}
