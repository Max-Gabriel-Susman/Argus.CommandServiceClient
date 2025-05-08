using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Argus.InventoryExplorer.Infrastructure;
using Argus.InventoryExplorer.Models;

namespace Argus.InventoryExplorer.ViewModels
{
    public sealed class InventoryExplorerViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<InventoryItem> InventoryItems { get; } =
            new ObservableCollection<InventoryItem>();

        private InventoryItem? _selectedItem;
        public InventoryItem? SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        public ICommand RefreshCommand { get; }
        public ICommand AddItemCommand { get; }
        public ICommand RemoveCommand { get; }

        public InventoryExplorerViewModel()
        {
            RefreshCommand = new RelayCommand(_ => LoadData());

            AddItemCommand = new RelayCommand(
                _ => AddNewItem(),
                _ => !string.IsNullOrWhiteSpace(NewItemName) && NewItemQuantity >= 0);

            RemoveCommand = new RelayCommand(
                _ => RemoveSelected(),
                _ => SelectedItem != null);

            LoadData();
        }

        void LoadData()
        {
            InventoryItems.Clear();
            InventoryItems.Add(new InventoryItem { Id = 1, Name = "Widget", Quantity = 25 });
            InventoryItems.Add(new InventoryItem { Id = 2, Name = "Gadget", Quantity = 10 });
            InventoryItems.Add(new InventoryItem { Id = 3, Name = "Doohickey", Quantity = 5 });
        }

        private string _newItemName = string.Empty;
        public string NewItemName
        {
            get => _newItemName;
            set
            {
                _newItemName = value; OnPropertyChanged();
                ((RelayCommand)AddItemCommand).RaiseCanExecuteChanged();
            }
        }

        private int _newItemQuantity;
        public int NewItemQuantity
        {
            get => _newItemQuantity;
            set
            {
                _newItemQuantity = value; OnPropertyChanged();
                ((RelayCommand)AddItemCommand).RaiseCanExecuteChanged();
            }
        }

        void AddNewItem()
        {
            int nextId = InventoryItems.Any() ? InventoryItems.Max(i => i.Id) + 1 : 1;

            InventoryItems.Add(new InventoryItem
            {
                Id = nextId,
                Name = NewItemName.Trim(),
                Quantity = NewItemQuantity
            });

            NewItemName = string.Empty;
            NewItemQuantity = 0;
        }

        void RemoveSelected()
        {
            if (SelectedItem != null)
                InventoryItems.Remove(SelectedItem);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

            if (name == nameof(SelectedItem))
                ((RelayCommand)RemoveCommand).RaiseCanExecuteChanged();
        }
    }
}
