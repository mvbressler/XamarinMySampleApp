using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace MySampleApp.ViewModels
{
    [Preserve(AllMembers = true)]
    [DataContract]
    public class MainPageViewModel: BaseViewModel
    {
        public MainPageViewModel()
        {
            IsLoading = true;
            Items = new ObservableCollection<ListItem>();
           // Xamarin.Forms.BindingBase.EnableCollectionSynchronization(Items, null, ObservableCollectionCallback);
        }
        
        public MainPageViewModel(IList<ListItem> _list)
        {
            IsLoading = true;
            Items = new ObservableCollection<ListItem>(_list);
            // Xamarin.Forms.BindingBase.EnableCollectionSynchronization(Items, null, ObservableCollectionCallback);
        }
        
        #region Properties

        [DataMember(Name = "Items")]
        public ObservableCollection<ListItem> Items { get; set; }

        #endregion
        
        
        #region PublicMethods

        public async Task LoadItems(bool force = false)
        {

            try
            {
                IsLoading = true;
                while (this.Items.Count > 0)
                {
                    this.Items.RemoveAt(0);
                }
                
                Items.Add(new ListItem
                    {Title = "TabbedPage", Description = "TabbedPage based on Childs", ViewName = "InlineTabbedPage"});
                
                NotifyPropertyChanged(nameof(Items));
               
                
            }
            finally
            {
                IsLoading = false;
            }
        }

        #endregion
        
        void ObservableCollectionCallback(IEnumerable collection, object context, Action accessMethod, bool writeAccess)
        {
            // `lock` ensures that only one thread access the collection at a time
            lock (collection)
            {
                accessMethod?.Invoke();
            }
        }
    }
}