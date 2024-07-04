using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using MySql.Data.MySqlClient;
using WMPLib;


namespace WindowsFormsApp1
{
    public partial class Form4 : Form
    {
        private string Username;
        public int Plauplay = 0;
        public int BackGround = 0;

        private const int ButtonWidth = 90;
        private const int ButtonHeight = 84;
        private const int ButtonSpacing = 10; // Отступ между кнопками

        private static WindowsMediaPlayer player = new WindowsMediaPlayer();
        private static string[] songs = { "song1.mp3" };
        private static int currentSongIndex = 0;
        private static int volumeLevel = 25;
        private Timer checkPlaybackTimer;

        private const int MinFormWidth = 818;
        private const int MinFormHeight = 497;

        private Image showImage = Image.FromFile(@"C:/Users/Женя/Desktop/програмы/проект по кпияп/WindowsFormsApp1/unnamed.png");// Загрузка изображения для показа текста
        private Image hideImage = Image.FromFile(@"C:/Users/Женя/Desktop/програмы/проект по кпияп/WindowsFormsApp1/closed_eye.png");// Загрузка изображения для скрытия текста

        public static void Play()
        {
            if (player != null)
            {
                player.URL = songs[currentSongIndex];
                player.controls.play();
            }
        }

        public static void Pause()
        {
            if (player != null)
            {
                player.controls.pause();
            }
        }
        public Form4(int background, int plauplay, string username)
        {

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100); // Установка координат X и Y для местоположения формы

            InitializeComponent();
            // Другие операции и инициализации формы
            this.Username = username;

            // Привязываем обработчики событий для кнопок и ползунка громкости
            playButton.Click += playButton_Click_1;
            guna2TrackBar1.Scroll += VolumeTrackBar_Scroll;

            Resize += YourForm_Resize;

            Plauplay = plauplay;
            BackGround = background;
            if (BackGround == 1)
            {
                // Меняем цвет формы на черный
                this.BackColor = Color.Black;
                BackGround = 1;
                // Меняем цвет обводки кнопки на белый и текст внутри кнопки на белый для guna2Button2
                guna2Button1.BorderColor = Color.White;
                guna2Button1.ForeColor = Color.White;

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
                guna2Button1.BorderColor = Color.Black;
                guna2Button1.ForeColor = Color.Black;

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

            // Устанавливаем свойство Anchor для Label
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            // Устанавливаем обработчик события изменения размера формы
            this.Resize += Form4_Resize;

            // Инициализируем центрирование Label при загрузке формы
            CenterLabel();

            // Инициализируем таймер
            checkPlaybackTimer = new Timer();
            checkPlaybackTimer.Interval = 1000; // Проверяем каждую секунду
            checkPlaybackTimer.Tick += CheckPlaybackTimer_Tick;
            checkPlaybackTimer.Start();
        }

        // Обработчик события таймера для проверки воспроизведения
        private void CheckPlaybackTimer_Tick(object sender, EventArgs e)
        {
            // Если воспроизведение завершено и плеер не в процессе воспроизведения
            if (player.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                // Запускаем воспроизведение заново
                Play();
            }
        }

        private void YourForm_Resize(object sender, EventArgs e)
        {
        }


        private void Form4_Resize(object sender, EventArgs e)
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
            // Установка размеров кнопки guna2Button1 пропорционально изменению размеров формы
            guna2Button1.Width = (int)(this.Width * 0.2); // Например, кнопка будет занимать 20% ширины формы
            guna2Button1.Height = (int)(this.Height * 0.1); // Например, кнопка будет занимать 10% высоты формы

            // При изменении размера формы вызываем метод центрирования Label
            CenterLabel();
            // В конструкторе формы или методе инициализации добавьте следующий код для установки свойств кнопки:
            guna2Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            guna2Button1.Location = new Point(10, this.ClientSize.Height - guna2Button1.Height - 10);

        }
        private void CenterLabel()
        {
            // Рассчитываем новые координаты для центрирования Label по горизонтали
            int labelX = (ClientSize.Width - label1.Width) / 2;
            int labelY = label1.Location.Y; // Не изменяем положение Label по вертикали

            // Устанавливаем новые координаты Label
            label1.Location = new System.Drawing.Point(labelX, labelY);
        }


