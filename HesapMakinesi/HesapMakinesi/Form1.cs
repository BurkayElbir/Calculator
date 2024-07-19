using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HesapMakinesi
{
    public partial class Form1 : Form
    {
        private string currentInput = "";
        private string previousInput = "";
        private string currentOperator = "";
        private List<string> history = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Sayibtn(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text == "," && currentInput.Contains(","))
            {
                return;
            }
            currentInput += button.Text;

            if (double.TryParse(currentInput, out double result))
            {
                currentInput = result.ToString();
            }
            UpdateScreen();
        }

        private void Operatorbtn(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (!string.IsNullOrEmpty(currentInput))
            {
                currentOperator = button.Text;
                previousInput = currentInput;
                currentInput = "";
                UpdateScreen();
            }
            else
            {
                MessageBox.Show("Lutfen bir sayi girin", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button26_Click(object sender, EventArgs e)
        {
            ShowHistory();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnEsittir_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentInput) && !string.IsNullOrEmpty(previousInput) && !string.IsNullOrEmpty(currentOperator))
            {
                double sayi1 = Convert.ToDouble(previousInput);
                double sayi2 = Convert.ToDouble(currentInput);
                double sonuc = PerformOperation(sayi1, sayi2, currentOperator);
                string sonucString = sonuc.ToString();
                DisplayResult(sonucString);
                history.Add($"{previousInput} {currentOperator} {currentInput} = {sonucString}");
            }
            else
            {
                MessageBox.Show("Eksik bilgi. Lutfen tum gerekli alanlari doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateScreen()
        {
            label1.Text = previousInput;
            label2.Text = currentOperator;
            label3.Text = currentInput;
            label4.Text = "";

        }
        private void DisplayResult(string sonuc)
        {
            int resultValue;

            if (int.TryParse(sonuc, out resultValue) && resultValue >= 1 && resultValue <= 9999)
            {
                label4.Text = sonuc + "(" + ConvertToRoman(resultValue) + ")";
            }
            else
            {
                label4.Text = sonuc;
            }
            label4.Refresh();
        }
        private void ClearInput()
        {
            currentInput = "";
            previousInput = "";
            currentOperator = "";
            UpdateScreen();
        }

        private double PerformOperation(double sayi1, double sayi2, string islem)
        {
            switch (islem)
            {
                case "+":
                    return sayi1 + sayi2;
                case "-":
                    return sayi1 - sayi2;
                case "*":
                    return sayi1 * sayi2;
                case "/":
                    if (sayi2 != 0)
                        return sayi1 / sayi2;
                    else
                    {
                        DisplayResult("=∞");
                        return 0;
                    }
                case "%":
                    return sayi1 % sayi2;
                default:
                    return 0;
            }
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        private void btnCe_Click(object sender, EventArgs e)
        {
            currentInput = "";
            UpdateScreen();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (currentInput.Length > 0)
            {
                currentInput = currentInput.Substring(0, currentInput.Length - 1);
                UpdateScreen();
            }
        }

        private void btnIsaret_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentInput))
            {
                currentInput = (Convert.ToDouble(currentInput) * -1).ToString();
                UpdateScreen();
            }
        }
        
        private void btnEkok_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentInput) && !string.IsNullOrEmpty(previousInput))
            {
                int sayi1 = Convert.ToInt32(previousInput);
                int sayi2 = Convert.ToInt32(currentInput);
                int ekok = EkokHesapla(sayi1, sayi2);
                DisplayResult(ekok.ToString());
            }
            else
            {
                MessageBox.Show("Eksik bilgi. Lutfen tum gerekli alanlari doldurunuz. ", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private int EbobHesapla(int a, int b)
        {
            int ebob = 0;
            int enkucuk;
            if (a < b)
            {
                enkucuk = a;

            }
            else
                enkucuk = b;

            for(int i = enkucuk; ; i--)
            {
                if( a % i == 0 && b % i ==0)
                {
                    ebob = i;
                }
            }
            return ebob;
        }
        
        private int EkokHesapla(int a, int b)  
        {
            return a * b / EbobHesapla(a, b);
        }                                     

        private void ShowHistory()
        {
            string islemgecmisi = "Islem gecmisi\n";
            foreach (string item in history)
            {
                islemgecmisi += $"{item}\n";
            }
            MessageBox.Show(islemgecmisi, "Islem gecmini", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUst_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentInput))
            {
                double sayi = Convert.ToDouble(currentInput);
                double sonuc = Math.Pow(sayi, 2);
                DisplayResult(sonuc.ToString());
                history.Add($"{currentInput} ^ 2 = {sonuc}");
                ClearInput();
            }
        }

        private void btnMod_Click(object sender, EventArgs e)
        {
            
         }



        private void btnKok_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentInput))
            {
                double sayi = Convert.ToDouble(currentInput);
                double sonuc = Math.Sqrt(sayi);
                DisplayResult(sonuc.ToString());
                history.Add($"√{currentInput} = {sonuc}");
            }
            else
            {
                MessageBox.Show("Eksik bilgi. Lutfen bir sayi girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ClearInput();
        }

        private string ConvertToRoman(int sayi)
        {
            string[] romenRakamlar = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            int[] normalsayi = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };

            StringBuilder result = new StringBuilder();

            for(int i = 0; i < normalsayi.Length; i++)
            {
                while (sayi >= normalsayi[i])
                {
                    result.Append(romenRakamlar[i]);
                    sayi -= normalsayi[i];
                }
            }
            return result.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnVirgul_Click(object sender, EventArgs e)
        {
            if (!currentInput.Contains(","))
            {
                currentInput += ",";
                UpdateScreen();
            }
        }
        private void btnEbob_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentInput) && !string.IsNullOrEmpty(previousInput))
            {
                int sayi1 = Convert.ToInt32(previousInput);
                int sayi2 = Convert.ToInt32(currentInput);
                int ebob = EbobHesapla(sayi1, sayi2);
                DisplayResult(ebob.ToString());
            }
            else
            {
                MessageBox.Show("Eksik bilgi. Lutfen tum gerekli alanlari doldurun", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
