
using System.Net.Http.Json;

namespace Personas.WinForms
{
    public partial class Form1 : Form
    {
        private readonly HttpClient httpClient;

        public Form1()
        {
            InitializeComponent();

            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7115/")
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        // Consultar personas (GET)
        private async Task ConsultarPersonas()
        {
            try
            {
                var personas = await httpClient.GetFromJsonAsync<List<Persona>>(
                    "api/Personas");

                dgvPersonas.DataSource = personas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar: " + ex.Message);
            }
        }

        // Guardar persona (POST)
        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtEdad.Text, out int edad))
            {
                MessageBox.Show("Introduce una edad válida.");
                return;
            }

            var persona = new Persona
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Edad = edad
            };

            try
            {
                var respuesta = await httpClient.PostAsJsonAsync(
                    "api/Personas", persona);

                if (respuesta.IsSuccessStatusCode)
                {
                    MessageBox.Show("Persona guardada correctamente.");

                    txtNombre.Clear();
                    txtApellido.Clear();
                    txtEdad.Clear();

                    await ConsultarPersonas();
                }
                else
                {
                    MessageBox.Show("No se pudo guardar la persona.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        // Consultar al presionar el botón
        private async void btnConsultar_Click(object sender, EventArgs e)
        {
            await ConsultarPersonas();
        }

    }

    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
    }
}
