using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using WMPLib;

namespace WindowsFormsApp1
{
    public partial class Form8 : Form
    {
        private string Username;
        public int BackGround = 0;
        public int Plauplay = 0;
        private static WindowsMediaPlayer player = new WindowsMediaPlayer();
        private static string[] songs = { "song1.mp3" };
        private static int currentSongIndex = 0;

        private const int MinFormWidth = 710;
        private const int MinFormHeight = 711;

        private bool isTextVisible = false;
        private Image showImage = Image.FromFile(@"C:/Users/Женя/Desktop/програмы/проект по кпияп/WindowsFormsApp1/unnamed.png");// Загрузка изображения для показа текста
        private Image hideImage = Image.FromFile(@"C:/Users/Женя/Desktop/програмы/проект по кпияп/WindowsFormsApp1/closed_eye.png");// Загрузка изображения для скрытия текста

        public Form8(int background, int plauplay, string username)
        {
            InitializeComponent();
            // Установите минимальную дату для dateTimePicker2
            dateTimePicker2.MinDate = dateTimePicker1.Value.AddMonths(1);
            // Установите значение dateTimePicker1 на сегодняшнюю дату и отключите его
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker1.Enabled = false;
            // Заполнение ComboBox данными
            guna2ComboBox1.Items.Add("замужем");
            guna2ComboBox1.Items.Add("женат");
            guna2ComboBox1.Items.Add("разведен");
            guna2ComboBox1.Items.Add("многодетный");
            guna2ComboBox1.Items.Add("одинок");
            // Устанавливаем начальный размер и минимальный размер формы
            this.Size = new Size(MinFormWidth, MinFormHeight); // Устанавливаем начальный размер
            this.MinimumSize = new Size(MinFormWidth, MinFormHeight); // Устанавливаем минимальный размер
            this.MaximumSize = new Size(MinFormWidth, MinFormHeight);
            // Другие операции и инициализации формы
            this.Username = username;

            Plauplay = plauplay;
            BackGround = background;
            if (BackGround == 1)
            {
                // Меняем цвет формы на черный
                this.BackColor = Color.Black;
                BackGround = 1;
                // Меняем цвет обводки кнопки на белый и текст внутри кнопки на белый для guna2Button2
                guna2Button2.BorderColor = Color.White;
                guna2Button2.ForeColor = Color.White;

                // Меняем цвет текста на белый для всех остальных элементов управления на форме
                foreach (Control control in Controls)
                {
                    if (control is Label || control is Guna.UI2.WinForms.Guna2Button)
                    {
                        // Если элемент управления - это Label или Guna2Button, меняем его цвет текста на белый
                        control.ForeColor = Color.White;

                        // Если элемент управления - это Guna2Button, также меняем цвет обводки на белый
                        if (control is Guna.UI2.WinForms.Guna2Button)
                        {
                            ((Guna.UI2.WinForms.Guna2Button)control).BorderColor = Color.White;
                        }
                    }
                    // Добавьте другие типы элементов управления, которые нужно изменить на белый
                    showImage = Image.FromFile(@"C:/Users/Женя/Desktop/програмы/проект по кпияп/WindowsFormsApp1/images.png"); // Загрузка изображения для показа текста
                    hideImage = Image.FromFile(@"C:/Users/Женя/Desktop/програмы/проект по кпияп/WindowsFormsApp1/closed_eye_1.png");// Загрузка изображения для скрытия текста

                }
            }
            else
            {
                this.BackColor = Color.White;
                BackGround = 0;
                // Меняем цвет обводки кнопки на белый и текст внутри кнопки на белый для guna2Button2
                guna2Button2.BorderColor = Color.Black;
                guna2Button2.ForeColor = Color.Black;

                // Меняем цвет текста на белый для всех остальных элементов управления на форме
                foreach (Control control in Controls)
                {
                    if (control is Label || control is Guna.UI2.WinForms.Guna2Button)
                    {
                        // Если элемент управления - это Label или Guna2Button, меняем его цвет текста на белый
                        control.ForeColor = Color.Black;

                        // Если элемент управления - это Guna2Button, также меняем цвет обводки на белый
                        if (control is Guna.UI2.WinForms.Guna2Button)
                        {
                            ((Guna.UI2.WinForms.Guna2Button)control).BorderColor = Color.Black;
                        }
                    }
                    // Добавьте другие типы элементов управления, которые нужно изменить на белый
                    showImage = Image.FromFile(@"C:/Users/Женя/Desktop/програмы/проект по кпияп/WindowsFormsApp1/unnamed.png"); // Загрузка изображения для показа текста
                    hideImage = Image.FromFile(@"C:/Users/Женя/Desktop/програмы/проект по кпияп/WindowsFormsApp1/closed_eye.png");// Загрузка изображения для скрытия текста

                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Пользователь существует в таблице "user", открываем форму 7 и передаем имя пользователя
            Form7 form7 = new Form7(BackGround, Plauplay, Username);
            form7.Show();
            form7.Size = this.Size;
            this.Hide(); // Скрываем текущую форму
            return;
        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Server=localhost;Database=finance;Uid=root;Password=zhe27;";
                bool isValid = true;
                string errorMessage = "";

                // Проверяем, что все поля заполнены
                if (string.IsNullOrWhiteSpace(guna2TextBox1.Text) ||
                    string.IsNullOrWhiteSpace(guna2TextBox2.Text) ||
                    string.IsNullOrWhiteSpace(guna2TextBox3.Text) ||
                    string.IsNullOrWhiteSpace(guna2TextBox4.Text) ||
                    string.IsNullOrWhiteSpace(guna2TextBox5.Text) ||
                    string.IsNullOrWhiteSpace(guna2TextBox6.Text) ||
                    string.IsNullOrWhiteSpace(guna2TextBox7.Text) ||
                    string.IsNullOrWhiteSpace(guna2TextBox8.Text) ||
                    (!radioButton1.Checked && !radioButton2.Checked) ||
                    string.IsNullOrWhiteSpace(Username))
                {
                    isValid = false;
                    errorMessage = "Пожалуйста, заполните все поля перед сохранением данных.";
                }

                // Проверка фамилии, имени, отчества (только буквы)
                if (!System.Text.RegularExpressions.Regex.IsMatch(guna2TextBox1.Text, @"^[a-zA-Zа-яА-Я]+$") ||
                    !System.Text.RegularExpressions.Regex.IsMatch(guna2TextBox2.Text, @"^[a-zA-Zа-яА-Я]+$") ||
                    !System.Text.RegularExpressions.Regex.IsMatch(guna2TextBox3.Text, @"^[a-zA-Zа-яА-Я]+$"))
                {
                    isValid = false;
                    errorMessage = "Фамилия, имя и отчество должны содержать только буквы.";
                }

                // Проверка номера телефона (только цифры и допустимые символы)
                if (!System.Text.RegularExpressions.Regex.IsMatch(guna2TextBox6.Text, @"^[0-9\+\-\(\)\s]+$"))
                {
                    isValid = false;
                    errorMessage = "Номер телефона должен содержать только цифры и допустимые символы (+, -, (, ), пробел).";
                }

                // Проверка возраста (18 и более)
                if (int.TryParse(guna2TextBox8.Text, out int age))
                {
                    if (age < 18)
                    {
                        isValid = false;
                        errorMessage = "Возраст должен быть 18 лет или более.";
                    }
                }
                else
                {
                    isValid = false;
                    errorMessage = "Возраст должен быть числом.";
                }

                // Проверка даты выдачи (должна быть сегодняшняя)
                if (dateTimePicker1.Value.Date != DateTime.Today)
                {
                    isValid = false;
                    errorMessage = "Дата выдачи должна быть сегодняшним днем.";
                }

                // Проверка срока действия (должен быть не менее месяца с даты выдачи)
                if (dateTimePicker2.Value <= dateTimePicker1.Value.AddMonths(1))
                {
                    isValid = false;
                    errorMessage = "Срок действия должен быть не менее месяца с даты выдачи.";
                }

                if (isValid)
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        string gender = radioButton1.Checked ? "Мужчина" : "Женщина";
                        string query = "INSERT INTO credit (creditcol, фамилия, имя, отчество, идентификационный_номер, адрес, возраст, номер_телефона, доход, пол, дата_выдачи, срок_действия) VALUES (@creditcol, @фамилия, @имя, @отчество, @идентификационный_номер, @адрес, @возраст, @номер_телефона, @доход, @пол, @дата_выдачи, @срок_действия)";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@creditcol", Username);
                            command.Parameters.AddWithValue("@фамилия", guna2TextBox1.Text);
                            command.Parameters.AddWithValue("@имя", guna2TextBox2.Text);
                            command.Parameters.AddWithValue("@отчество", guna2TextBox3.Text);
                            command.Parameters.AddWithValue("@идентификационный_номер", guna2TextBox4.Text);
                            command.Parameters.AddWithValue("@адрес", guna2TextBox5.Text);
                            command.Parameters.AddWithValue("@возраст", guna2TextBox8.Text);
                            command.Parameters.AddWithValue("@номер_телефона", guna2TextBox6.Text);
                            command.Parameters.AddWithValue("@доход", guna2TextBox7.Text);
                            command.Parameters.AddWithValue("@пол", gender);
                            command.Parameters.AddWithValue("@дата_выдачи", dateTimePicker1.Value);
                            command.Parameters.AddWithValue("@срок_действия", dateTimePicker2.Value);

                            command.ExecuteNonQuery();
                        }

                        MessageBox.Show("Данные успешно сохранены в базе данных.");
                        OpenForm1();
                    }
                }
                else
                {
                    MessageBox.Show(errorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }

        void OpenForm1()
        {
            Form9 form9 = new Form9(BackGround, Plauplay, Username);
            form9.Show();
            form9.Size = this.Size;
            this.Close();
        }


        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            // Установите минимальную дату для dateTimePicker2 при изменении даты
            dateTimePicker2.MinDate = dateTimePicker1.Value.AddMonths(1);
        }

        private void guna2TextBox8_TextChanged(object sender, EventArgs e)
        {
         
        }
    }
}
