using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FaceForNine
{
    public class Permutation
    {
        public int[,] gameField = new int[Consts.SIZE, Consts.SIZE];

        /// <summary>
        /// Конструктор: Принимает квадратную матрицу либо просто массив
        /// </summary>
        /// <param name="field"></param>
        public Permutation(int[,] field)
        {
            gameField = (int[,])field.Clone();
        }
        public Permutation(int [] field)
        {
            for (int i=0; i<Consts.SIZE; i++)
            {
                for (int j=0; j<Consts.SIZE; j++)
                {
                    gameField[i,j] = field[i*Consts.SIZE + j];
                }
            }
        }



        /// <summary>
        /// Возвращает линейный массив полученный из квадратной матрицы
        /// </summary>
        /// <returns></returns>
        public int[] AllNumbers()
        {
            int[] arr = new int[Consts.SIZE * Consts.SIZE];
            int k=0;
            for (int i=0; i<Consts.SIZE; i++)
            {
                for (int j=0; j<Consts.SIZE; j++)
                {
                    arr[k] = gameField[i,j];
                    k++;
                }
            }
            return arr;
        }



        /// <summary>
        /// По перестановке определяет ее номер
        /// Число i - номер элемента массива, с которого начинаем считать номер перестановки
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int GetNum(int i)
        {
            if (i == Consts.SIZE * Consts.SIZE)
                return 0;
            else
            {
                int countLess = AllNumbers()[i];
                for (int j = 0; j < i; j++)
                    if (AllNumbers()[j] < AllNumbers()[i])
                        countLess--;
                return (countLess) * Graph.Factorial(Consts.SIZE * Consts.SIZE - i - 1) + GetNum(i + 1);
            }
        }


        /// <summary>
        /// Проверка перестановок на равенство
        /// </summary>
        /// <param name="per1"></param>
        /// <param name="per2"></param>
        /// <returns></returns>
        public static bool PerEquals(Permutation per1, Permutation per2)
        {
            bool complete = true;
            for (int currentRow = 0; currentRow < Consts.SIZE; currentRow++)
            {
                for (int currentColumn = 0; currentColumn < Consts.SIZE; currentColumn++)
                {
                    if (per1.gameField[currentRow, currentColumn] != per2.gameField[currentRow, currentColumn])
                        complete = false;
                }
            }
            return complete;
        }

/*-------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Поднимает пустую ячейку вверх (Если возможно)
        /// </summary>
        /// <returns></returns>
        public Permutation Up()
        {
            Permutation per1 = new Permutation(this.gameField);

            int row = 0; 
            int column = 0;

            //Нашли столбец и строку в которых 0
            for (int currentRow = 0; currentRow < Consts.SIZE; currentRow++)
            {
                for (int currentColumn = 0; currentColumn < Consts.SIZE; currentColumn++)
                {
                    if (per1.gameField[currentRow, currentColumn] ==0)
                    {
                        row = currentRow;
                        column = currentColumn;
                    }
                }
            }

            if (row > 0)
            {
                //выполняем перестановку
                int extra = per1.gameField[row, column];
                per1.gameField[row, column] = per1.gameField[row - 1, column];
                per1.gameField[row - 1, column] = extra;

                return per1;
            }
            return null;
        }

        /// <summary>
        /// Опускает пустую ячейку вниз (Если возможно)
        /// </summary>
        /// <returns></returns>
        public Permutation Down()
        {
            Permutation per1 = new Permutation(this.gameField);

            int row = 0;
            int column = 0;

            //Нашли столбец и строку в которых 0
            for (int currentRow = 0; currentRow < Consts.SIZE; currentRow++)
            {
                for (int currentColumn = 0; currentColumn < Consts.SIZE; currentColumn++)
                {
                    if (per1.gameField[currentRow, currentColumn] == 0)
                    {
                        row = currentRow;
                        column = currentColumn;
                    }
                }
            }

            //Пытаемся чделать движение вниз
            if (row < Consts.SIZE - 1)
            {
                //выполняем перестановку
                int extra = per1.gameField[row, column];
                per1.gameField[row, column] = per1.gameField[row + 1, column];
                per1.gameField[row + 1, column] = extra;

                return per1;
            }
            return null;
        }


        /// <summary>
        /// Двигает пустую ячейку влево
        /// </summary>
        /// <returns></returns>
        public Permutation Left()
        {
            Permutation per1 = new Permutation(this.gameField);

            int row = 0;
            int column = 0;

            //Нашли столбец и строку в которых 0
            for (int currentRow = 0; currentRow < Consts.SIZE; currentRow++)
            {
                for (int currentColumn = 0; currentColumn < Consts.SIZE; currentColumn++)
                {
                    if (per1.gameField[currentRow, currentColumn] == 0)
                    {
                        row = currentRow;
                        column = currentColumn;
                    }
                }
            }

            
            if (column > 0)
            {
                //выполняем перестановку
                int extra = per1.gameField[row, column];
                per1.gameField[row, column] = per1.gameField[row, column - 1];
                per1.gameField[row, column - 1] = extra;


                return per1;
            }
            return null;
        }



        /// <summary>
        ///  Двигает пустую ячейку вправо
        /// </summary>
        /// <returns></returns>
        public Permutation Right()
        {
            Permutation per1 = new Permutation(this.gameField);

            int row = 0;
            int column = 0;

            //Нашли столбец и строку в которых 0
            for (int currentRow = 0; currentRow < Consts.SIZE; currentRow++)
            {
                for (int currentColumn = 0; currentColumn < Consts.SIZE; currentColumn++)
                {
                    if (per1.gameField[currentRow, currentColumn] == 0)
                    {
                        row = currentRow;
                        column = currentColumn;
                    }
                }
            }

   
            if (column < Consts.SIZE - 1)
            {
                //выполняем перестановку
                int extra = per1.gameField[row, column];
                per1.gameField[row, column] = per1.gameField[row, column + 1];
                per1.gameField[row, column + 1] = extra;


                return per1;
            }
            return null;
        }
    }
}
