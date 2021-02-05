using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace MySampleApp
{
     public class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The PropertyChanged event occurs when changing the value of property.
        /// </summary>
        /// <param name="propertyName">The PropertyName</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Validation

        //https://hub.packtpub.com/how-to-implement-data-validation-with-xamarin-forms/

        readonly IDictionary<string, List<string>> _errors =
            new Dictionary<string, List<string>>();

      

        protected void Validate(Func<bool> rule, string error,
            [CallerMemberName] string propertyName = "")
        {
            if (string.IsNullOrWhiteSpace(propertyName)) return;

            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
            }

            if (rule() == false)
            {
                _errors.Add(propertyName, new List<string> {error});
            }

            // OnPropertyChanged(nameof(HasErrors)); 
            //
            // ErrorsChanged?.Invoke(this,  
            //     new DataErrorsChangedEventArgs(propertyName)); 
        }

        public bool HasErrors =>
            _errors?.Any(x => x.Value?.Any() == true) == true;

        public string ErrorsAsString => string.Join(Environment.NewLine, _errors.Values.SelectMany(x => x).ToList());

        #endregion
        
        private bool _isInlineUpdateReady;
        private bool _isSelected;

        [JsonIgnore]
        public bool IsInlineUpdateReady
        {
            get => _isInlineUpdateReady;
            set
            {
                
                _isInlineUpdateReady = value;
                NotifyPropertyChanged(nameof(IsInlineUpdateReady));
            }
        }
        
        [JsonIgnore]
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                NotifyPropertyChanged(nameof(IsSelected));
            }
        }
    }
}