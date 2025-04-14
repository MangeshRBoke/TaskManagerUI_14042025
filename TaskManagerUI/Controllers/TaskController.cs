using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TaskManagerUI.Models;

namespace TaskManagerUI.Controllers
{
    public class TaskController : Controller
    {
        private readonly HttpClient _httpClient;


        public TaskController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("TaskAPI");
        }

        //public TaskController(HttpClient httpClient)
        //{
        //    _httpClient = httpClient;
        //    _httpClient.BaseAddress = new Uri("https://localhost:7271");
        //}

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync($"/api/Tasks/GetTasks");
            var json = await response.Content.ReadAsStringAsync();
            var tasks = JsonConvert.DeserializeObject<List<TaskItem>>(json);
            return View(tasks);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(TaskItem task)
        {
            var content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/tasks/CreateTask", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            return View(task);
        }

        //public async Task<IActionResult> Edit(int id)
        //{
        //    var res = await _httpClient.GetAsync($"/{id}");
        //    var json = await res.Content.ReadAsStringAsync();
        //    var task = JsonConvert.DeserializeObject<TaskItem>(json);
        //    return View(task);
        //}

        public async Task<IActionResult> Edit(int id)
        {
           // var response = await _httpClient.GetAsync($"/{id}");

            var response = await _httpClient.GetAsync($"/api/Tasks/GetTasks");
            var json = await response.Content.ReadAsStringAsync();
            var tasks = JsonConvert.DeserializeObject<List<TaskItem>>(json);
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                //var jsonData = await response.Content.ReadAsStringAsync();
                //var task = JsonConvert.DeserializeObject<TaskItem>(jsonData);
                return View(task);
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> Edit(TaskItem task)
        {
            var content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/api/tasks/UpdateTask", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            return View(task);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var res = await _httpClient.GetAsync($"/api/task/GetTaskById/{id}");
            var json = await res.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<TaskItem>(json);
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _httpClient.DeleteAsync($"api/task/DeleteTask/{id}");
            return RedirectToAction("Index");
        }
    }
}
