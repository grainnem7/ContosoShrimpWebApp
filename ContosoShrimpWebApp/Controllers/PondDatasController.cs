using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;


using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using ContosoShrimpWebApp.Models;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Linq;
//using FluentAssertions.Common;

namespace ContosoShrimpWebApp.Controllers
{
    public class PondDatasController : Controller
    {
        //private readonly PondDataContext _context;


        //public PondDatasController(PondDataContext context)
        //{
        //    _context = context;
        //}

        Uri baseAddress = new Uri("https://adamtestcontosowebapi.azurewebsites.net/");
        HttpClient client;
        public PondDatasController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }




        string Baseurl = "https://adamtestcontosowebapi.azurewebsites.net/";
        public async Task<ActionResult> ShowAll()
        {
            List<PondData> PondInfo = new List<PondData>();
            List<string> stringList = new List<string>();
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
                    PondInfo = JsonConvert.DeserializeObject<List<PondData>>(PondResponse); // this is where it goes wrong!

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
        public ActionResult Create()
        {
            return View();
        }
        //Create new record
        [HttpPost]
        public ActionResult Create(PondData pond)
        {
            string data = JsonConvert.SerializeObject(pond, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "api/PondModels/", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ShowAll");
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



        public ActionResult Edit(string id)
        {
            client.DefaultRequestHeaders.Accept.Clear();


            PondData model = new PondData();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/pondModels/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<PondData>(data);
            }
            return View("Edit", model);
        }
        [HttpPost]
        public ActionResult Edit(PondData pond)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            string data = JsonConvert.SerializeObject(pond);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "api/pondModels/" + pond.Id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ShowAll");
            }
            return View("Edit", pond);
        }

        public ActionResult Delete(string id)
        {
            PondData model = new PondData();
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "api/pondModels/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<PondData>(data);
            }
            return View("Delete", model);
        }

        //[HttpPost]
        //public ActionResult Delete(PondData pond)
        //{
        //   // string data = JsonConvert.SerializeObject(pond);
        //  //  StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        //    HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/api/pondModels/" + pond.ID).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("ShowAll");
        //    }
        //    return View("Create", pond);



        //public async Task<ActionResult> Delete(string id)
        //{
        //    String requestUri = "https://adamtestcontosowebapi.azurewebsites.net/api/pondmodels/" + id;
        //    var response = await client.PostAsync(requestUri, null);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("ShowAll");
        //    }
        //    return RedirectToAction("Delete");
        //}
    }
}






//[HttpDelete]
//public async ActionResult Delete(string id)


//{

//    PondData pond = null;
//    using(var client = new HttpClient())
//    {
//        //client.BaseAddress = new Uri(apiBaseAddress);

//        var result = await client.GetAsync(client.BaseAddress + "/api/pondModels/" + id);

//        if (result.IsSuccessStatusCode)
//        {
//            pond = await result.Content.ReadAsStringAsync<PondData>();
//        }
//        else
//        {
//            ModelState.AddModelError(string.Empty, "Server error try after some time.");
//        }
//    }

//    string data = JsonConvert.SerializeObject(id);
//    HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/api/pondModels/" + id).Result;
//    if (response.IsSuccessStatusCode)
//    {
//        return RedirectToAction("ShowAll");
//    }
//    return View("Delete", id);
//}

//public ActionResult Delete(string id)
//{
//    using (var client = new HttpClient())
//    {
//        client.BaseAddress = new Uri("http://localhost:64189/api/");

//        //HTTP DELETE
//        var deleteTask = client.DeleteAsync("student/" + id.ToString());
//        deleteTask.Wait();

//        var result = deleteTask.Result;
//        if (result.IsSuccessStatusCode)
//        {

//            return RedirectToAction("Index");
//        }
//    }

//    return RedirectToAction("Index");
//}



//[HttpPost]
//public ActionResult Delete(PondData pond)
//{
//    string data = JsonConvert.SerializeObject(pond);
//    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
//    HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/api/pondModels/" + pond.ID, content).Result;
//    if (response.IsSuccessStatusCode)
//    {
//        return RedirectToAction("ShowAll");
//    }
//    return View("Delete", pond);
//}

//public ActionResult Delete(string id)
//{
//    HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/api/pondmodels/" + id).Result;
//    if (response.IsSuccessStatusCode)
//    {
//        return RedirectToAction("ShowAll");
//    }
//    return RedirectToAction("ShowAll");
//}





















//// GET: PondDatas
//public async Task<IActionResult> Index()
//{
//    return View(await _context.Pond.ToListAsync());
//}

// GET: PondDatas/Details/5
//        public async Task<IActionResult> Details(string id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var pondData = await _context.Pond
//                .FirstOrDefaultAsync(m => m.ID == id);
//            if (pondData == null)
//            {
//                return NotFound();
//            }

//            return View(pondData);
//        }

//        // GET: PondDatas/Create
//        //public IActionResult Create()
//        //{
//        //    return View();
//        //}

//        // POST: PondDatas/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("ID,pondID,start_date,initial_population,initial_average_weight,estimated_end_date,estimated_harvest_average_weight,estimated_survival,food_type")] PondData pondData)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(pondData);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(pondData);
//        }

//        // GET: PondDatas/Edit/5
//        public async Task<IActionResult> Edit(string id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var pondData = await _context.Pond.FindAsync(id);
//            if (pondData == null)
//            {
//                return NotFound();
//            }
//            return View(pondData);
//        }

//        // POST: PondDatas/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(string id, [Bind("ID,pondID,start_date,initial_population,initial_average_weight,estimated_end_date,estimated_harvest_average_weight,estimated_survival,food_type")] PondData pondData)
//        {
//            if (id != pondData.ID)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(pondData);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!PondDataExists(pondData.ID))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(pondData);
//        }

//        // GET: PondDatas/Delete/5
//        public async Task<IActionResult> Delete(string id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var pondData = await _context.Pond
//                .FirstOrDefaultAsync(m => m.ID == id);
//            if (pondData == null)
//            {
//                return NotFound();
//            }

//            return View(pondData);
//        }

//        // POST: PondDatas/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(string id)
//        {
//            var pondData = await _context.Pond.FindAsync(id);
//            _context.Pond.Remove(pondData);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool PondDataExists(string id)
//        {
//            return _context.Pond.Any(e => e.ID == id);
//        }
//    }


