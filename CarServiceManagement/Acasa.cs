using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;

namespace CarServiceManagement
{
    public partial class Acasa : Form
    {
        private FirestoreDb db;
        private string selectedOrderID;
        private IReadOnlyList<DocumentSnapshot> orders;

        public Acasa()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"service-account.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("fyp-car-service-management");

            fetchTodayOrders();
        }

        private async void fetchTodayOrders() {
            try
            {
                CollectionReference orderCollection = db.Collection("orders");
                QuerySnapshot orderSnapshot = await orderCollection.OrderBy("addedOn").GetSnapshotAsync();

                this.today_grid_view.Columns.Add("ID Comanda", "ID Comanda");
                this.today_grid_view.Columns.Add("Marca", "Marca");
                this.today_grid_view.Columns.Add("Nume Vehicul", "Nume Vehicul");
                this.today_grid_view.Columns.Add("Nume", "Nume");
                this.today_grid_view.Columns.Add("Numar Telefon", "Numar Telefon");
                this.today_grid_view.Columns.Add("Tip Comanda", "Tip Comanda");
                this.today_grid_view.Columns.Add("Pret Total", "Pret Total");

                orders = orderSnapshot.Documents;

                foreach (DocumentSnapshot documentSnapshot in orderSnapshot.Documents)
                {
                    Dictionary<string, object> data = documentSnapshot.ToDictionary();
                    Console.WriteLine(data);
                    Dictionary<string, object> order = new Dictionary<string, object>()
                    {
                        {"orderID", documentSnapshot.Id },
                        {"vehicleName", data.ContainsKey("vehicleName") ? data["vehicleName"]: "" },
                        {"make", data.ContainsKey("make") ? data["make"]: "" },
                        {"ownerContact", data.ContainsKey("ownerContact") ? data["ownerContact"]: "" },
                        {"ownerName", data.ContainsKey("ownerName") ? data["ownerName"]: "" },
                        {"requestType", data.ContainsKey("requestType") ? data["requestType"]: "" },
                        {"totalCost", data.ContainsKey("totalCost") ? data["totalCost"].ToString(): "" },
                    };
                    Console.WriteLine("HERE");
                    this.today_grid_view.Rows.Add(order["orderID"], order["make"], order["vehicleName"], order["ownerName"], order["ownerContact"], order["requestType"], order["totalCost"]);
                   
                    Console.WriteLine("End of Loop");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }


        
        private void add_order_btn_Click(object sender, EventArgs e)
        {
            AdaugaComanda addOrder = new AdaugaComanda();
            this.Hide();
            addOrder.Show();
        }

        private void today_grid_view_selection_changed(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if(dgv != null && dgv.SelectedRows.Count == 1)
            {
                DataGridViewRow row = dgv.SelectedRows[0];

                if (row != null)
                {
                    selectedOrderID = row.Cells[0].Value.ToString();
                    this.details_btn.Enabled = true;
                }
            }
            else
            {
                selectedOrderID = null;
                this.details_btn.Enabled = false;
            }
        }

        private void details_btn_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> foundOrder= null;

            foreach (DocumentSnapshot documentSnapshot in orders)
            {
                Dictionary<string, object> data = documentSnapshot.ToDictionary();
                Dictionary<string, object> newOrder = new Dictionary<string, object>()
                {
                    {"orderID", documentSnapshot.Id.ToString() },
                    {"vehicleName", data.ContainsKey("vehicleName") ? data["vehicleName"]: "" },
                    {"make", data.ContainsKey("make") ? data["make"]: "" },
                    {"ownerContact", data.ContainsKey("ownerContact") ? data["ownerContact"]: "" },
                    {"ownerName", data.ContainsKey("ownerName") ? data["ownerName"]: "" },
                    {"requestType", data.ContainsKey("requestType") ? data["requestType"]: "" },
                    {"totalCost", data.ContainsKey("totalCost") ? data["totalCost"].ToString(): "" },
                    {"addedOn", data["addedOn"] },
                    {"ownerAddress", data.ContainsKey("ownerAddress") ? data["ownerAddress"].ToString(): "" },
                    {"ownerEmail", data.ContainsKey("ownerEmail") ? data["ownerEmail"].ToString(): "" },
                    {"services", data.ContainsKey("services") ? data["services"]: new List<string>() },
                    {"vehicleModel", data.ContainsKey("vehicleModel") ? data["vehicleModel"]: "" },
                    {"vehicleRegistrationNumber", data.ContainsKey("vehicleRegistrationNumber") ? data["vehicleRegistrationNumber"]: "" },

                };
                if (documentSnapshot.Id == selectedOrderID)
                {
                    foundOrder = newOrder;
                }
            }

            if(foundOrder == null)
            {
                MessageBox.Show("Ceva nu a mers!");
            }
            else
            {
                DetaliiComanda orderDetails = new DetaliiComanda();
                orderDetails.order = foundOrder;
                this.Hide();
                orderDetails.Show();
            }
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide();
        }
    }
}
