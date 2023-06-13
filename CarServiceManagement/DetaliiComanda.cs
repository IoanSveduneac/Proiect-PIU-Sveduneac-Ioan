using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace CarServiceManagement
{
    public partial class DetaliiComanda : Form
    {
        public Dictionary<string, object> order;
        private FirestoreDb db;

        public DetaliiComanda()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Acasa home = new Acasa();
            home.Show();
            this.Hide();
        }

        private void OrderDetails_Load(object sender, EventArgs e)
        {         
            string path = AppDomain.CurrentDomain.BaseDirectory + @"service-account.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("fyp-car-service-management");

            this.order_id_lb.Text = order["orderID"].ToString();
            this.make_cb.Text = order["make"].ToString();
            this.vehicle_model_tb.Text = order["vehicleModel"].ToString();
            this.vehicle_name_tb.Text = order["vehicleName"].ToString();
            this.vehicle_reg_tb.Text = order["vehicleRegistrationNumber"].ToString();
            this.owner_address_tb.Text = order["ownerAddress"].ToString();
            this.owner_contact_tb.Text = order["ownerContact"].ToString();
            this.owner_email_tb.Text = order["ownerEmail"].ToString();
            this.owner_name_tb.Text = order["ownerName"].ToString();
            this.request_type_cb.Text = order["requestType"].ToString();
            this.total_cost_tb.Text = order["totalCost"].ToString();

            List<object> services = (List<object>)order["services"];
            
            
            if (services.Contains("Oil Change")) {
                this.oil_change_cb.Checked = true;
            }

            if (services.Contains("Engine Tune up"))
            {
                this.engine_tuneup_cb.Checked = true;
            }

            if (services.Contains("Denting"))
            {
                this.denting_cb.Checked = true;
            }

            if (services.Contains("Paint Work"))
            {
                this.paint_work_cb.Checked = true;
            }

            if (services.Contains("Tire Replacement"))
            {
                this.tire_replacement_cb.Checked = true;
            }
        }

        private bool isServicesSelect()
        {
            return buildServiceList().Count > 0;
        }

        private List<string> buildServiceList()
        {
            List<string> services = new List<string>();
            bool oilChange = this.oil_change_cb.Checked;
            bool engineTuneUp = this.engine_tuneup_cb.Checked;
            bool denting = this.denting_cb.Checked;
            bool paintWork = this.paint_work_cb.Checked;
            bool tireReplacement = this.tire_replacement_cb.Checked;

            if (oilChange)
            {
                services.Add("Oil Change");
            }

            if (engineTuneUp)
            {
                services.Add("Engine Tune up");
            }

            if (denting)
            {
                services.Add("Denting");
            }

            if (paintWork)
            {
                services.Add("Paint Work");
            }

            if (tireReplacement)
            {
                services.Add("Tire Replacement");
            }

            return services;
        }

        private Paragraph addDataToPDFDocument(string label, string value, Document document) {
            Phrase labelPhrase = new Phrase(label);
            labelPhrase.Font = FontFactory.GetFont(FontFactory.HELVETICA, 24f, BaseColor.BLACK);
            Phrase valuePhrase = new Phrase(value.Length == 0 ? "Nespecificat" : value);
            valuePhrase.Font = FontFactory.GetFont(FontFactory.HELVETICA, 24f, BaseColor.BLACK);

            Paragraph para = new Paragraph();
            para.Add(labelPhrase);
            para.Add(valuePhrase);
            para.SpacingAfter = 30f;

            document.Add(para);

            return para;
        }

        private void addServicesTableToPDFDocument(List<object> services, Document document) {
            PdfPTable table = new PdfPTable(2);

            PdfPCell cell = new PdfPCell(new Phrase("Servicii folosite"));

            cell.Colspan = 2;

            cell.HorizontalAlignment = 1;

            table.AddCell(cell);

            table.AddCell("Schimb Ulei");
            table.AddCell(services.Contains("Oil Change") ? "Da": "Nu");

            table.AddCell("Reglaje Motor");
            table.AddCell(services.Contains("Engine Tune up") ? "Da" : "Nu");


            table.AddCell("Polish");
            table.AddCell(services.Contains("Denting") ? "Da" : "Nu");


            table.AddCell("Vopsit");
            table.AddCell(services.Contains("Paint Work") ? "Da" : "Nu");

            table.AddCell("Schimb Anvelope");
            table.AddCell(services.Contains("Tire Replacement") ? "Da" : "Nu");

            table.SpacingAfter = 30f;

            document.Add(table);
        }

        private void addHeadingToPDFDocument(Document document)
        {
            PdfPTable table = new PdfPTable(2);
            PdfPCell cell = new PdfPCell(new Phrase("Factura"));
            cell.HorizontalAlignment = 1;
            cell.Colspan = 2;

            PdfPCell cell2 = new PdfPCell(new Phrase("ID comanda"));
            cell2.HorizontalAlignment = 0;
            PdfPCell cell3 = new PdfPCell(new Phrase(order["orderID"].ToString()));
            cell3.HorizontalAlignment = 0;
            table.AddCell(cell);
            table.AddCell(cell2);
            table.AddCell(cell3);

            table.SpacingAfter = 30f;

            document.Add(table);
        }

        private void generatePDF() {
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                Document document = new Document(PageSize.A4, 10, 10, 10, 10);

                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();


                addHeadingToPDFDocument(document);

                Paragraph datePara = addDataToPDFDocument("Date: ", DateTime.Now.ToString(), document);
                datePara.Alignment = Element.ALIGN_RIGHT;
                addDataToPDFDocument("Tipul Comenzii: ", order["requestType"].ToString(), document);
                addDataToPDFDocument("Nume: ",order["ownerName"].ToString(), document);
                addDataToPDFDocument("Email: ",order["ownerEmail"].ToString(), document);

                addDataToPDFDocument("Numar Telefon: ", order["ownerContact"].ToString(), document);

                addDataToPDFDocument("Adresa: ", order["ownerAddress"].ToString(), document);
                

                addDataToPDFDocument("Marca: ", order["make"].ToString(), document);
                addDataToPDFDocument("Nume Vehicul: ", order["vehicleName"].ToString(), document);

                addDataToPDFDocument("Model: ", order["vehicleModel"].ToString(), document);

                addDataToPDFDocument("Numar Inmatriculare: ", order["vehicleRegistrationNumber"].ToString(), document);

                addServicesTableToPDFDocument((List<object>)order["services"], document);

                addDataToPDFDocument("Pret Total: ", order["totalCost"].ToString() + " RON", document);

                document.Close();

                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();

                string currentDrive = AppDomain.CurrentDomain.BaseDirectory.Substring(0, 2);
                string billDirectoryPath = currentDrive + "\\Generated Bills";

                if (!Directory.Exists(billDirectoryPath))
                {
                    Directory.CreateDirectory(billDirectoryPath);
                }

                string path = billDirectoryPath + "\\" + "bill-" + order["orderID"].ToString() + ".pdf";

                if(File.Exists(path))
                {
                    File.Delete(path);
                }

                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();

                System.Diagnostics.Process.Start(path);
                MessageBox.Show("Factura a fost salvata in:  " + billDirectoryPath);
            }
        }

        private async void save_btn_Click(object sender, EventArgs e)
        {
            try
            {
                string make = this.make_cb.Text;
                string vehicleName = this.vehicle_name_tb.Text;
                string vehicleRegistrationNumber = this.vehicle_reg_tb.Text;
                string vehicleModel = this.vehicle_model_tb.Text;
                string ownerName = this.owner_name_tb.Text;
                string ownerContact = this.owner_contact_tb.Text;
                string ownerEmail = this.owner_email_tb.Text;
                string requestType = this.request_type_cb.Text;
                string ownerAddress = this.owner_address_tb.Text;
                string totalCost = this.total_cost_tb.Text;
                bool oilChange = this.oil_change_cb.Checked;
                bool engineTuneUp = this.engine_tuneup_cb.Checked;
                bool denting = this.denting_cb.Checked;
                bool paintWork = this.paint_work_cb.Checked;
                bool tireReplacement = this.tire_replacement_cb.Checked;

                if (make.Length == 0 || vehicleModel.Length == 0 || ownerName.Length == 0 || vehicleName.Length == 0 || vehicleRegistrationNumber.Length == 0 || ownerContact.Length == 0 || requestType.Length == 0 || totalCost.Length == 0 || !isServicesSelect())
                {
                    throw new Exception("Va tugam completati toate spatiile libere");
                }

                int totalCostCasted;
                if (!int.TryParse(totalCost, out totalCostCasted))
                {
                    throw new Exception("Va rugam completati spatiul doar cu numere");
                }

                if (requestType == "Dropoff" && ownerAddress.Length == 0)
                {
                    throw new Exception("Este necesara adresa pentru a livra vehiculul");
                }

                Dictionary<string, object> newOrder = new Dictionary<string, object>
                {
                    { "make", make },
                    { "vehicleName", vehicleName },
                    { "vehicleRegistrationNumber", vehicleRegistrationNumber },
                    { "ownerContact", ownerContact },
                    { "ownerEmail", ownerEmail },
                    { "requestType", requestType },
                    { "ownerAddress", ownerAddress },
                    { "vehicleModel", vehicleModel},
                    { "totalCost", totalCostCasted },
                    { "ownerName", ownerName },
                    { "services", buildServiceList() },
                };
                DocumentReference orderDocument = db.Collection("orders").Document(order["orderID"].ToString());
                await orderDocument.UpdateAsync(newOrder);

                MessageBox.Show("Comanda editata si salvata");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private async void delete_btn_Click(object sender, EventArgs e)
        {
            try
            {
                await db.Collection("orders").Document(order["orderID"].ToString()).DeleteAsync();
                MessageBox.Show("Comanda stearsa");

                this.Hide();
                Acasa home = new Acasa();
                home.Show();
            }
            catch (Exception error) {
                MessageBox.Show(error.Message);
            }

        }

        private void generate_bill_btn_Click(object sender, EventArgs e)
        {
            generatePDF();
        }
    }
}
