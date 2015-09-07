using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FaceForNine
{
    /// <summary>
    /// Граф, вершинами которого являются перестановки в нашей игре
    /// </summary>
    public class Graph
    {
        /// <summary>
        /// Вершины графа - перестановки
        /// </summary>
        public Permutation[] graphModel = new Permutation[Factorial(Consts.SIZE * Consts.SIZE)];

        /// <summary>
        /// Создается перестановка, номер которой в массиве graphModel соответствует значению этого поля
        /// </summary>
        private int currentPer = 0; 
        private static int[] fact = { 0, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880 }; //Количество вершин
        public static int Factorial(int x) // Нужен чтобы узнать кол-во вершин
        {
            return fact[x];
        }
        public const int INF = Int32.MaxValue; //Бесконечность ( для дейкстры)


        /// <summary>
        /// Конструктор заполняет все вершины графа всевозможными перестановками в лексикографическом порядке
        /// </summary>
        public Graph() 
        {
            createPermutations(Consts.SIZE * Consts.SIZE);//
        }


        /// <summary>
        /// Создает перестановки - вершины графа в лексикографическом порядке
        /// </summary>
        /// <param name="length"></param>
        public void createPermutations(int length) 
        {
            int[] arrayOfPer = new int[length]; //Массив, лежащий в основе перестановки
            for (int i = 0; i < length; i++) //Первая перестановка в лексикографическом порядке
                arrayOfPer[i] = i; 

            // Создаем перестановки
            CreatePermutation(arrayOfPer);
            while (hasNext(arrayOfPer))
                CreatePermutation(arrayOfPer);
        }



        /// <summary>
        /// Создает перестановку в массиве graphModel, находящуюся в нем под номером currentPer, передавая в конструктор перестановки конкретный массив
        /// </summary>
        /// <param name="arrayOfPer"></param>
        public void CreatePermutation(int[] arrayOfPer)
        {
            graphModel[currentPer] = new Permutation(arrayOfPer);
            currentPer++;
        }



        /// <summary>
        /// Проверяет, существует ли следующая перестановка. Если да, то меняет получаемый массив на соответствующий ей. Функция найдена в интернете.
        /// </summary>
        /// <param name="currentArray"></param>
        /// <returns></returns>
        public bool hasNext(int[] currentArray) 
        {
            int length = currentArray.Count();

            // find rightmost element massForPermutationConstructor[k] that is smaller than element to its right
            int k;
            for (k = length - 2; k >= 0; k--)
                if (currentArray[k] < currentArray[k + 1]) break;
            if (k == -1) return false;
            // find rightmost element a[j] that is larger than a[k]
            int j = length - 1;
            while (currentArray[k] > currentArray[j])
                j--;
            swap(currentArray, j, k);
            for (int r = length - 1, s = k + 1; r > s; r--, s++)
                swap(currentArray, r, s);
            return true;
        }


        /// <summary>
        /// Вершина.
        /// </summary>
        private class Top
        {
            public int Length { get; set; }//Путь от начальной вершины
            public int Prev { get; set; }//Предыдущая вершина
            public bool Const { get; set; }//Постоянность - к проверенным вершинам не возвращаемся

            public Top()
            {
                Const = false;
                Length = INF;
                Prev = -1;
            }
        }
        


        /// <summary>
        /// Определяет размер массива перестановок
        /// </summary>
        /// <returns></returns>
        public int Size() 
        {
            return Factorial(Consts.SIZE * Consts.SIZE);
        }



        /// <summary>
        /// Меняет местами элементы массива с указанными номерами
        /// </summary>
        /// <param name="a"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public  void swap(int[] a, int i, int j)
        {
            int temp = a[i];
            a[i] = a[j];
            a[j] = temp;
        }
        


        /// <summary>
        /// По перестановке определяет ее лексикографический номер
        /// </summary>
        /// <param name="per"></param>
        /// <returns></returns>
        public int GetNumberOfPermutation(Permutation per)
        {
            return per.GetNum(0);
        }


        /// <summary>
        /// Дейкстра. Алгоритм реализован годом ранее, оптимизирован под данную программу.
        /// Возвращает путь. Каждое число это номер перестановки в graphModel
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public List<int> Dijkstra(int from) //поиск мин пути из вершины from в to возврат длины, заполнение массива пути
        {
           
            List<int> perForSearch = new List<int>();//Номера тех вершин, которые не постоянны и вес которых не бесконечность

            int to = 0; //Куды
            List<int> path = new List<int>(); //Путь

            if (from == to) //Если уже там где надо
            {
                return null;
            }

            //Инициализируем массив вершин. Их количество равно числу всех возможных перестановок.
            Top[] tops = new Top[Size()];
            for (int i = 0; i < Size(); i++) 
            {
                tops[i] = new Top();
            }

            tops[from].Length = 0;//Начальная вершина. Вес равен 0
            tops[from].Const = true;
            perForSearch.Add(0);

            int currenttop;//Текущая вершина
            currenttop = from;

            int amount = 0;//Счетчик циклов(путь содержит не более n вершин). После каждой итерации цикла путь увеличивается на 1 вершину
            //Значит итераций циклов не может быть больше n. Считаем их и проверяем. Если счетчик перевалил за n,
            //значит что-то не так, а именно искомого пути не существует(связи нет). Выходим из цикла и возвращаем код ошибки


            while ((currenttop != to) && (amount < Size()))//Пока не дошли до конца (и счетчик не перепрыгнул предел)
            {
                for (int j = 1; j < 5; j++)//Направления движения. Из каждой перестановки мы можем получить за один ход 2, 3, либо 4
                {
                    Permutation per = null;
                    switch (j)
                    {
                        case 1:
                            per = graphModel[currenttop].Left(); //Предусмотрено, что если такой перестановки нет, то опять получим null
                            break;
                        case 2:
                            per = graphModel[currenttop].Right();
                            break;
                        case 3:
                            per = graphModel[currenttop].Up();
                            break;
                        case 4:
                            per = graphModel[currenttop].Down();
                            break;

                    }

                    if (per != null) //Движение удалось. Такая перестановка есть.
                    {
                        int i = this.GetNumberOfPermutation(per); //По ней определяем ее номер

                        if (tops[i].Const == false)//Если i не постоянна
                        {
                            if (tops[i].Length > tops[currenttop].Length + 1)//Если новый способ дойти до i меньше делаем его
                            {
                                tops[i].Length = tops[currenttop].Length + 1;// Длина каждой связи равна 1
                                tops[i].Prev = currenttop; //Откуда в нее попали (чтобы потом знать путь)
                                perForSearch.Add(i); //Номера тех вершин, которые не постоянны и вес которых не бесконечность
                            }
                        }
                    }
                }

                int Min = INF;//Будем сразу искать минимальный путь
                int Mintop = -1;
                foreach (int i in perForSearch)
                {
                    if ((tops[i].Length < Min)) //Находим минимальный путь до вершины
                    {
                        if (tops[i].Const == false) //Если Вершина еще не постоянна, тоесть не просмотрена и ее вес не равен бесконечности
                        {
                            Min = tops[i].Length; //Присваиваем ей новый вес
                            Mintop = i; //Считаем ее как ближайшую
                        }
                    }
                }


                currenttop = Mintop;//Ближайшая вершина будет постоянной и новой текущей

                tops[currenttop].Const = true; //Мы ее рассмотрели, теперь она имеет минимальный к ней путь от начала, значит она постоянна
                perForSearch.Remove(currenttop); //Убираем ее из списка рассматриваемых вершин
                amount++; //Увеличиваем значение счетчика циклов
            }//end While
            
            //заполнение пути
            while (currenttop != from)//заполняем массив пути
            {
                path.Add(tops[currenttop].Prev);
                currenttop = tops[currenttop].Prev;
            }
            return path;
        }
    }
}