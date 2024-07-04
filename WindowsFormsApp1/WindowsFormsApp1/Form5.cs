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
using System.IO;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
using DataTable = System.Data.DataTable; // Указываем конкретное пространство имен для DataTable


namespace WindowsFormsApp1
{

    public partial class Form5 : Form
    {
        private MySqlConnection connection; // Объявление переменной для соединения

        private string Username;
        public int BackGround = 0;
        public int Plauplay = 0;
        private static WindowsMediaPlayer player = new WindowsMediaPlayer();
        private static string[] songs = { "song1.mp3" };
        private static int currentSongIndex = 0;

        private const int MinFormWidth = 746;
        private const int MinFormHeight = 501;
        public Form5(int background, int plauplay, string username)
        {
            InitializeComponent();
            // Установите значение dateTimePicker1 на сегодняшнюю дату и отключите его
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker1.Enabled = false;
            LoadData();
            // Устанавливаем начальный размер и минимальный размер формы
            this.Size = new Size(MinFormWidth, MinFormHeight); // Устанавливаем начальный размер
            this.MinimumSize = new Size(MinFormWidth, MinFormHeight); // Устанавливаем минимальный размер
            this.MaximumSize = new Size(MinFormWidth, MinFormHeight);

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
                        control.ForeColor = Color.White;
                        if (control is Guna.UI2.WinForms.Guna2Button)
                        {
                            ((Guna.UI2.WinForms.Guna2Button)control).BorderColor = Color.White;
                        }
                    }
                }
            }
            else
            {
                // Меняем цвет формы на белый
                this.BackColor = Color.White;
                BackGround = 0;
                // Меняем цвет обводки кнопки на черный и текст внутри кнопки на черный для guna2Button2
                guna2Button2.BorderColor = Color.Black;
                guna2Button2.ForeColor = Color.Black;
                // Меняем цвет текста на черный для всех остальных элементов управления на форме
                foreach (Control control in Controls)
                {
                    if (control is Label || control is Guna.UI2.WinForms.Guna2Button)
                    {
                        control.ForeColor = Color.Black;
                        if (control is Guna.UI2.WinForms.Guna2Button)
                        {
                            ((Guna.UI2.WinForms.Guna2Button)control).BorderColor = Color.Black;
                        }
                    }
                }
            }
            string connectionString = "Server=localhost;Database=toystore;Uid=root;Password=zhe27;";
            connection = new MySqlConnection(connectionString);
            dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; // Устанавливаем режим отображения изображения
            this.Username = username;
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7(BackGround, Plauplay, Username);
            form7.Size = this.Size;
            // Показываем Form2 и скрываем текущую форму (Form1)
            form7.Show();
            this.Hide();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadData()
        {
            try
            {
                string connectionString = "Server=localhost;Database=finance;Uid=root;Password=zhe27;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    LoadCategories(connection);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке данных: " + ex.Message);
            }
        }
        private void LoadCategories(MySqlConnection connection)
        {
            string query = "SELECT * FROM img;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }




        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                // Проверка, существует ли столбец "сылка_на_картинку" и не пустое ли его значение
                if (row.Cells["сылка_на_картинку"] != null && row.Cells["сылка_на_картинку"].Value != null)
                {
                    string imagePath = row.Cells["сылка_на_картинку"].Value.ToString();
                    Console.WriteLine("Путь к изображению: " + imagePath); // Логирование для отладки


                    // Установить путь к изображению в TextBox
                    guna2TextBox7.Text = imagePath;
                    // Проверка, существует ли файл перед его загрузкой
                    if (File.Exists(imagePath))
                    {
                        try
                        {
                            pictureBox1.Image = Image.FromFile(imagePath);
                            Console.WriteLine("Изображение успешно загружено."); // Логирование для отладки
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка при загрузке изображения: " + ex.Message);
                            Console.WriteLine("Ошибка при загрузке изображения: " + ex.Message); // Логирование для отладки
                        }
                    }
                    else
                    {
                        MessageBox.Show("Файл не найден: " + imagePath);
                        Console.WriteLine("Файл не найден: " + imagePath); // Логирование для отладки
                    }
                }
                else
                {
                    MessageBox.Show("Столбец 'сылка_на_картинку' не найден или значение пустое.");
                    Console.WriteLine("Столбец 'сылка_на_картинку' не найден или значение пустое."); // Логирование для отладки
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
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
                    string.IsNullOrWhiteSpace(Username))
                {
                    isValid = false;
                    errorMessage = "Пожалуйста, заполните все поля перед сохранением данных.";
                }

                // Валидация фамилии, имени, отчества (только буквы)
                if (!System.Text.RegularExpressions.Regex.IsMatch(guna2TextBox1.Text, @"^[a-zA-Zа-яА-Я]+$") ||
                    !System.Text.RegularExpressions.Regex.IsMatch(guna2TextBox2.Text, @"^[a-zA-Zа-яА-Я]+$") ||
                    !System.Text.RegularExpressions.Regex.IsMatch(guna2TextBox3.Text, @"^[a-zA-Zа-яА-Я]+$"))
                {
                    isValid = false;
                    errorMessage = "Фамилия, имя и отчество должны содержать только буквы.";
                }

                // Валидация номера телефона (только цифры и допустимые символы)
                if (!System.Text.RegularExpressions.Regex.IsMatch(guna2TextBox5.Text, @"^[0-9\+\-\(\)\s]+$"))
                {
                    isValid = false;
                    errorMessage = "Номер телефона должен содержать только цифры и допустимые символы (+, -, (, ), пробел).";
                }

                // Валидация возраста (18 и более)
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

                if (isValid)
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "INSERT INTO reservation (кто_выдал, фамилия, имя, отчество, номер_паспорта, номер_телефона, адрес, дата_выдачи, картинка, возраст) VALUES (@кто_выдал, @фамилия, @имя, @отчество, @номер_паспорта, @номер_телефона, @адрес, @дата_выдачи, @картинка, @возраст)";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@кто_выдал", Username);
                            command.Parameters.AddWithValue("@фамилия", guna2TextBox1.Text);
                            command.Parameters.AddWithValue("@имя", guna2TextBox2.Text);
                            command.Parameters.AddWithValue("@отчество", guna2TextBox3.Text);
                            command.Parameters.AddWithValue("@номер_паспорта", guna2TextBox4.Text);
                            command.Parameters.AddWithValue("@номер_телефона", guna2TextBox5.Text);
                            command.Parameters.AddWithValue("@адрес", guna2TextBox6.Text);
                            command.Parameters.AddWithValue("@дата_выдачи", dateTimePicker1.Value);
                            command.Parameters.AddWithValue("@картинка", guna2TextBox7.Text);
                            command.Parameters.AddWithValue("@возраст", guna2TextBox8.Text);
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

            void OpenForm1()
            {
                Form5 form5 = new Form5(BackGround, Plauplay, Username);
                form5.Show();
                form5.Size = this.Size;
                this.Close();
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
