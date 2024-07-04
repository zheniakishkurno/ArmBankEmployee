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
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using WMPLib;
using WinFormsControl = System.Windows.Forms.Control;
using WinFormsColor = System.Drawing.Color;
using OpenXmlControl = DocumentFormat.OpenXml.Wordprocessing.Control;
using OpenXmlColor = DocumentFormat.OpenXml.Wordprocessing.Color;

namespace WindowsFormsApp1
{
    public partial class Form14 : Form
    {
        private string Username;
        public int BackGround = 0;
        public int Plauplay = 0;
        private MySqlConnection connection;
        private static WindowsMediaPlayer player = new WindowsMediaPlayer();
        private static string[] songs = { "song1.mp3" };
        private static int currentSongIndex = 0;

        private const int MinFormWidth = 818;
        private const int MinFormHeight = 497;

        private bool isTextVisible = false;
        private Image showImage = Image.FromFile(@"C:/Users/Женя/Desktop/програмы/проект по кпияп/WindowsFormsApp1/unnamed.png");// Загрузка изображения для показа текста
        private Image hideImage = Image.FromFile(@"C:/Users/Женя/Desktop/програмы/проект по кпияп/WindowsFormsApp1/closed_eye.png");// Загрузка изображения для скрытия текста

        public Form14(int background, int plauplay, string username)
        {
            InitializeComponent();
            // Другие операции и инициализации формы
            this.Username = username;

            Plauplay = plauplay;
            BackGround = background;
            if (BackGround == 1)
            {
                // Меняем цвет формы на черный
                this.BackColor = WinFormsColor.Black;
                BackGround = 1;
                // Меняем цвет обводки кнопки на белый и текст внутри кнопки на белый для guna2Button2
                guna2Button2.BorderColor = WinFormsColor.White;
                guna2Button2.ForeColor = WinFormsColor.White;

                // Меняем цвет текста на белый для всех остальных элементов управления на форме
                foreach (WinFormsControl control in Controls)
                {
                    if (control is Label || control is Guna.UI2.WinForms.Guna2Button)
                    {
                        // Если элемент управления - это Label или Guna2Button, меняем его цвет текста на белый
                        control.ForeColor = WinFormsColor.White;

                        // Если элемент управления - это Guna2Button, также меняем цвет обводки на белый
                        if (control is Guna.UI2.WinForms.Guna2Button)
                        {
                            ((Guna.UI2.WinForms.Guna2Button)control).BorderColor = WinFormsColor.White;
                        }
                    }
                    // Добавьте другие типы элементов управления, которые нужно изменить на белый
                    showImage = Image.FromFile(@"C:/Users/Женя/Desktop/програмы/проект по кпияп/WindowsFormsApp1/images.png"); // Загрузка изображения для показа текста
                    hideImage = Image.FromFile(@"C:/Users/Женя/Desktop/програмы/проект по кпияп/WindowsFormsApp1/closed_eye_1.png");// Загрузка изображения для скрытия текста

                }
            }
            else
            {
                this.BackColor = WinFormsColor.White;
                BackGround = 0;
                // Меняем цвет обводки кнопки на белый и текст внутри кнопки на белый для guna2Button2
                guna2Button2.BorderColor = WinFormsColor.Black;
                guna2Button2.ForeColor = WinFormsColor.Black;

                // Меняем цвет текста на белый для всех остальных элементов управления на форме
                foreach (WinFormsControl control in Controls)
                {
                    if (control is Label || control is Guna.UI2.WinForms.Guna2Button)
                    {
                        // Если элемент управления - это Label или Guna2Button, меняем его цвет текста на белый
                        control.ForeColor = WinFormsColor.Black;

                        // Если элемент управления - это Guna2Button, также меняем цвет обводки на белый
                        if (control is Guna.UI2.WinForms.Guna2Button)
                        {
                            ((Guna.UI2.WinForms.Guna2Button)control).BorderColor = WinFormsColor.Black;
                        }
                    }
                    // Добавьте другие типы элементов управления, которые нужно изменить на белый
                    showImage = Image.FromFile(@"C:/Users/Женя/Desktop/програмы/проект по кпияп/WindowsFormsApp1/unnamed.png"); // Загрузка изображения для показа текста
                    hideImage = Image.FromFile(@"C:/Users/Женя/Desktop/програмы/проект по кпияп/WindowsFormsApp1/closed_eye.png");// Загрузка изображения для скрытия текста

                }
            }
            string connectionString = "Server=localhost;Database=finance;Uid=root;Password=zhe27;";
            connection = new MySqlConnection(connectionString);
            this.Resize += Form13_Resize;
            guna2ComboBox1.Items.Add("оформить кредит");
            guna2ComboBox1.Items.Add("оформить карту");
        }
        private void Form13_Resize(object sender, EventArgs e)
        {
            // Проверяем, не меньше ли размер формы, чем минимальные значения
            if (this.Width < MinFormWidth)
            {
                this.Width = MinFormWidth; // Если меньше, устанавливаем минимальную ширину
            }
            if (this.Height < MinFormHeight)
            {
                this.Height = MinFormHeight; // Если меньше, устанавливаем минимальную высоту
            }
            // Рассчитываем позиции и размеры для кнопок
            int buttonWidth = (int)(this.Width * 0.2); // Ширина кнопок будет 20% ширины формы
            int buttonHeight = (int)(this.Height * 0.1); // Высота кнопок будет 10% высоты формы
            int buttonSpacing = 10; // Расстояние между кнопками и нижним краем формы

            guna2Button1.Width = buttonWidth;
            guna2Button1.Height = buttonHeight;
            guna2Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            guna2Button1.Location = new Point(10, this.ClientSize.Height - buttonHeight - buttonSpacing);

            guna2Button2.Width = buttonWidth;
            guna2Button2.Height = buttonHeight;
            guna2Button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            guna2Button2.Location = new Point((this.ClientSize.Width - buttonWidth) / 2, this.ClientSize.Height - buttonHeight - buttonSpacing);

           

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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string фамилия = guna2TextBox1.Text;
            string имя = guna2TextBox2.Text;
            string отчество = guna2TextBox3.Text;
            string время = guna2TextBox4.Text;
            string дата = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string кому = guna2ComboBox1.SelectedItem?.ToString(); // Предполагается, что в guna2ComboBox1 выбран получатель

            if (string.IsNullOrEmpty(фамилия) || string.IsNullOrEmpty(имя) || string.IsNullOrEmpty(отчество) ||
                string.IsNullOrEmpty(время) || string.IsNullOrEmpty(дата) || string.IsNullOrEmpty(кому))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            SaveDataToDatabase(фамилия, имя, отчество, время, дата, кому);
            SaveDataToWord(фамилия, имя, отчество, время, дата, кому);
        }

