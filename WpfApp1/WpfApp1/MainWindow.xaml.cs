using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BitmapImage mine;
        CGenerator gen = new CGenerator();
        int h = 0;
        int q = 5;

        public MainWindow()
        {
            InitializeComponent();

            mine = new BitmapImage(new Uri(@"pack://application:,,,/img/bomb.jpg", UriKind.Absolute));
        }



        private void b1_Click(object sender, RoutedEventArgs e)
        {
            h = 0;
            setka.Children.Clear();
            setka.IsEnabled = true;
            //
            gen.init(5);
            gen.plantMines(q);
            gen.calculate();

            //указыается количество строк и столбцов в сетке
            setka.Rows = 5;
            setka.Columns = 5;
            //указываются размеры сетки (число ячеек * (размер кнопки в ячейки + толщина её границ))
            setka.Width = 5 * (50 + 4);
            setka.Height = 5 * (50 + 4);
            //толщина границ сетки
            setka.Margin = new Thickness(3);

            this.Width = 5 * 70;
            this.Height = 6 * 72;

            for (int i = 0; i < 5 * 5; i++)
            {
                //создание кнопки
                Button btn = new Button();
                //запись номера кнопки
                btn.Tag = i;
                //установка размеров кнопки
                btn.Width = 50;
                btn.Height = 50;
                //текст на кнопке
                btn.Content = " ";
                //толщина границ кнопки
                btn.Margin = new Thickness(2);
                //при нажатии кнопки, будет вызываться метод Btn_Click
                btn.Click += Btn_Click; 
                //добавление кнопки в сетку
                setka.Children.Add(btn);
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            ////получение значения лежащего в Tag
            int n = (int)((Button)sender).Tag;


            if (gen.getCell(n % 5, n / 5) == 0)
            {
                gen.reveal(n % 5, n / 5);

                Button[] buts = new Button[setka.Children.Count];
                setka.Children.CopyTo(buts, 0);

                for (int i = 0; i < buts.Length; i++)
                {
                    int ind = (int)(buts[i]).Tag;

                    if (gen.getCell(ind % 5, ind / 5) == 10)
                    {

                        //установка фона нажатой кнопки, цвета и размера шрифта
                        (buts[i]).Background = Brushes.MediumPurple;
                        (buts[i]).Foreground = Brushes.Red;
                        (buts[i]).FontSize = 23;

                        //запись в нажатую кнопку её номера
                        (buts[i]).Content = 0;
                        h++;
                        (buts[i]).Click -= Btn_Click;
                    }
                }
            } else


            if (gen.getCell(n % 5, n / 5) > 0)
            {
                //установка фона нажатой кнопки, цвета и размера шрифта
                ((Button)sender).Background = Brushes.MediumPurple;
                ((Button)sender).Foreground = Brushes.Red;
                ((Button)sender).FontSize = 23;

                //запись в нажатую кнопку её номера
                ((Button)sender).Content = gen.getCell(n % 5, n / 5);

                h++;
                ((Button)sender).Click -= Btn_Click;
                if ((25 - h) == q) { MessageBox.Show("Du bist WINNER!"); }

            } else

            if (gen.getCell(n % 5, n / 5) == -1)
            {

                Button[] buts = new Button[setka.Children.Count];
                setka.Children.CopyTo(buts, 0);

                for(int i = 0; i < buts.Length; i++)
                {
                    int ind = (int)(buts[i]).Tag;

                    if (gen.getCell(ind % 5, ind / 5) == -1)
                    {

                        Image img = new Image();
                        img.Source = mine;
                        //создание переменной для отображения изображения мины
                        StackPanel minePnl;
                        //инициализация и установка ориентации, можно вызвать в методе инициализации формы
                        minePnl = new StackPanel();
                        // minePnl.Orientation = Orientation.Vertical;
                        //установка толщины границы объекта
                        minePnl.Margin = new Thickness(1);
                        //добавление в объект изображения
                        minePnl.Children.Add(img);

                        (buts[i]).Content = minePnl;
                    }
                    
                }
                MessageBox.Show("you are R E D I S K A ");
                setka.IsEnabled = false;


            }
        }

    }
}
