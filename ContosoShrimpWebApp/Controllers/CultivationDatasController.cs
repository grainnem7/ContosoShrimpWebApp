using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using ContosoShrimpWebApp.Data;
using ContosoShrimpWebApp.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;

namespace ContosoShrimpWebApp.Controllers
{
    public class CultivationDatasController : Controller
    {
        //private readonly CultivationDataContext _context;


        //public PondDatasController(PondDataContext context)
        //{
        //    _context = context;
        //}

        Uri baseAddress = new Uri("https://cultivationapi.azurewebsites.net/");
        HttpClient client;
        public CultivationDatasController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }




        string Baseurl = "https://cultivationapi.azurewebsites.net/";
        public async Task<ActionResult> CShowAll()
        {
            List<CultivationData> PondInfo = new List<CultivationData>();

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource Get using HttpClient
                HttpResponseMessage Res = await client.GetAsync(client.BaseAddress + "api/PondModels");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var PondResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Pondlist
                    PondInfo = JsonConvert.DeserializeObject<List<CultivationData>>(PondResponse); // this is where it goes wrong!

                }
                //returning the Pond list to view
                return View(PondInfo);
            }
        }



        //public ActionResult ShowAll()
        //{
        //    List<PondData> modelList = new List<PondData>();
        //    HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/api/PondModels").Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        string data = response.Content.ReadAsStringAsync().Result;
        //        modelList = JsonConvert.DeserializeObject<List<PondData>>(data);
        //    }
        //    return View(modelList);
        //}
        public ActionResult CCreate()
        {
            return View();
        }
        //Create new record
        [HttpPost]
        public ActionResult CCreate(CultivationData pond)
        {
            string data = JsonConvert.SerializeObject(pond, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "api/PondModels/", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("CShowAll");
            }
            return View();
        }



        //static async Task<PondData> UpdateProductAsync(PondData pond)
        //{
        //    HttpResponseMessage response = client.PutAsJsonAsync(
        //        "$/api/pondModels/{pond.ID}", pond);
        //    response.EnsureSuccessStatusCode();

        //    // Deserialize the updated product from the response body.
        //    pond = await response.Content.ReadAsAsync<PondData>();
        //    return pond;
        //}



        public ActionResult CEdit(string id)
        {
            client.DefaultRequestHeaders.Accept.Clear();


            CultivationData model = new CultivationData();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/pondModels/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<CultivationData>(data);
            }
            return View("CEdit", model);
        }
        [HttpPost]
        public ActionResult CEdit(CultivationData pond)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            string data = JsonConvert.SerializeObject(pond);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "api/pondModels/" + pond.Id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("CShowAll");
            }
            return View("CEdit", pond);
        }

        public ActionResult CDelete(string id)
        {
            CultivationData model = new CultivationData();
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "api/pondModels/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<CultivationData>(data);
            }
            return View("CDelete", model);
        }

        public ActionResult CDeleteQuestion(string id)
        {
            client.DefaultRequestHeaders.Accept.Clear();


            CultivationData model = new CultivationData();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/pondModels/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<CultivationData>(data);
            }
            return View("CDeleteQuestion", model);
        }
    }
}
