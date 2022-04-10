using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeBookingApp.Models;

namespace FakeBookingApp.Services
{
    public class MockDataStore : IDataStore<HotelItem>
    {
        List<HotelItem> items;

        public MockDataStore()
        {
            items = new List<HotelItem>();
            var mockItems = new List<HotelItem>
            {
                new HotelItem { id = Guid.NewGuid().ToString(), name = "First item", info="This is an item description.", image_path="https://colourlex.com/wp-content/uploads/2021/02/Chrome-red-painted-swatch-N-300x300.jpg" },
                new HotelItem { id = Guid.NewGuid().ToString(), name = "Second item", info="This is an item description." },
                new HotelItem { id = Guid.NewGuid().ToString(), name = "Third item", info="This is an item description." },
                new HotelItem { id = Guid.NewGuid().ToString(), name = "Fourth item", info="This is an item description." },
                new HotelItem { id = Guid.NewGuid().ToString(), name = "Fifth item", info="This is an item description." },
                new HotelItem { id = Guid.NewGuid().ToString(), name = "Sixth item", info="This is an item description." },
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(HotelItem item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(HotelItem item)
        {
            var oldItem = items.Where((HotelItem arg) => arg.id == item.id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((HotelItem arg) => arg.id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<HotelItem> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.id == id));
        }

        public async Task<IEnumerable<HotelItem>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}