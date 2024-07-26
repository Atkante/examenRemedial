using Newtonsoft.Json;
using DavidAstudilloComplementario.Modelos;
using System.Collections.ObjectModel;

namespace DavidAstudilloComplementario.Views;

public partial class vEstudiante : ContentPage
{

    private const string Url = "http://192.168.100.11/DBCOMPLEMENTARIO/estudiantews.php";
    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<Modelos.Estudiante> est;
    public vEstudiante()
    {
        InitializeComponent();
        Mostrar();
    }

    public async void Mostrar()
    {
        var content = await cliente.GetStringAsync(Url);
        List<Modelos.Estudiante> mostrar = JsonConvert.DeserializeObject<List<Modelos.Estudiante>>(content);
        est = new ObservableCollection<Modelos.Estudiante>(mostrar);
        listaEstudiantes.ItemsSource = est;
    }

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Views.vAgregar());
    }
    private void listaEstudiantes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var objEstudiante = (Estudiante)e.SelectedItem;
        Navigation.PushAsync(new Views.vActEliminar(objEstudiante));
    }

}