namespace App_InchiriereAuto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Luam datele din interfata
            string nume = textBox1.Text;
            string masina = textBox2.Text;
            string zile = textBox3.Text;

            // Afisam un mesaj simplu de confirmare (Bare Bones)
            MessageBox.Show("Rezervare: " + nume + " | Masina: " + masina + " | Zile: " + zile);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
