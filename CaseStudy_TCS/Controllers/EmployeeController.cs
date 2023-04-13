using CaseStudy_TCS.Common;
using CaseStudy_TCS.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace CaseStudy_TCS.Controllers
{
    public class EmployeeController : Controller
    {
        public static readonly HttpClient client = new HttpClient();

        [HttpGet]
        public ActionResult GetEmployee(int page = 0)
        {
            return View();
        }

        [HttpPost]
        public async Task<string> GetEmployeeList(string sEcho, int iDisplayStart, int iDisplayLength, string sSearch)
        {
            var page = sEcho;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Utility.Token, Utility.APIToken);
            using HttpResponseMessage response = await client.GetAsync($"{Utility.APIURL}?page={page}");
            string responseBody = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<APIResponseModel>(responseBody);

            int totalRecord = model.meta.pagination.Total;
            var result = model.data.ToList().OrderByDescending(a => a.id);
            var employees = new List<EmployeeDetails>();
            if (!string.IsNullOrEmpty(sSearch))
            {
                sSearch = sSearch.ToLower();
                result = model.data.Where(a => a.Name.Contains(sSearch)).OrderBy(a => a.id).Skip(iDisplayStart).Take(iDisplayLength).ToList().OrderByDescending(a => a.id); ;
            }

            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append("{");
            sb.Append("\"page\": ");
            sb.Append(page);
            sb.Append(",");
            sb.Append("\"iTotalRecords\": ");
            sb.Append(totalRecord);
            sb.Append(",");
            sb.Append("\"iTotalDisplayRecords\": ");
            sb.Append(totalRecord);
            sb.Append(",");
            sb.Append("\"aaData\": ");
            sb.Append(JsonConvert.SerializeObject(result));
            sb.Append("}");
            return sb.ToString();
        }

        [HttpGet, ActionName("EmployeeDetails")]
        public async Task<IActionResult> EmployeeDetails(int? id)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Utility.Token, Utility.APIToken);
            using HttpResponseMessage response = await client.GetAsync($"{Utility.APIURL}/{id}");
            string responseBody = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<EmployeeDetailById>(responseBody);
            return View(model.data);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteEmployee(int? id)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Utility.Token, Utility.APIToken);
            using HttpResponseMessage response = await client.GetAsync($"{Utility.APIURL}/{id}");
            string responseBody = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<EmployeeDetailById>(responseBody);
            return View(model.data);
        }

        [HttpPost, ActionName("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Utility.Token, Utility.APIToken);
                client.BaseAddress = new Uri(Utility.APIURL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.DeleteAsync($"users/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                }
            }
            return RedirectToAction("GetEmployee");
        }

        [HttpGet]
        public IActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateEmployee(EmployeeDetails employeeDetails)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Utility.APIURL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Utility.Token, Utility.APIToken);
                HttpResponseMessage response = new HttpResponseMessage();
                response = await client.PostAsJsonAsync("users", employeeDetails).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                }
            }
            return RedirectToAction("GetEmployee");
        }

        [HttpGet]
        public async Task<IActionResult> EditEmployee(int? id)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Utility.Token, Utility.APIToken);
            using HttpResponseMessage response = await client.GetAsync($"{Utility.APIURL}/{id}");
            string responseBody = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<EmployeeDetailById>(responseBody);
            return View(model.data);
        }


        public class EmployeeObject

        {
            public int? id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string gender { get; set; }
            public string status { get; set; }
        }



        [HttpPost]
        public async Task<ActionResult> EditEmployee(EmployeeDetails employeeDetails)
        {
            var empDetail = new EmployeeObject
            {
                id = employeeDetails.id,
                name = employeeDetails.Name,
                email = employeeDetails.Email,
                gender = employeeDetails.Gender.ToLower(),
                status = employeeDetails.Status.ToLower()
            };
            string json = JsonConvert.SerializeObject(empDetail);
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Put, $"{Utility.APIURL}/{empDetail.id}");
            request.Headers.Add("Authorization", $"{Utility.Token} {Utility.APIToken}");
            var content = new StringContent(json, null, "application/json");
            request.Content = content;
            var response =  client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            var rsult = await response.Content.ReadAsStringAsync();
            return RedirectToAction("GetEmployee");
        }
    }
}
