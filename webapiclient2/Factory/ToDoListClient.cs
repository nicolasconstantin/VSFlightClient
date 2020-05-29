using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapiclient2.Models;

namespace webapiclient2
{
    public partial class ApiClient
    {
        public async Task<List<TodoItem>> GetTodoItems()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "TodoItems/"));
            return await GetAsync<List<TodoItem>>(requestUrl);
        }

        public async Task<Message<TodoItem>> SaveTodoItem(TodoItem model)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "TodoItems/"));
            return await PostAsync<TodoItem>(requestUrl, model);
        }

        public async Task<List<Flights>> GetFlights()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "flightsets/"));
            return await GetAsync<List<Flights>>(requestUrl);
        }
    }
}
