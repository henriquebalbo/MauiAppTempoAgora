namespace MauiAppTempoAgora.Models
{
    public class Tempo // troca interno para publico para chamar de qualquer lugar
                       // importando informações - API do site openweathermap.org
    {
        public double? lon { get; set; } //"?" para indicar que a propriedade pode ser nula, caso haja algum erro na requisição da API
        public double? lat { get; set; }
        public int? visibility { get; set; }
        public double? temp { get; set; }
        public string? sunrise { get; set; }
        public string? sunset { get; set; }
        public string? main { get; set; }
        public string? description { get; set; }
        public double? speed { get; set; }
        public string? country { get; set; }
        public int? timezone { get; set; }
    }



}
