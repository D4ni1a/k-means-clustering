using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
          
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
class Algorithm
{
    //Создаем класс, в котором будет храниться таблица значений (столбец - измерение, строка - пример)
    private int[,] mass;
    public int n;
    public int m;
    public Algorithm(int first, int second)
    {
        n = first;
        m = second;
        mass = new int[first, second];
    }
    public int this [int index1, int index2]
    {
        get
        {
            return mass[index1, index2];
        }
        set
        {
            mass[index1, index2] = value;
        }
    }

    //Переменные хранящие значения центров кластеров, центров для лучшего приближения
    public float[,] centroids;
    public float[,] best_centroids;
    public float cont;
    float min_count;
    public Algorithm K_Mean(int k)
    {
        centroids = new float[k, m];
        best_centroids = new float[k, m];
        int[] res = new int[n];
        min_count = -1;
        Algorithm C = new Algorithm(n, m + 1);
        Random rnd = new Random();
        //Inizialize
        //Сначала создается экземпляр класса, который хранит в себе ту же таблицу, плюс еще один столбец, 
        //в котором будет указан номер соответствующего примеру кластера
        for (int i = 0; i < n; i++)
        {

            for (int j = 0; j < m; j++)
            {
                C[i, j] = mass[i, j];
            }
        }
        //Начальные значения кластеров задаются из самих примеров рандомно
        //Цикл нужен для того, чтобы перебрать разные комбинации и выбрать ту, которая имеет лучшее приближение
        for (int z=0; z<n*n*n*k*k*k; z++)
            //Random Inizialization of Centroids
        { 
        int t_last = -1;
        int t;
            //Вот цикл, задающий рандомно значения из имеющихся
        for (int i = 0; i < k; i++)
        {
            do
            {
                t = rnd.Next(0, n);
            }
            while (t == t_last);
            for (int j = 0; j < m; j++)
            {
                centroids[i, j] = mass[t, j];
            }
            t_last = t;
        }
        //Find the best cluster for each example
        //Цикл, запускающий сам алгоритм к средних, состоит из поиска лучшего кластера для каждого примера и обновления центра с учетом имеющихся данных
        for (int s = 0; s < 20; s++)
        {
                //Поиск
            for (int i = 0; i < n; i++)
            {
                float min = -1;
                int minK = 0;
                for (int j = 0; j < k; j++)
                {
                    float count = 0;
                    for (int l = 0; l < m; l++)
                    {
                            //Разница между центром кластера и значением должна быть минимальна, тогда данный пример будет присвоен этому кластеру
                        count += ((mass[i, l] - centroids[j, l]) * (mass[i, l] - centroids[j, l]));
                    }

                    if ((count <= min) || (min < 0))
                    {
                        minK = j;
                        min = count;
                    }
                }
                res[i] = minK;

            }
            //How musch examples for each cluster
            //Подсчет количества примеров, относящихся к каждому кластеру
            int[] counts = new int[k];
            for (int i = 0; i < n; i++)
            {
                counts[res[i]] += 1;
            }

            //Renew centroids
            //Два цикла обновляют значения центров
            for (int i = 0; i < k; i++)
            {
                    if (counts[i] != 0)
                    {
                        for (int j = 0; j < m; j++)
                        {
                            centroids[i, j] = 0;
                        }
                    }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {

                    centroids[res[i], j] += mass[i, j];
                }

            }
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (counts[i] != 0)
                    {
                        centroids[i, j] /= counts[i];

                    }
                }
            }


                //По окончанию подгонки центров вычисляется близость каждой точки к своему центроиду, эта величина должна быть меньше,
                //чем для других итераций рандомных
                //значений, тогда данные значения будут выбраны как лучшие
                //Best centroids
                cont = 0;
                for (int i = 0; i < n; i++)
                {

                    
                        for (int l = 0; l < m; l++)
                        {
                            cont += ((mass[i, l] - centroids[res[i], l]) * (mass[i, l] - centroids[res[i], l]));
                        }

                    
                }
                if ((cont<min_count)||(min_count<0))
                {
                    min_count = cont;
                    best_centroids = centroids;
                }
                    }

    }
        //Find cluster for best centroid variant
        //Производится присвоение примера центроиду при помощи набора из лучших значений центров
        for (int i = 0; i < n; i++)
        {
            float min_2 = -1;
            int minK_2 = 0;
            for (int j = 0; j < k; j++)
            {
                float key = 0;
                for (int l = 0; l < m; l++)
                {
                    key += ((mass[i, l] - best_centroids[j, l]) * (mass[i, l] - best_centroids[j, l]));
                }

                if ((key <= min_2) || (min_2 < 0))
                {
                    minK_2 = j;
                    min_2 = key;
                }
            }
            res[i] = minK_2;
        
        }
        //Колонка с значениями кластеров дописывается как колонка справа
        //Add column of centroids
        for (int i=0; i<n; i++)
        {
            C[i, m] = res[i];
        }
        return C;



        
    }
}