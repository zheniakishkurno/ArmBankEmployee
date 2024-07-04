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
    public partial class Form10 : Form
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


        public Form10(int background, int plauplay, string username)
        {
            InitializeComponent();
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
            string connectionString = "Server=localhost;Database=finance;Uid=root;Password=zhe27;";
            connection = new MySqlConnection(connectionString);
            this.Resize += Form10_Resize;
        }
        private void Form10_Resize(object sender, EventArgs e)
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

            // Рассчитываем размеры DataGridView
            guna2DataGridView1.Width = this.ClientSize.Width - 50;
            guna2DataGridView1.Height = this.ClientSize.Height - 2 * buttonHeight - 3 * buttonSpacing;
            guna2DataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            guna2DataGridView1.Location = new Point(25, buttonSpacing + buttonHeight);

        }
        private void LoadData()
        {
            string query = "SELECT * FROM credit;"; // SQL-запрос для выборки всех данных из таблицы program

            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Установка источника данных DataGridView
                guna2DataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
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

        private void Form10_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

            // Проверяем, есть ли хотя бы одна выделенная строка в DataGridView
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                // Получаем идентификаторы строк для удаления из базы данных
                List<int> idsToDelete = new List<int>();

                foreach (DataGridViewRow row in guna2DataGridView1.SelectedRows)
                {
                    if (!row.IsNewRow) // Исключаем строку добавления новых данных
                    {
                        int idcredit = Convert.ToInt32(row.Cells["idcredit"].Value); // Предположим, что "id" - это имя столбца с уникальным идентификатором строки
                        idsToDelete.Add(idcredit);
                    }
                }

                // Удаляем строки из базы данных
                DeleteRowsFromDatabase(idsToDelete);

                // Удаляем строки из DataGridView
                foreach (DataGridViewRow row in guna2DataGridView1.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        guna2DataGridView1.Rows.Remove(row);
                    }
                }
            }
            else
            {
                MessageBox.Show("Нет выделенных строк для удаления.");
            }
        }

        // Метод для удаления строк из базы данных
        private void DeleteRowsFromDatabase(List<int> ids)
        {
            try
            {
                connection.Open();

                foreach (int idcredit in ids)
                {
                    string query = $"DELETE FROM credit WHERE idcredit = {idcredit};"; // Предположим, что "program" - это имя вашей таблицы
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении строк из базы данных: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
