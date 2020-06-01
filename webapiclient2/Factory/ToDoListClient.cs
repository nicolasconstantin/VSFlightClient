﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        public async Task<Message<Bookings>> BuyOneTicket(Bookings model)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "BookingSets/"));
            return await PostAsync<Bookings>(requestUrl, model);
        }

        public async Task<List<Flights>> GetFlights()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "flightsets/"));
            return await GetAsync<List<Flights>>(requestUrl);
        }

        public async Task<Flights> GetFlight(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "flightsets/" + id));
            return await GetAsync<Flights>(requestUrl);
        }

        public async Task<List<Bookings>> GetBookings()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "bookingsets/"));
            return await GetAsync<List<Bookings>>(requestUrl);
        }

        public async Task<List<Passengers>> GetPassengers()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "passengersets/"));
            
            return await GetAsync<List<Passengers>>(requestUrl);

        }
    }
}