        private void SaveDataToDatabase(string фамилия, string имя, string отчество, string время, string дата, string кому)
        {
            string connectionString = "Server=localhost;Database=finance;Uid=root;Password=zhe27;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO `order` (фамилия, имя, отчество, время, дата, кому) VALUES (@фамилия, @имя, @отчество, @время, @дата, @кому)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@фамилия", фамилия);
                    command.Parameters.AddWithValue("@имя", имя);
                    command.Parameters.AddWithValue("@отчество", отчество);
                    command.Parameters.AddWithValue("@время", время);
                    command.Parameters.AddWithValue("@дата", дата);
                    command.Parameters.AddWithValue("@кому", кому);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Данные сохранены в базу данных.");
        }

        private void SaveDataToWord(string фамилия, string имя, string отчество, string время, string дата, string кому)
        {
            string filePath = $"SelectedRows_{фамилия}_{имя}.docx";

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(filePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());

                Paragraph title = body.AppendChild(new Paragraph());
                Run titleRun = title.AppendChild(new Run());
                titleRun.AppendChild(new Text("Документ для данных"));
                titleRun.AppendChild(new Break());

                AddParagraph(body, "Фамилия", фамилия);
                AddParagraph(body, "Имя", имя);
                AddParagraph(body, "Отчество", отчество);
                AddParagraph(body, "Время", время);
                AddParagraph(body, "Дата", дата);
                AddParagraph(body, "Кому", кому);

                int queuePosition = GetQueuePosition(дата); // Получаем позицию в очереди для указанной даты
                AddParagraph(body, "Позиция в очереди", queuePosition.ToString());

                mainPart.Document.Save();
            }

            MessageBox.Show("Данные сохранены в файл " + filePath);
        }

        private int GetQueuePosition(string дата)
        {
            int position = 0;
            string connectionString = "Server=localhost;Database=finance;Uid=root;Password=zhe27;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM `order` WHERE дата = @дата";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@дата", дата);

                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        int.TryParse(result.ToString(), out position);
                    }
                }
            }

            return position + 1; // Увеличиваем на 1, так как позиция начинается с 1, а не с 0
        }

        private void AddParagraph(Body body, string columnHeader, string cellValue)
        {
            Paragraph paragraph = body.AppendChild(new Paragraph());
            Run run = paragraph.AppendChild(new Run());
            run.AppendChild(new Text($"{columnHeader}: {cellValue}"));
            run.AppendChild(new Break());
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form14_Load(object sender, EventArgs e)
        {
           
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {
            guna2TextBox4.TextChanged -= guna2TextBox4_TextChanged;

            string input = guna2TextBox4.Text;

            // Ограничение ввода до 5 символов (включая двоеточие)
            if (input.Length > 5)
            {
                input = input.Substring(0, 5);
                guna2TextBox4.Text = input;
                guna2TextBox4.SelectionStart = input.Length; // Перемещаем курсор в конец текста
            }

            // Автоматически добавляем двоеточие после двух цифр
            if (input.Length == 2 && !input.Contains(":"))
            {
                input += ":";
                guna2TextBox4.Text = input;
                guna2TextBox4.SelectionStart = input.Length; // Перемещаем курсор в конец текста
            }

            // Проверяем, соответствует ли ввод формату HH:mm
            if (System.Text.RegularExpressions.Regex.IsMatch(input, @"^\d{1,2}:\d{2}$"))
            {
                string[] timeParts = input.Split(':');

                if (int.TryParse(timeParts[0], out int hours) && int.TryParse(timeParts[1], out int minutes))
                {
                    // Проверяем, находятся ли часы и минуты в допустимых диапазонах
                    if (hours >= 0 && hours < 24 && minutes >= 0 && minutes < 60)
                    {
                        // Ввод корректен
                       // Устанавливаем цвет текста на черный, если ввод правильный
                        guna2TextBox4.TextChanged += guna2TextBox4_TextChanged;
                        return;
                    }
                } 
            }

           
            guna2TextBox4.TextChanged += guna2TextBox4_TextChanged;
        }
    }
}
