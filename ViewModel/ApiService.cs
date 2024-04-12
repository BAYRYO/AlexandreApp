namespace AlexandreApp.ModelView;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> GetDataFromApi(string apiUrl)
    {
        try
        {
            // Envoyer une requête HTTP GET à l'API
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            // Vérifier si la réponse est réussie (200 OK)
            if (response.IsSuccessStatusCode)
            {
                // Lire le contenu de la réponse en tant que chaîne JSON
                string content = await response.Content.ReadAsStringAsync();
                return content;
            }
            else
            {
                // Gérer les erreurs de requête HTTP
                Console.WriteLine($"Erreur HTTP : {response.StatusCode}");
                return null;
            }
        }
        catch (Exception ex)
        {
            // Gérer les erreurs de connexion réseau
            Console.WriteLine($"Erreur : {ex.Message}");
            return null;
        }
    }
}