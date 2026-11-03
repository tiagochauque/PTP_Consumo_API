using System.Text.Json;

class Program
{
    static async Task Main()
    {
        using HttpClient client = new HttpClient();
        try
        {
            string url = "https://dummyjson.com/quotes/random";
            HttpResponseMessage resposta = await client.GetAsync(url);
            resposta.EnsureSuccessStatusCode();

            string json = await resposta.Content.ReadAsStringAsync();
            Citacao? c = JsonSerializer.Deserialize<Citacao>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Console.WriteLine($"Citação: {c?.Quote}");
            Console.WriteLine($"Autor: {c?.Author}");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erro de ligação: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro inesperado: {ex.Message}");
        }
    }
}

class Citacao
{
    public int Id { get; set; }
    public string? Quote { get; set; }
    public string? Author { get; set; }
}
