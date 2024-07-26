using DavidAstudilloComplementario.Modelos;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DavidAstudilloComplementario.Views
{
    public partial class Login : ContentPage
    {
        private const string Url = "http://192.168.100.11/DBCOMPLEMENTARIO/estudiantews_Login.php";

        public Login()
        {
            InitializeComponent();
        }

        private async void iniciarSesion_Clicked(object sender, EventArgs e)
        {
            try
            {
                using (HttpClient cliente = new HttpClient())
                {
                    var loginData = new Dictionary<string, string>
                    {
                        
                        { "usuario", txtUser.Text },
                        { "contrasena", txtPass.Text }
                    };

                    var content = new FormUrlEncodedContent(loginData);
                    HttpResponseMessage response = await cliente.PostAsync(Url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        System.Diagnostics.Debug.WriteLine("Respuesta del servidor: " + responseString);

                        if (responseString == "1")
                        {
                            // Inicio de sesión exitoso
                            await DisplayAlert("Éxito", "Bienvenido", "OK");
                            // Aquí podrías navegar a otra página, por ejemplo:
                            await Navigation.PushAsync(new Views.vEstudiante());
                        }
                        else
                        {
                            await DisplayAlert("Error", responseString, "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", "Error en la comunicación con el servidor", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Mensaje: {ex.Message}\nTipo: {ex.GetType()}\nStack Trace: {ex.StackTrace}", "OK");
            }
        }
    }
}
