using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoFy.Mobile.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region Fields

        private string _title = string.Empty;
        private bool _isBusy;

        #endregion

        #region Properties

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler? PropertyChanged;
        
        #endregion

        #region Methods

        protected bool SetProperty<T>(
            ref T backingStore,
            T value,
            [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
