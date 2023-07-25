using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfAppClientToApi
{
    class ModelLogicApi : IModel
    {
        HttpClient Client;
        static readonly string url = $@"api/Person";
        public ModelLogicApi()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(@"https://localhost:7068/");
        }

        /// <summary>
        /// Метод загрузки данных о контактах из бд
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Person> GetData()
        {
                string json = Client.GetStringAsync(url).Result;
                return JsonConvert.DeserializeObject<ObservableCollection<Person>>(json);
        }

        /// <summary>
        /// Метод добавления нового контакта в бд
        /// </summary>
        /// <param name="person"></param>
        public void AddNewPerson(Person person)
        {
            var result = Client.PostAsync(url, 
                new StringContent(
                    JsonConvert.SerializeObject(person),
                    Encoding.UTF8,
                    "application/json")
                ).Result;
        }
        /// <summary>
        /// Метод удаления контакта из БД
        /// </summary>
        /// <param name="person"></param>
        public void DeletePerson(Person person)
        {
            int id = person.Id;
            var result = Client.DeleteAsync(url + $"/{id}").Result;
        }
        /// <summary>
        /// Метод изменения контакта в БД
        /// </summary>
        /// <param name="person"></param>
        public void UpdatePerson(Person person)
        {
            string id = person.Id.ToString();
            var result = Client.PutAsync($"{url}/{id}",
                new StringContent(
                    JsonConvert.SerializeObject(person),
                    Encoding.UTF8,
                    "application/json")
                ).Result;
        }

        /// <summary>
        /// Метод проверки, имеется ли у пользователя роль администратора
        /// </summary>
        /// <param name="username">Логин пользователя</param>
        /// <returns></returns>
        public bool IsAdminUser(string username)
        {
            string urlAccount = $"api/Account/{username}";
            var result = Client.GetAsync(urlAccount).Result;
            if (result.IsSuccessStatusCode)
            {
                var otvet = result.Content.ReadAsStringAsync().Result;
                bool ret = Convert.ToBoolean(otvet);
                return ret;
            }
            return false;
        }

        /// <summary>
        /// Метод добавления нового пользователя в БД
        /// </summary>
        /// <param name="NewUser">Новый пользователь</param>
        public void AddNewUser(UserRegistration NewUser)
        {
            string urlAccount = "api/Account/Register";
            string json = JsonConvert.SerializeObject(NewUser);
            var r = Client.PostAsync(urlAccount, new StringContent(JsonConvert.SerializeObject(NewUser), 
                Encoding.UTF8, "application/json")).Result;
        }

        /// <summary>
        /// Метод проверки наличия пользователя в БД
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Login(UserModel user)
        {
            string url = "api/Account/Login";
            var r = Client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")).Result;
            if (r.IsSuccessStatusCode)
            {
                var otvet = r.Content.ReadAsStringAsync().Result;
                bool ret = Convert.ToBoolean(otvet);
                return ret;
            }
            return false;
        }
    }

    internal interface IModel
    {
        ObservableCollection<Person> GetData();
        void AddNewPerson(Person person);
        void DeletePerson(Person person);
        void UpdatePerson(Person person);
        bool IsAdminUser(string username);
        void AddNewUser(UserRegistration userRegistration);
        bool Login (UserModel user);
    }
}
