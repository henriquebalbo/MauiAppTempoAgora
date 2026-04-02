using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

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

                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text); // chamando o método Getprevisão da classe ApiTempo para
                                                                               // obter os dados do tempo com base na cidade fornecida

                    if (t != null)
                    {
                        // *** Solicitado na Agenda 07

                        string Visibilidade; // criando a variavel para armazenar a descrição da visibilidade
                                             // com base no valor retornado pela API

                        if (t.visibility >= 10000) // Explicando: 10000 metros ou mais é considerado "Normal",
                                                   // entre 5000 e 9999 metros é considerado "Ruim" e
                                                   // abaixo de 5000 metros é considerado "Reduzida"
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

                        DateTime horaUtc = DateTime.UtcNow; // obtendo a hora atual em UTC (Tempo Universal Coordenado)
                        DateTime horaLocal = horaUtc.AddSeconds((int)t.timezone); // convertendo a hora UTC para a hora local da cidade,
                                                                                  // adicionando o deslocamento de fuso horário retornado
                                                                                  // pela API (t.timezone) em segundos


                        string dados_previsao = ""; // criando a variavel para armazenar os dados da previsão do tempo, formatados como uma string

                        dados_previsao = $"Cidade: {txt_cidade.Text} / {t.country} \n" +
                                         $"Data: {horaLocal:dd/MM/yyyy HH:mm} \n" +
                                         $"Latitude: {t.lat} \n" +
                                         $"Longitude: {t.lon} \n" +
                                         $"Nascer do Sol: {t.sunrise} \n" +
                                         $"Por do Sol: {t.sunset} \n" +
                                         $"Temperatura: {t.temp} \n" +
                                         $"Visibilidade: {Visibilidade} \n" + // Agenda 07
                                         $"Velocidade do Vento: {t.speed} \n" + // Agenda 07
                                         $"Céu Agora: {t.description} \n"; // Agenda 07

                        resultado.Text = dados_previsao; // exibindo os dados da previsão do tempo 

                    } // *** Agenda 07 - Melhorias no tratamento de erros
                    else 

                    {
                        await DisplayAlert("Aviso", "Cidade não encontrada! Verifique o nome digitado.", "OK"); // mensagem de erro caso a cidade
                                                                                                                // não seja encontrada 
                    }
                }
                else
                {
                    await DisplayAlert("Atenção", "Digite o nome da cidade!", "OK"); // mensagem caso o campo de entrada da cidade
                                                                                     // esteja vazio ou nulo
                }
            }

            catch (Exception ex) // qualquer exceção que possa ocorrer durante a execução do código,
                                 // como erros de rede ou problemas com a API
            {
                if (ex.Message.Contains("404")) // erro 404 indica que a cidade não foi encontrada na API
                {
                    await DisplayAlert("Erro", "Cidade não encontrada!", "OK");
                }
                else
                {
                    await DisplayAlert("Erro", "Erro ao buscar previsão do tempo.", "OK");
                }

             }
        }

        private void Button_Clicked_1(object sender, EventArgs e) // botão para limpar
        {
            txt_cidade.Text = string.Empty;   // limpa o campo de texto
            resultado.Text = string.Empty; // limpa o resultado exibido no label
        }
    }
}
