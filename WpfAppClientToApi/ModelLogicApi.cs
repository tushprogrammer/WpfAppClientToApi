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
        public ObservableCollection<Person> GetData()
        {
            try
            {
                string json = Client.GetStringAsync(url).Result;
                return JsonConvert.DeserializeObject<ObservableCollection<Person>>(json);
            }
            catch (SocketException E)
            {
                //MessageBox.Show("Приложение Api не запущено");
            }
            catch(Exception e)
            {

            }
              
            return null;
        }
        
        public void AddNewPerson(Person person)
        {
            var result = Client.PostAsync(url, 
                new StringContent(
                    JsonConvert.SerializeObject(person),
                    Encoding.UTF8,
                    "application/json")
                ).Result;
        }
        public void DeletePerson(Person person)
        {
            int id = person.Id;
            var result = Client.DeleteAsync(url + $"/{id}");
        }
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
        public void AddNewUser(UserRegistration NewUser)
        {
            //проверка на существование такого же пользователя
            string url = "api/Account/Login";
            var r = Client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(NewUser), Encoding.UTF8, "application/json")).Result;
            bool ret = false;
            if (r.IsSuccessStatusCode)
            {
                var otvet = r.Content.ReadAsStringAsync().Result;
                ret = Convert.ToBoolean(otvet);
            }
            if (!ret) //если вход не выполнен, значит такого пользователя нет (эту проверку надо вынести отсюда раньше)
            {
                //не факт что заработает, не понятно к какому post запросу это придет
                //пока что не работает (возможно сам запрос через жепу работате)
                string urlAccount = "api/Account/Register";
                string json = JsonConvert.SerializeObject(NewUser);
                r = Client.PostAsync(urlAccount, new StringContent(JsonConvert.SerializeObject(NewUser), 
                    Encoding.UTF8, "application/json")).Result;
                if (r.IsSuccessStatusCode)
                {
                    var otvet = r.Content.ReadAsStringAsync().Result;
                    //bool ret = Convert.ToBoolean(otvet);
                }
            }
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
    }
}
