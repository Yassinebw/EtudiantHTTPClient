using DAOMysqlLib.Entities;
using EtudiantHTTPClient.URL;
using Newtonsoft.Json;
using System.Text;
ajouterEtudiant();
getAllStudent();


void getAllStudent()
{
    using (var client = new HttpClient())
    {
        client.BaseAddress = new Uri("https://localhost:7042/");
        //HTTP GET
        var responseTask = client.GetAsync(ApiURL.EtudiantsSearch);//https://localhost:7042/Etudiant
        responseTask.Wait();

        var result = responseTask.Result;
        if (result.IsSuccessStatusCode)
        {

            var readTask = result.Content.ReadAsAsync<Etudiant[]>();
            readTask.Wait();

            var students = readTask.Result;

            foreach (var student in students)
            {
                Console.WriteLine(student.Name);
            }
        }

    }
}
//ajouterEtudiant();
static async Task  ajouterEtudiant()
{
    using var client = new HttpClient();
    client.BaseAddress = new Uri("https://localhost:7042/");

    var etudiant = new Etudiant(5, "Sami", 15);
    

    HttpContent body = new StringContent(JsonConvert.SerializeObject(etudiant), Encoding.UTF8, "application/json");
    var response = client.PostAsync(ApiURL.AddStudent, body).Result;

    if (response.IsSuccessStatusCode)
    {
        var content = await response.Content.ReadAsStringAsync();
        if (!string.IsNullOrEmpty(content))
        {
            var objDeserializeObject = JsonConvert.DeserializeObject<Etudiant>(content);

            Console.WriteLine("Data Saved Successfully.");

            if (objDeserializeObject != null)
            {
                Console.WriteLine(objDeserializeObject.Name);
            }
        }
    }

}
