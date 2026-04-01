using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq; // importando o modelo Tempo para usar na classe DataService


namespace MauiAppTempoAgora.Services
{
    public class DataService
    {
        // adicionando o método para trazer a previsão do tempo
        public static async Task<Tempo?>  GetPrevisao(string cidade)
        {
            // task<Tempo?> para retornar um objeto do tipo Tempo ou nulo caso haja algum erro na requisição
            //criando o objeto  tempo

            Tempo? t = null; //variavel do tipo t, iniciada como nula para ser preenchida com os dados da API posteriormente

            // buscando o webService do openweathermap para trazer a previsão do tempo da cidade escolhida


            string chave = "42b5ee73239678aa80f337570e8fb588"; // chave da API do openweathermap, é necessário criar uma conta gratuita para obter a chave

            string url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&units=metric&appid={chave}&lang=pt_br"; // url da API do openweathermap, passando a cidade escolhida, a chave da API, as unidades de medida e o idioma

            // agora vamos abrir no nagegador no c# para trazer a resposta da API

            // criando o laço using para abrir a conexão com a API e trazer a resposta

            using (HttpClient client = new HttpClient())
            {
                // pegando a resposta da URL

                HttpResponseMessage resp = await client.GetAsync(url); // pegando a resposta da URL, usando o método GetAsync para fazer a requisição e o método Result para esperar a resposta

                if (resp.IsSuccessStatusCode) // verificando se o status da resposta foi bem sucedida

                {
                    string json = await resp.Content.ReadAsStringAsync(); // pegando o conteúdo da resposta e convertendo para string


                            // agora convertendo realmente string para um Json
                    var rascunho = JObject.Parse(json); // parseando a string para um objeto do tipo JsonObject usando o método Parse da biblioteca System.Json

                    // agora vamos preencher o objeto tempo com os dados da API

                    DateTime time = new();
                    DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime(); // convertendo o horário do nascer do sol para o formato DateTime usando o método AddSeconds para adicionar os segundos do horário do nascer do sol e o método ToLocalTime para converter para o horário local
                    DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime(); // convertendo o horário do pôr do sol para o formato DateTime usando o método AddSeconds para adicionar os segundos do horário do pôr do sol e o método ToLocalTime para converter para o horário local






                    t = new()
                    {
                        lat = (double)rascunho["coord"]["lat"], // pegando a latitude do objeto Json e convertendo para double
                        lon = (double)rascunho["coord"]["lon"], // pegando a longitude do objeto Json e convertendo para double
                        description = (string)rascunho["weather"][0]["description"], // pegando a descrição do objeto Json e convertendo para string // [0] posição do array
                        main = (string)rascunho["weather"][0]["main"], // pegando a descrição do objeto Json e convertendo para string  
                        temp = (double)rascunho["main"]["temp"], // pegando a temperatura mínima do objeto Json e convertendo para double
                        
                        visibility = (int)rascunho["visibility"], // pegando a visibilidade do objeto Json e convertendo para int
                        speed = (double)rascunho["wind"]["speed"], // pegando a velocidade do vento do objeto Json e convertendo para double
                        sunrise = sunrise.ToString("HH:mm"), // convertendo o horário do nascer do sol para string no formato HH:mm usando o método ToString e passando o formato como parâmetro
                        sunset = sunset.ToString("HH:mm") // convertendo o horário do pôr do sol para string no formato HH:mm usando o método ToString e passando o formato como parâmetro
                    }; // finalizando o preenchimento do objeto tempo com os dados da API



                } // Finaliza o IF se o status da resposta foi bem sucedida


            } // fecha laço using para fechar a conexão com a API


            return t;


        }

    }
}
