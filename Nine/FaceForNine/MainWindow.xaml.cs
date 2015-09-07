using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FaceForNine
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

        Timer timer;
        Graph graph;
        List<int> path;
        int currentMtx;


		public MainWindow()
		{
			this.InitializeComponent();

           //Создаем пустое поле для игры
            int[] startMatr = new int[Consts.SIZE*Consts.SIZE]; 
            for (int i = 0; i < Consts.SIZE * Consts.SIZE; i++)
            {
                startMatr[i] = 0;
            }

            CreateField(startMatr);
		}

		private void Start(object sender, System.Windows.RoutedEventArgs e)
		{
            int[] matr = new int[Consts.SIZE*Consts.SIZE];
            bool ZeroWas = false;
            //Считываем в массив ввеленные пользователем числа
            /*for (int i = 0; i < matr.Length; i++ ) 
            {
                if (!String.IsNullOrEmpty(((TextBox)Field.Children[i]).Text ))
                {
                    matr[i] = Convert.ToInt32(((TextBox)Field.Children[i]).Text);
                }
                else
                {
                    if (!ZeroWas)
                    {
                        matr[i] = 0;
                        ZeroWas = true;
                    }
                    else
                    {
                        MessageBox.Show("Введите числа");
                        return;
                    }
                }
            }*/


            graph = new Graph(); //Создали граф
            Random r = new Random();
          //  Permutation per = new Permutation(matr);
           Permutation per = new Permutation(new int[] {0,1,2,3,4,5,6,7,8});
            for (int j = 1; j < 50; j++)//проходим по всем вершинам
            {
                int i = r.Next(4);
                Permutation per1 = null;
                switch (i)
                {
                    case 0:
                        per1 = per.Left();
                        break;
                    case 1:
                        per1 = per.Right();
                        break;
                    case 2:
                        per1 = per.Up();
                        break;
                    case 3:
                        per1 = per.Down();
                        break;
                }

                if (per1 != null)
                {
                    per = per1;
                }
            }

                path = graph.Dijkstra(graph.GetNumberOfPermutation(per)); //Нашли путь

                if (path == null)
                {
                    MessageBox.Show("Это конечная");
                }
                else
                {
                currentMtx = path.Count - 1; //Текущая матрица
                Print(per.AllNumbers()); //Отрисовка

                //Timer
                timer = new Timer();
                timer.Interval = 1000;
                timer.Elapsed += TimeIs;
                timer.Start();
                //endTimer
            }
		}



        private void TimeIs(object sender, ElapsedEventArgs e)
        {
           // MessageBox.Show(currentMtx.ToString());
           if (currentMtx>=0) //Если еще не подошли к первой матрице
           {
               Dispatcher.Invoke(DispatcherPriority.Normal, new Action<int[]>(Print), graph.graphModel[path[currentMtx]].AllNumbers()); //Отрисовка
               currentMtx--; //Шагаем дальше
           }
           else
           {
               Dispatcher.Invoke(DispatcherPriority.Normal, new Action<int[]>(Print), graph.graphModel[0].AllNumbers());
               timer.Stop();
           }
        }


       


		//Создание пустого поля для игры
		private void CreateField (int[] matr)
		{
			int side = (int)(237 / Math.Sqrt(matr.Length));
			
			for (int i = 0; i < matr.Length; i++)
			{
				int leftPlace = (((int)(i % Math.Sqrt(matr.Length)))*side);
				int topPlace = ((i / (int)(Math.Sqrt(matr.Length)))*side);
				TextBox textBox = new TextBox();
				textBox.FontSize = 50;
				textBox.TextAlignment = TextAlignment.Center;
				
				var bc = new BrushConverter();
                textBox.Background = (Brush)bc.ConvertFrom("#FFD6D6D6");
				
				if (matr[i] != 0)
					textBox.Text = Convert.ToString(matr[i]);
				
				textBox.Width = side - 1;
				textBox.Height = side - 1;
				
				Canvas.SetLeft(textBox, leftPlace);
				Canvas.SetTop(textBox, topPlace);
				
			    Field.Children.Add(textBox);
			}
		}
		


        //Отрисовка
		private void Print(int[] matr)
		{
			for (int i = 0; i < matr.Length; i++)
			{
                if (matr[i] == 0)
                {
                    var bc = new BrushConverter();
                    ((TextBox)Field.Children[i]).Background = (Brush)bc.ConvertFrom("#FF00F9FF");
                    ((TextBox)Field.Children[i]).BorderThickness = new Thickness(0);
                    ((TextBox)Field.Children[i]).Text = "";
                }

                else
                {
                    ((TextBox)Field.Children[i]).Text = Convert.ToString(matr[i]);
                    var bc = new BrushConverter();
                    ((TextBox)Field.Children[i]).Background = (Brush)bc.ConvertFrom("#FFD6D6D6");
                }
			}
		}
	}
}