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

    public partial class Form7 : Form
    {
        private string Username;
        public int BackGround = 0;
        public int Plauplay = 0;
        private static WindowsMediaPlayer player = new WindowsMediaPlayer();
        private static string[] songs = { "song1.mp3" };
        private static int currentSongIndex = 0;

        private const int MinFormWidth = 818;
        private const int MinFormHeight = 688;
        public Form7(int background, int plauplay, string username)
        {
            InitializeComponent();
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
            // Другие операции и инициализации формы
            this.Username = username;
            label1.Text = "Добро пожаловать, " + username + "!";

            this.Resize += Form7_Resize;
        }
        private void Form7_Resize(object sender, EventArgs e)
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
            guna2Button2.Width = (int)(this.Width * 0.2); // Например, кнопка будет занимать 20% ширины формы
            guna2Button2.Height = (int)(this.Height * 0.1); // Например, кнопка будет занимать 10% высоты формы
            // Установка размеров кнопки guna2Button1 пропорционально изменению размеров формы
            guna2Button3.Width = (int)(this.Width * 0.2); // Например, кнопка будет занимать 20% ширины формы
            guna2Button3.Height = (int)(this.Height * 0.1); // Например, кнопка будет занимать 10% высоты формы
            // Установка размеров кнопки guna2Button1 пропорционально изменению размеров формы
            guna2Button4.Width = (int)(this.Width * 0.2); // Например, кнопка будет занимать 20% ширины формы
            guna2Button4.Height = (int)(this.Height * 0.1); // Например, кнопка будет занимать 10% высоты формы


            // При изменении размера формы вызываем метод центрирования Label
            CenterLabel();
            // При изменении размера формы вызываем метод центрирования кнопок
            CenterButtons();
            // Определение отступов и других параметров

            // Установка анкера и позиции для guna2Button1
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
        private void CenterButtons()
        {
            // Рассчитываем новые координаты для центрирования кнопок и их размеры
            int buttonWidth = (int)(ClientSize.Width * 0.2); // Ширина кнопок - 20% от ширины формы
            int buttonHeight = 50; // Задаем фиксированную высоту кнопок
            int spacing = 10; // Расстояние между кнопками

            int buttonX = (ClientSize.Width - buttonWidth) / 2;
            int buttonY = (ClientSize.Height - (9 * buttonHeight + 2 * spacing)) / 2;

            // Устанавливаем новые координаты и размеры кнопок
            guna2Button1.Size = new Size(buttonWidth, buttonHeight);
            guna2Button2.Size = new Size(buttonWidth, buttonHeight);
            guna2Button3.Size = new Size(buttonWidth, buttonHeight);
            guna2Button4.Size = new Size(buttonWidth, buttonHeight);
            guna2Button5.Size = new Size(buttonWidth, buttonHeight);
            guna2Button6.Size = new Size(buttonWidth, buttonHeight);
            guna2Button7.Size = new Size(buttonWidth, buttonHeight);
            guna2Button8.Size = new Size(buttonWidth, buttonHeight);
            guna2Button9.Size = new Size(buttonWidth, buttonHeight);
            guna2Button10.Size = new Size(buttonWidth, buttonHeight);

            guna2Button1.Location = new Point(buttonX, buttonY);
            guna2Button2.Location = new Point(buttonX, buttonY);
            guna2Button3.Location = new Point(buttonX, buttonY + buttonHeight + spacing);
            guna2Button4.Location = new Point(buttonX, buttonY + 2 * (buttonHeight + spacing));
            guna2Button5.Location = new Point(buttonX, buttonY + 3 * (buttonHeight + spacing));
            guna2Button6.Location = new Point(buttonX, buttonY + 4 * (buttonHeight + spacing));
            guna2Button7.Location = new Point(buttonX, buttonY + 5 * (buttonHeight + spacing));
            guna2Button8.Location = new Point(buttonX, buttonY + 6 * (buttonHeight + spacing));
            guna2Button9.Location = new Point(buttonX, buttonY + 7 * (buttonHeight + spacing));
            guna2Button10.Location = new Point(buttonX, buttonY + 8 * (buttonHeight + spacing));

            // Установка размеров кнопки guna2Button1 пропорционально изменению размеров формы
            guna2Button1.Width = (int)(this.Width * 0.2); // Например, кнопка будет занимать 20% ширины формы
            guna2Button1.Height = (int)(this.Height * 0.1); // Например, кнопка будет занимать 10% высоты формы

            guna2Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            guna2Button1.Location = new Point(10, this.ClientSize.Height - guna2Button1.Height - 10);
        }

        private void Form7_Load(object sender, EventArgs e)
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
            Form4 form4 = new Form4(BackGround, Plauplay, Username);
            form4.Size = this.Size;
            // Показываем Form2 и скрываем текущую форму (Form1)
            form4.Show();
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8(BackGround, Plauplay, Username);
            form8.Show();
            form8.Size = this.Size;
            this.Hide(); // Скрываем текущую форму
            return;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Form10 form10 = new Form10(BackGround, Plauplay, Username);
            form10.Show();
            form10.Size = this.Size;
            this.Hide(); // Скрываем текущую форму
            return;
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9(BackGround, Plauplay, Username);
            form9.Show();
            form9.Size = this.Size;
            this.Hide(); // Скрываем текущую форму
            return;
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5(BackGround, Plauplay, Username);
            form5.Show();
            form5.Size = this.Size;
            this.Hide(); // Скрываем текущую форму
            return;
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            Form12 form12 = new Form12(BackGround, Plauplay, Username);
            form12.Show();
            form12.Size = this.Size;
            this.Hide(); // Скрываем текущую форму
            return;
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            Form13 form13 = new Form13(BackGround, Plauplay, Username);
            form13.Show();
            form13.Size = this.Size;
            this.Hide(); // Скрываем текущую форму
            return;
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            Form14 form14 = new Form14(BackGround, Plauplay, Username);
            form14.Show();
            form14.Size = this.Size;
            this.Hide(); // Скрываем текущую форму
            return;
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            Form15 form15 = new Form15(BackGround, Plauplay, Username);
            form15.Show();
            form15.Size = this.Size;
            this.Hide(); // Скрываем текущую форму
            return;
        }
    }
}
