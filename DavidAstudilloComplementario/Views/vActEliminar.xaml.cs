using DavidAstudilloComplementario.Modelos;

namespace DavidAstudilloComplementario.Views;

public partial class vActEliminar : ContentPage
{
    public vActEliminar(Estudiante datos)
    {
        InitializeComponent();

        txtCodigo.Text = datos.COD_ESTUDIANTE.ToString();
        txtNombre.Text = datos.NOMBRE;
        txtApellido.Text = datos.APELLIDO;
        txtEdad.Text = datos.NOTA_FINAL.ToString();
    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            using (HttpClient cliente = new HttpClient { Timeout = TimeSpan.FromMinutes(5) })
            {
                var param = new Dictionary<string, string>
            {
                { "nombre", txtNombre.Text },
                { "apellido", txtApellido.Text },
                { "edad", txtEdad.Text }
            };

                var content = new FormUrlEncodedContent(param);
                HttpResponseMessage response = await cliente.PutAsync("http://192.168.100.11/DBCOMPLEMENTARIO/estudiantews.php", content);

                response.EnsureSuccessStatusCode();

                // Navegar a la nueva página solo si la respuesta fue exitosa
                await Navigation.PushAsync(new vEstudiante());
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("alerta", ex.Message, "OK");
        }
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            using (HttpClient cliente = new HttpClient { Timeout = TimeSpan.FromMinutes(5) })
            {
                // Construir la URL con el ID del estudiante a eliminar
                string url = $"http://192.168.100.2/Semana6/estudiantews.php?codigo={txtCodigo.Text}";

                // Enviar la solicitud DELETE
                HttpResponseMessage response = await cliente.DeleteAsync(url);

                // Verificar si la solicitud fue exitosa
                response.EnsureSuccessStatusCode();

                string responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response from server: {responseString}");

                // Navegar a la nueva página solo si la respuesta fue exitosa
                await Navigation.PushAsync(new vEstudiante());
            }
        }
        catch (HttpRequestException httpEx)
        {
            await DisplayAlert("Error de HTTP", $"Mensaje: {httpEx.Message}", "OK");
        }
        catch (TaskCanceledException ex) when (ex.CancellationToken == default)
        {
            // Manejo específico para el tiempo de espera agotado
            await DisplayAlert("Alerta", "La solicitud se canceló debido a que se agotó el tiempo de espera configurado.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alerta", ex.Message, "OK");
        }
    }
}