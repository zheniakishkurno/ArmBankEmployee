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
    public partial class Form9 : Form
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

        public Form9(int background, int plauplay, string username)
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

        private void Form9_Load(object sender, EventArgs e)
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
                List<DataGridViewRow> rowsToSave = new List<DataGridViewRow>();

                foreach (DataGridViewRow row in guna2DataGridView1.SelectedRows)
                {
                    if (!row.IsNewRow) // Исключаем строку добавления новых данных
                    {
                        int idcredit = Convert.ToInt32(row.Cells["idcredit"].Value); // Предположим, что "idcredit" - это имя столбца с уникальным идентификатором строки
                        idsToDelete.Add(idcredit);
                        rowsToSave.Add(row);
                    }
                }

                // Сохраняем строки в Word
                SaveRowsToWord(rowsToSave);
            }
            else
            {
                MessageBox.Show("Нет выделенных строк .");
            }
        }

        private void SaveRowsToWord(List<DataGridViewRow> rows)
        {
            string filePath = $"SelectedRows_{rows[0].Cells["creditcol"].Value}_{rows[0].Cells["idcredit"].Value}.docx";

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(filePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());

                // Добавляем заголовок документа
                Paragraph title = body.AppendChild(new Paragraph());
                Run titleRun = title.AppendChild(new Run());
                titleRun.AppendChild(new Text("Документ для кредита"));
                titleRun.AppendChild(new Break());

                // Добавляем данные о кредите
                foreach (DataGridViewRow row in rows)
                {
                    // Добавляем заголовок строки
                    Paragraph rowTitle = body.AppendChild(new Paragraph());
                    Run rowTitleRun = rowTitle.AppendChild(new Run());
                    rowTitleRun.AppendChild(new Text("Новая информация:"));
                    rowTitleRun.AppendChild(new Break());

                    // Добавляем данные из ячеек строки
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string columnHeader = GetColumnHeaderName(cell.OwningColumn.HeaderText);
                        string cellValue = GetCellValue(cell);

                        Paragraph cellParagraph = body.AppendChild(new Paragraph());
                        Run cellRun = cellParagraph.AppendChild(new Run());
                        cellRun.AppendChild(new Text($"{columnHeader}: {cellValue}"));
                        cellRun.AppendChild(new Break());
                    }
                }

                mainPart.Document.Save();
            }

            MessageBox.Show("Выбранные строки сохранены в файл " + filePath);
        }


        // Метод для преобразования заголовков столбцов
        private string GetColumnHeaderName(string columnHeader)
        {
            switch (columnHeader)
            {

                case "creditcol":
                    return "id";
                case "фамилия":
                    return "фамилия";
                case "имя":
                    return "Имя";
                case "отчество":
                    return "отчество";
                case "идентификационный_номер":
                    return "идентификационный номер";
                case "адрес":
                    return "адрес";
                case "возраст":
                    return "возраст";
                case "номер_телефона":
                    return "номер телефона";
                case "доход":
                    return "доход";
                case "пол":
                    return "пол";
                case "дата_выдачи":
                    return "дата выдачи";
                case "срок действия":
                    return "срок действия";
                default:
                    return columnHeader;
            }
        }

        // Метод для преобразования значения ячейки
        private string GetCellValue(DataGridViewCell cell)
        {
            if (cell.OwningColumn.HeaderText == "дата_выдачи" || cell.OwningColumn.HeaderText == "срок_действия")
            {
                DateTime dateValue;
                if (DateTime.TryParse(cell.Value.ToString(), out dateValue))
                {
                    return dateValue.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            return cell.Value.ToString();
        }



        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {

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

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