        private void Form4_Load(object sender, EventArgs e)
        {
            // Устанавливаем положение формы
            this.Location = new Point(100, 100); // Пример: x=100, y=100
                                                 // Ensure the volume level falls within the valid range of the trackbar
            int volume = Math.Max(guna2TrackBar1.Minimum, Math.Min(volumeLevel, guna2TrackBar1.Maximum));

            // Load the volume level from the static variable and apply it to the player
            guna2TrackBar1.Value = volume;
            player.settings.volume = volume;
        }

        // Функция для проверки, является ли пользователь обычным пользователем
        bool CheckUser(string username)
        {

            string connectionString = "Server=localhost;Database=finance;Uid=root;Password=zhe27;";
            string query = "SELECT COUNT(*) FROM user WHERE username = @Username";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                    return count > 0;
                }
            }
        }

        // Функция для проверки, является ли пользователь администратором
        bool CheckAdmin(string username)
        {

            string connectionString = "Server=localhost;Database=finance;Uid=root;Password=zhe27;";
            string query = "SELECT COUNT(*) FROM admin WHERE username = @Username";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                    return count > 0;
                }
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Username))
            {
                bool isUser = false;
                bool isAdmin = false;

                if (!string.IsNullOrEmpty(Username))
                {
                    isUser = CheckUser(Username); // Проверяем, является ли пользователь обычным пользователем
                    isAdmin = CheckAdmin(Username); // Проверяем, является ли пользователь администратором
                }

                if (isAdmin)
                {
                    // Если пользователь является администратором, открываем Form6
                    Form6 form6 = new Form6(BackGround, Plauplay, Username);
                    form6.Size = this.Size;
                    form6.Show();
                }
                else if (isUser)
                {
                    // Если пользователь является обычным пользователем, открываем Form7
                    Form7 form7 = new Form7(BackGround, Plauplay, Username);
                    form7.Size = this.Size;
                    form7.Show();
                }


                // Скрываем текущую форму
                this.Hide();
            }
            else
            {
                // Если пользователя не существует ни в одной из таблиц, открываем Form1
                Form1 form1 = new Form1(BackGround, Plauplay);
                form1.Size = this.Size;
                form1.Show();
                // Скрываем текущую форму
                this.Hide();
            }

        }

        private void sToggleButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (sToggleButton1.Checked)
            {
                // Если кнопка включена, то меняем цвет фона и текста на белый
                sToggleButton1.BackColor = Color.White;
                sToggleButton1.ForeColor = Color.Black;

                // Меняем цвет формы на черный
                this.BackColor = Color.Black;
                BackGround = 1;
                // Меняем цвет обводки кнопки на белый и текст внутри кнопки на белый для guna2Button2
                guna2Button1.BorderColor = Color.White;
                guna2Button1.ForeColor = Color.White;
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
                // Если кнопка выключена, то меняем цвет фона и текста на черный
                sToggleButton1.BackColor = Color.Black;
                sToggleButton1.ForeColor = Color.White;

                // Меняем цвет формы на белый
                this.BackColor = Color.White;
                BackGround = 0;
                // Меняем цвет обводки кнопки на черный и текст внутри кнопки на черный для guna2Button2
                guna2Button1.BorderColor = Color.Black;
                guna2Button1.ForeColor = Color.Black;
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

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Pause();
        }

        private void guna2TrackBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }
        private void VolumeTrackBar_Scroll(object sender, EventArgs e)
        {
            // Adjust the volume based on the trackbar value
            player.settings.volume = guna2TrackBar1.Value;

            // Update the static variable with the current volume level
            volumeLevel = guna2TrackBar1.Value;
        }

        private void playButton_Click(object sender, EventArgs e)
        {
        }

        private void playButton_Click_1(object sender, EventArgs e)
        {
            Play();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }
    }
}
