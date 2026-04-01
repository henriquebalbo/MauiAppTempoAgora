using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;
using System.Threading.Tasks;

namespace MauiAppTempoAgora
{

    public partial class MainPage : ContentPage
    {
        int count = 0;
        private object tempo;

        public MainPage()
        {
            InitializeComponent();
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text)) // verificando se o campo de entrada da cidade não está vazio ou nulo
                {

                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text); // chamando o método Getprevisão da classe ApiTempo para obter os dados do tempo com base na cidade fornecida

                    if (t != null)
                    {

                        string Visibilidade;

                        if (t.visibility >= 10000)
                        {
                            Visibilidade = "Normal";
                        }
                        else if (t.visibility > 5000)
                        {
                            Visibilidade = "Ruim";
                        }
                        else
                        {
                            Visibilidade = "Reduzida";
                        }

                        DateTime horaUtc = DateTime.UtcNow;
                       // DateTime sunriseUtc = DateTime.UtcNow;
                       // DateTime sunsetUtc = DateTime.UtcNow;

                        DateTime horaLocal = horaUtc.AddSeconds((int)t.timezone);
                       // DateTime sunrise = sunriseUtc.AddSeconds((int)t.timezone);
                       // DateTime sunset= sunsetUtc.AddSeconds((int)t.timezone);

                        string dados_previsao = "";

                        dados_previsao = $"Cidade: {txt_cidade.Text} / {t.country} \n" +
                                         $"Data: {horaLocal:dd/MM/yyyy HH:mm} \n" +
                                         $"Latitude: {t.lat} \n" +
                                         $"Longitude: {t.lon} \n" +
                                         $"Nascer do Sol: {t.sunrise} \n" +
                                       //  $"Nascer do Sol: {sunrise:dd/MM/yyyy HH:mm} \n" +
                                         $"Por do Sol: {t.sunset} \n" +
                                       //  $"Por do Sol: {sunset:dd/MM/yyyy HH:mm} \n" +
                                         $"Temperatura: {t.temp} \n" +
                                         $"Visibilidade: {Visibilidade} \n" +
                                         $"Velocidade do Vento: {t.speed} \n" +
                                         $"Céu Agora: {t.description} \n";

                        resultado.Text = dados_previsao; // exibindo os dados da previsão do tempo na interface do usuário, formatados como uma string

                    }
                    else

                    {
                        resultado.Text = "Sem Previsão para esta cidade!"; // mensagem de erro caso a resposta da API seja nula, indicando que não há dados disponíveis para a cidade fornecida
                    }



                }
                else
                {
                    resultado.Text = "Digite o nome da cidade!"; // mensagem de erro caso o campo de entrada da cidade esteja vazio ou nulo

                }

            }

            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");


            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            txt_cidade.Text = string.Empty;   // limpa o campo de texto
            resultado.Text = string.Empty; // limpa o resultado exibido no label
        }
    }
}
