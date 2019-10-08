using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Admin.Models;

namespace Admin.Services
{
    public class CustomerService
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUrl;

        public CustomerService(HttpClient httpClient, IOptionsSnapshot<HttpServiceOptions> httpServiceOptions)
        {
            if (httpServiceOptions == null)
            {
                throw new ArgumentNullException(nameof(httpServiceOptions));
            }

            this.baseUrl = httpServiceOptions.Get("CustomerOptions").BaseUrl;
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<CustomerViewModel>> GetCustomers()
        {
            try
            {
                using (var response = await this.httpClient.GetAsync(this.baseUrl))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var jobject = JObject.Parse(responseBody);
                    var jtoken = jobject["customers"];
                    return jtoken.Select(MapCustomer);
                }
            }
            catch (Exception exception)
            {
                throw new CustomerException(this.baseUrl, exception);
            }
        }

        public async Task<CustomerViewModel> GetCustomer(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var url = new Uri(this.baseUrl, id);

            try
            {
                using (var response = await this.httpClient.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var jobject = JObject.Parse(responseBody);
                    return MapCustomer(jobject);
                }
            }
            catch (Exception exception)
            {
                throw new CustomerException(url, exception);
            }
        }

        public async Task<CustomerViewModel> UpdateCustomer(CustomerViewModel customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            var url = new Uri(this.baseUrl, customer.Id);

            try
            {
                var json = JsonConvert.SerializeObject(customer);
                var content = new StringContent(json);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                using (var response = await this.httpClient.PutAsync(url, content))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var jobject = JObject.Parse(json);
                    return MapCustomer(jobject);
                }
            }
            catch (Exception exception)
            {
                throw new CustomerException(url, exception);
            }
        }

        private static CustomerViewModel MapCustomer(JToken jtoken)
        {
            var id = jtoken["id"]?.ToString();
            var name = jtoken["name"]?.ToString();
            var birthDate = jtoken["birthDate"]?.ToString();
            var email = jtoken["email"]?.ToString();
            var ssn = jtoken["ssn"]?.ToString();
            var phone = jtoken["phone"]?.ToString();
            var userName = jtoken["userName"]?.ToString();
            var website = jtoken["website"]?.ToString();

            var customer = new CustomerViewModel
            {
                Id = id,
                Name = name,
                BirthDate = birthDate,
                Email = email,
                Ssn = ssn,
                Phone = phone,
                UserName = userName,
                Website = website
            };

            var address = jtoken["address"];
            if (address != null)
            {
                var street = address["street"]?.ToString();
                var zipCode = address["zipCode"]?.ToString();
                var city = address["city"]?.ToString();
                var state = address["state"]?.ToString();
                customer.Address = new AddressViewModel
                {
                    Street = street,
                    ZipCode = zipCode,
                    City = city,
                    State = state
                };
            }

            return customer;
        }

        private class CustomerException : ApplicationException
        {
            public CustomerException(Uri requestUrl, Exception innerException)
                : base($"The customer operation request to <{requestUrl}> failed.", innerException)
            {
            }
        }
    }
}
