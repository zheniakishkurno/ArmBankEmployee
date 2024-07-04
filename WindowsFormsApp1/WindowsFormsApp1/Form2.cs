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

    public partial class Form2 : Form
    {
        public int BackGround = 0;
        public int Plauplay = 0;
        // ToolTip для отображения подсказок
        private static WindowsMediaPlayer player = new WindowsMediaPlayer();
        private static string[] songs = { "song1.mp3" };
        private static int currentSongIndex = 0;

        private const int MinFormWidth = 818;
        private const int MinFormHeight = 497;

        private bool isTextVisible = false;

        private Image showImage = Image.FromFile(@"C:/Users/Женя/Desktop/програмы/проект по кпияп/WindowsFormsApp1/unnamed.png");// Загрузка изображения для показа текста
        private Image hideImage = Image.FromFile(@"C:/Users/Женя/Desktop/програмы/проект по кпияп/WindowsFormsApp1/closed_eye.png");// Загрузка изображения для скрытия текста

        public Form2(int background, int plauplay)
        {
            InitializeComponent();
            guna2TextBox2.UseSystemPasswordChar = true; // Начально скрываем текст
            guna2Button3.Image = showImage; // Устанавливаем начальное изображение для кнопки

            guna2TextBox3.UseSystemPasswordChar = true; // Начально скрываем текст
            guna2Button4.Image = showImage; // Устанавливаем начальное изображение для кнопки

            // Инициализация ToolTip для TextBox
            toolTipForTextBox = new ToolTip();
            // Устанавливаем подсказку для TextBox1
            toolTipForTextBox.SetToolTip(guna2TextBox1, "Введите текст не менее 4 символов на английском языке в первом поле");
            // Инициализация ToolTip для TextBox
            toolTipForTextBox1 = new ToolTip();
            // Устанавливаем подсказку для TextBox1
            toolTipForTextBox1.SetToolTip(guna2TextBox2, "Введите текст от 4 до 16 символов во втором поле");
            // Инициализация ToolTip для TextBox
            toolTipForTextBox2 = new ToolTip();
            // Устанавливаем подсказку для TextBox1
            toolTipForTextBox2.SetToolTip(guna2TextBox3, "Должен совподать паролем ");


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
            // Устанавливаем обработчик события изменения размера формы
            this.Resize += Form1_Resize;

            // Инициализируем центрирование Label при загрузке формы
            CenterLabel();

        }

        private void Form1_Resize(object sender, EventArgs e)
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
            // Установка размеров кнопок пропорционально изменению размеров формы
            guna2Button1.Width = (int)(this.Width * 0.2); // Например, кнопка будет занимать 20% ширины формы
            guna2Button1.Height = (int)(this.Height * 0.1); // Например, кнопка будет занимать 10% высоты формы
            guna2Button2.Width = (int)(this.Width * 0.2);
            guna2Button2.Height = (int)(this.Height * 0.1);

            // При изменении размера формы вызываем метод центрирования Label
            CenterLabel();
            // Определение отступов и других параметров
            int textBoxHorizontalPadding = 160; // Отступ от левой и правой сторон guna2TextBox
            int labelOffset = 10; // Отступ между меткой и текстовым полем
            int label1ffset = 80;

            // Установка анкера и позиции для guna2Button1
            guna2Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            guna2Button1.Location = new Point(10, this.ClientSize.Height - guna2Button1.Height - 10);

            // Установка анкера и позиции для guna2Button2
            guna2Button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            guna2Button2.Location = new Point(this.ClientSize.Width - guna2Button2.Width - 10, this.ClientSize.Height - guna2Button2.Height - 10);

            // Расчет горизонтальной координаты центра формы
            int centerX = this.ClientSize.Width / 2;
            // Расчет вертикальной координаты центра формы с учетом смещения вверх
            int centerY = this.ClientSize.Height / 2 - 50; // Уменьшаем на 50 пикселей

            // Определение отступа между текстовыми полями
            int textBoxVerticalMargin = 20;

            // Расчет ширины текстовых полей с учетом отступов и уменьшения на 20 пикселей
            int textBoxWidth = this.ClientSize.Width - 3 * textBoxHorizontalPadding - 40; // Уменьшаем на 20 пикселей

            // Установка ширины текстовых полей
            guna2TextBox1.Width = textBoxWidth;
            guna2TextBox2.Width = textBoxWidth;
            guna2TextBox3.Width = textBoxWidth;

            // Расчет вертикальной координаты для guna2TextBox1, чтобы его центр совпадал с центром формы
            int textBox1Y = centerY - guna2TextBox1.Height / 2;

            // Установка координат для guna2TextBox1 в центре формы по горизонтали
            guna2TextBox1.Left = centerX - guna2TextBox1.Width / 2;

            // Установка координат для guna2TextBox1 в центре формы по вертикали
            guna2TextBox1.Top = textBox1Y;

            // Расчет вертикальной координаты для guna2TextBox2, с учетом отступа
            int textBox2Y = textBox1Y + guna2TextBox1.Height + textBoxVerticalMargin;

            // Установка координат для guna2TextBox2
            guna2TextBox2.Left = centerX - guna2TextBox2.Width / 2;
            guna2TextBox2.Top = textBox2Y;

            // Расчет вертикальной координаты для guna2TextBox3, с учетом отступа
            int textBox3Y = textBox2Y + guna2TextBox2.Height + textBoxVerticalMargin;

            // Установка координат для guna2TextBox3
            guna2TextBox3.Left = centerX - guna2TextBox3.Width / 2;
            guna2TextBox3.Top = textBox3Y;

            // Уменьшаем значение отступа между метками и текстовыми полями
            int labelOffset2 = 2; // Устанавливаем новое значение отступа

            // Расчет новых координат для меток
            int labelX = textBoxHorizontalPadding - label2.Width - labelOffset2;
            int labelX1 = textBoxHorizontalPadding - label2.Width - label1ffset;

            // Установка позиции и отступа для label2 перед guna2TextBox1
            label2.Location = new Point(labelX, guna2TextBox1.Top + (guna2TextBox1.Height - label2.Height) / 2);

            // Установка позиции и отступа для label3 перед guna2TextBox2
            label3.Location = new Point(labelX, guna2TextBox2.Top + (guna2TextBox2.Height - label3.Height) / 2);

            // Установка позиции и отступа для label4 перед guna2TextBox3
            label4.Location = new Point(labelX1, guna2TextBox3.Top + (guna2TextBox3.Height - label4.Height) / 2);

            // Вычисляем координату X для guna2Button3
            int button3X = guna2TextBox2.Right + 10; // Добавляем отступ от правого края guna2TextBox2

            // Устанавливаем координаты для guna2Button3
            guna2Button4.Location = new Point(button3X, guna2TextBox2.Top);

            // Вычисляем координату X для guna2Button4
            int button4X = guna2TextBox3.Right + 10; // Добавляем отступ от правого края guna2TextBox3

            // Устанавливаем координаты для guna2Button4
            guna2Button3.Location = new Point(button4X, guna2TextBox3.Top);

            // Обновление расположения существующего LinkLabel под guna2TextBox3
            linkLabel1.Left = centerX - linkLabel1.Width / 2;
            linkLabel1.Top = guna2TextBox3.Bottom + 10; // Расположение под guna2TextBox3 с отступом


        }

        private void CenterLabel()
        {
            // Рассчитываем новые координаты для центрирования Label по горизонтали
            int labelX = (ClientSize.Width - label1.Width) / 2;
            int labelY = label1.Location.Y; // Не изменяем положение Label по вертикали

            // Устанавливаем новые координаты Label
            label1.Location = new System.Drawing.Point(labelX, labelY);
        }


        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(BackGround, Plauplay);
            form1.Size = this.Size;
            // Показываем Form2 и скрываем текущую форму (Form1)
            form1.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

            // Проверка валидации для guna2TextBox1
            if (guna2TextBox1.Text.Length < 4 || !IsEnglish(guna2TextBox1.Text))
            {
                ShowError(guna2TextBox1, "Введите не менее 4 символов на английском языке .");
            }
            else
            {
                SetValid(guna2TextBox1);
            }

            // Проверка валидации для guna2TextBox2
            Regex englishAndDigitsRegex = new Regex("^[a-zA-Z0-9]{4,16}$");
            if (!englishAndDigitsRegex.IsMatch(guna2TextBox2.Text))
            {
                ShowError(guna2TextBox2, "Введите от 4 до 16 символов на английском языке или цифры во втором поле.");
            }
            else
            {
                SetValid(guna2TextBox2);
            }



            // Проверка на совпадение значений guna2TextBox2 и guna2TextBox3
            if (guna2TextBox2.Text != guna2TextBox3.Text)
            {
                ShowError(guna2TextBox2, "Пароли не совпадают.");
                ShowError(guna2TextBox3, "Пароли не совпадают.");
            }
            else
            {
                SetValid(guna2TextBox2);
                SetValid(guna2TextBox3);
            }

            // Если все поля прошли валидацию, сохраняем логин и пароль в базу данных
            if (AllValid())
            {
                try
                {
                    string connectionString = "Server=localhost;Database=finance;Uid=root;Password=zhe27;";

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO user (username, password) VALUES (@username, @password)";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@username", guna2TextBox1.Text);
                            command.Parameters.AddWithValue("@password", guna2TextBox2.Text);
                            command.ExecuteNonQuery();
                        }

                        MessageBox.Show("Логин и пароль успешно сохранены в базе данных.");
                        OpenForm1();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка: " + ex.Message);
                }
            }

            // Метод для отображения сообщения об ошибке и изменения цвета рамки
            void ShowError(Guna.UI2.WinForms.Guna2TextBox textBox, string errorMessage)
            {
                textBox.BorderColor = System.Drawing.Color.Red;
                MessageBox.Show(errorMessage);
            }

            // Метод для установки цвета рамки валидного поля
            void SetValid(Guna.UI2.WinForms.Guna2TextBox textBox)
            {
                textBox.BorderColor = System.Drawing.Color.Gray;
            }

            // Метод для проверки всех полей на валидность
            bool AllValid()
            {
                return guna2TextBox1.BorderColor == System.Drawing.Color.Gray &&
                       guna2TextBox2.BorderColor == System.Drawing.Color.Gray &&
                       guna2TextBox3.BorderColor == System.Drawing.Color.Gray;
            }

            // Метод для открытия формы Form1
            void OpenForm1()
            {
                Form1 form1 = new Form1(BackGround, Plauplay);
                form1.Show();
                form1.Size = this.Size;
                this.Close();
            }

        }

        private bool IsEnglish(string text)
        {
            return Regex.IsMatch(text, @"^[a-zA-Z0-9]+$");
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (isTextVisible)
            {
                guna2TextBox3.UseSystemPasswordChar = true; // Скрываем текст
                guna2Button3.Image = showImage; // Устанавливаем изображение для показа текста
            }
            else
            {
                guna2TextBox3.UseSystemPasswordChar = false; // Показываем текст
                guna2Button3.Image = hideImage; // Устанавливаем изображение для скрытия текста
            }

            isTextVisible = !isTextVisible; // Инвертируем состояние кнопки
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (isTextVisible)
            {
                guna2TextBox2.UseSystemPasswordChar = true; // Скрываем текст
                guna2Button4.Image = showImage; // Устанавливаем изображение для показа текста
            }
            else
            {
                guna2TextBox2.UseSystemPasswordChar = false; // Показываем текст
                guna2Button4.Image = hideImage; // Устанавливаем изображение для скрытия текста
            }

            isTextVisible = !isTextVisible; // Инвертируем состояние кнопки
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form3 form3 = new Form3(BackGround, Plauplay);
            form3.Size = this.Size;
            // Показываем Form2 и скрываем текущую форму (Form1)
            form3.Show();
            this.Hide();
        }
    }
}
