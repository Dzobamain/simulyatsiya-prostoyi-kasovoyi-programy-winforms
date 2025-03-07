using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task1_hw_winForms_28_2_2025
{
    public partial class Form1 : Form
    {
        private TextBox fuelTotalTextBox;
        private TextBox cafeTotalTextBox;
        private TextBox overallTotalTextBox;
        private ComboBox fuelTypeComboBox;
        private TextBox litersTextBox;
        private TextBox hotDogTextBox;
        private TextBox hamburgerTextBox;
        public Form1()
        {
            InitializeComponent();
            CreateUI();
        }

        private void CreateUI()
        {
            // === Автозаправка ===
            GroupBox gasStationGroup = new GroupBox()
            {
                Text = "Автозаправка",
                Location = new Point(10, 10),
                Size = new Size(300, 200)
            };

            Label fuelTypeLabel = new Label()
            {
                Text = "Тип пального:",
                Location = new Point(10, 30),
                AutoSize = true
            };

            fuelTypeComboBox = new ComboBox()
            {
                Location = new Point(100, 25),
                Size = new Size(150, 20),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Items = { "Бензин - 6.40", "Дизель - 5.50" }
            };
            fuelTypeComboBox.SelectedIndex = 0;

            Label litersLabel = new Label()
            {
                Text = "Літри:",
                Location = new Point(10, 70),
                AutoSize = true
            };

            litersTextBox = new TextBox()
            {
                Location = new Point(100, 65),
                Size = new Size(150, 20)
            };

            Label fuelTotalLabel = new Label()
            {
                Text = "До сплати:",
                Location = new Point(10, 110),
                AutoSize = true
            };

            fuelTotalTextBox = new TextBox()
            {
                Location = new Point(100, 105),
                Size = new Size(150, 20),
                ReadOnly = true
            };

            litersTextBox.TextChanged += CalculateFuelTotal;

            gasStationGroup.Controls.Add(fuelTypeLabel);
            gasStationGroup.Controls.Add(fuelTypeComboBox);
            gasStationGroup.Controls.Add(litersLabel);
            gasStationGroup.Controls.Add(litersTextBox);
            gasStationGroup.Controls.Add(fuelTotalLabel);
            gasStationGroup.Controls.Add(fuelTotalTextBox);

            // === МініКафе ===
            GroupBox miniCafeGroup = new GroupBox()
            {
                Text = "МініКафе",
                Location = new Point(320, 10),
                Size = new Size(300, 200)
            };

            CheckBox hotDogCheckBox = new CheckBox()
            {
                Text = "Хот-дог (4.00 грн)",
                Location = new Point(10, 30),
                AutoSize = true
            };

            hotDogTextBox = new TextBox()
            {
                Location = new Point(150, 25),
                Size = new Size(50, 20),
                Enabled = false
            };

            hotDogCheckBox.CheckedChanged += (s, e) => hotDogTextBox.Enabled = hotDogCheckBox.Checked;

            CheckBox hamburgerCheckBox = new CheckBox()
            {
                Text = "Гамбургер (5.40 грн)",
                Location = new Point(10, 70),
                AutoSize = true
            };

            hamburgerTextBox = new TextBox()
            {
                Location = new Point(150, 65),
                Size = new Size(50, 20),
                Enabled = false
            };

            hamburgerCheckBox.CheckedChanged += (s, e) => hamburgerTextBox.Enabled = hamburgerCheckBox.Checked;

            Label cafeTotalLabel = new Label()
            {
                Text = "До сплати:",
                Location = new Point(10, 110),
                AutoSize = true
            };

            cafeTotalTextBox = new TextBox()
            {
                Location = new Point(100, 105),
                Size = new Size(150, 20),
                ReadOnly = true
            };

            hotDogTextBox.TextChanged += CalculateCafeTotal;
            hamburgerTextBox.TextChanged += CalculateCafeTotal;

            miniCafeGroup.Controls.Add(hotDogCheckBox);
            miniCafeGroup.Controls.Add(hotDogTextBox);
            miniCafeGroup.Controls.Add(hamburgerCheckBox);
            miniCafeGroup.Controls.Add(hamburgerTextBox);
            miniCafeGroup.Controls.Add(cafeTotalLabel);
            miniCafeGroup.Controls.Add(cafeTotalTextBox);

            // === ВСЬОГО до сплати ===
            GroupBox totalGroup = new GroupBox()
            {
                Text = "ВСЬОГО до сплати",
                Location = new Point(10, 220),
                Size = new Size(610, 100)
            };

            Label overallTotalLabel = new Label()
            {
                Text = "Загальна сума:",
                Location = new Point(10, 30),
                AutoSize = true
            };

            overallTotalTextBox = new TextBox()
            {
                Location = new Point(100, 25),
                Size = new Size(150, 20),
                ReadOnly = true
            };

            Button calculateButton = new Button()
            {
                Text = "Порахувати",
                Location = new Point(500, 25),
                Size = new Size(90, 40)
            };
            calculateButton.Click += CalculateOverallTotal;

            totalGroup.Controls.Add(overallTotalLabel);
            totalGroup.Controls.Add(overallTotalTextBox);
            totalGroup.Controls.Add(calculateButton);

            // Додавання всіх груп на форму
            this.Controls.Add(gasStationGroup);
            this.Controls.Add(miniCafeGroup);
            this.Controls.Add(totalGroup);

            // Налаштування форми
            this.Text = "Автозаправка і МініКафе";
            this.Size = new Size(650, 380);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void CalculateFuelTotal(object sender, EventArgs e)
        {
            if (double.TryParse(litersTextBox.Text, out double liters))
            {
                double pricePerLiter = fuelTypeComboBox.SelectedIndex == 0 ? 6.4 : 5.5;
                fuelTotalTextBox.Text = (liters * pricePerLiter).ToString("F2");
            }
            else
            {
                fuelTotalTextBox.Text = "0.00";
            }
        }

        private void CalculateCafeTotal(object sender, EventArgs e)
        {
            double hotDogTotal = 0;
            double hamburgerTotal = 0;

            if (double.TryParse(hotDogTextBox.Text, out double hotDogs))
            {
                hotDogTotal = hotDogs * 4.0;
            }

            if (double.TryParse(hamburgerTextBox.Text, out double hamburgers))
            {
                hamburgerTotal = hamburgers * 5.4;
            }

            cafeTotalTextBox.Text = (hotDogTotal + hamburgerTotal).ToString("F2");
        }

        private void CalculateOverallTotal(object sender, EventArgs e)
        {
            double fuelTotal = double.TryParse(fuelTotalTextBox.Text, out double fuel) ? fuel : 0;
            double cafeTotal = double.TryParse(cafeTotalTextBox.Text, out double cafe) ? cafe : 0;

            double overallTotal = fuelTotal + cafeTotal;
            overallTotalTextBox.Text = overallTotal.ToString("F2");

            MessageBox.Show($"Загальна сума до оплати: {overallTotal:F2} грн", "Розрахунок");

        }
    }
}