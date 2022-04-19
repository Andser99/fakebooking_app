using MobileBooking.Models;
using MobileBooking.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileBooking.ViewModels
{
    
    class StoriesViewModel
    {
        public ObservableCollection<StoriesItem> Stories { get; set; }
        public Command LoadItemsCommand { get; set; }

        public StoriesViewModel()
        {
            Stories = new ObservableCollection<StoriesItem>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            try
            {
                Stories.Clear();
                //var items = await DataStore.GetItemsAsync(true);
                var items = await RestService.GetStories();
                if (items != null)
                {
                    foreach (var item in items)
                    { 
                        item.created_at = item.created_at.Substring(0, 10);
                        Stories.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
