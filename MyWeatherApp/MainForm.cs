using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.Windows.Forms;

namespace MyWeatherApp
{
    public partial class MainForm : Form
    {
        const string apiId = "c8637de3c5672dc81957ff9aab03a2a8";
        string username; 

        public MainForm(string name)
        {
            InitializeComponent();
            getWeather("Calgary");
            username = name;
        }

        void getWeather(string cityName)
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric&cnt=6", cityName, apiId);
                var json = web.DownloadString(url);
                var jsonObject = JsonConvert.DeserializeObject<weatherInfo.root>(json);
                weatherInfo.root fetch = jsonObject;

                lblCity.Text = string.Format("{0}", fetch.name);
                lblCountry.Text = string.Format("{0}", fetch.sys.country);
                lblTemp.Text = string.Format("{0}\u00B0" + "C", fetch.main.temp);
                lblConditions.Text = string.Format("{0}", fetch.weather[0].main);
                lblDescription.Text = string.Format("{0}", fetch.weather[0].description);
                lblWind.Text = string.Format("{0} km/h", fetch.wind.speed);

            }
        }

        private void btnGo_Click(object sender, System.EventArgs e)
        {
            getWeather(txtCityName.Text);
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            DataTable dt = new DataTable();
            DataBase db = new DataBase();

            db.OpenDB();
            dt = db.noteData();
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[3].Width = 300;
            db.CloseDB();
        }

        private void Add_Click(object sender, System.EventArgs e)
        {
            DataTable dt = new DataTable();
            DataBase db = new DataBase();

            db.OpenDB();
            dt=db.noteAdd(txtTitle.Text,txtContent.Text,username);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[3].Width = 300;
            txtTitle.Text = " ";
            txtContent.Text = " ";
            db.CloseDB();
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            DataTable dt = new DataTable();
            DataBase db = new DataBase();

            db.OpenDB();
            dt = db.noteDelete(txtDelete.Text);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[3].Width = 300;
            txtDelete.Text = " ";
            db.CloseDB();
        }
    }
}
