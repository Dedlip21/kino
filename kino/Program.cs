using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kordamine
{
    class Program
    {
        static int Saali_suurus() // Спрашивает вариант размера
        {
            Console.WriteLine("Vali saali suurus: 1,2,3"); //На экране пишутся 3 варианта
            int suurus = int.Parse(Console.ReadLine()); //Переменная suurus
            return suurus; //Возвращает переменную suurus
        }
        static int[,] saal = new int[,] { };//двумерный массив saal(зал)
        static int[] ost = new int[] { }; //двумерный массив ost(покупка)
        static int kohad, read, mitu, mitu_veel; //места, ряды, кол-во билетов и
        static void Saali_taitmine(int suurus) //Функция которая задает размер зала и запоминает ваш вариант из 3 доступных но не выводит его на экран
        {
            Random rnd = new Random(); //Переменная rnd для 
            if (suurus == 1) //Если ты выбрал размер 1
            { kohad = 20; read = 10; } //то размер будет 20 мест на каждый ряд из 10 рядов (200 мест)
            else if (suurus == 2)//Если ты выбрал размер 2
            { kohad = 20; read = 20; }//то размер будет 20 мест на каждый ряд из 20 рядов (400 мест)
            else//Если ты выбрал размер 3
            { kohad = 30; read = 20; }//то размер будет 30 мест на каждый ряд из 20 рядов (600 мест)
            saal = new int[read, kohad];//массив saal в котором содержится совокупность рядов и мест
            for (int rida = 0; rida < read; rida++)//Ряды будут создаваться пока не достигнут указанного максимума
            {
                for (int koht = 0; koht < kohad; koht++)//Места будут создаваться пока не достигнут указанного максимума
                {
                    saal[rida, koht] = rnd.Next(0, 2);//Все свободные и занятые места задаются рандомно(1 и 0 в рандомных местах)
                }
            }
        }
        static void Saal_ekraanile() //Вся эта функция выводит на экран зал(ряды, места и т.д)
        {
            Console.Write("     ");
            for (int koht = 0; koht < kohad; koht++)//Создаются нужное кол-во мест
            {
                if (koht.ToString().Length == 2)//Если длина значения koht равняется 2
                { Console.Write(" {0}", koht + 1); } // то расстояние между числами 
                else
                { Console.Write("  {0}", koht + 1); }
            }

            Console.WriteLine();
            for (int rida = 0; rida < read; rida++)
            {
                Console.Write("Rida " + (rida + 1).ToString() + ":");
                for (int koht = 0; koht < kohad; koht++)
                {

                    Console.Write(saal[rida, koht] + "  ");
                }
                Console.WriteLine();
            }
        }

        static void Muuk_ise()
        {
            Console.WriteLine("Rida: "); //Спрашивает
            int pileti_rida = int.Parse(Console.ReadLine());
            Console.WriteLine("Koht: ");
            int pileti_koht = int.Parse(Console.ReadLine());
            if (saal[pileti_rida, pileti_koht]==0) //Если место которое ты выбрал имеет значение 0 
            {
                saal[pileti_rida, pileti_koht] = 1;// то заменяет 0 на 1 
            }
        }
        static void Muuk()//Покупка билетов
        {
            Console.WriteLine("Rida:");
            int pileti_rida = int.Parse(Console.ReadLine());
            Console.WriteLine("Mitu piletid:");
            mitu = int.Parse(Console.ReadLine()); //Кол-во билетов
            ost = new int[mitu]; 
            int p = (kohad - mitu) / 2; //Переменная p = места минус кол-во билетов и поделенная на 2
            bool t = false;
            int k = 0; //k - вспомогательная переменная 
            do
            {
                if (saal[pileti_rida, p] == 0)//Если в зале на месте который ты выбрал есть 0, то 
                {
                    ost[k] = p;
                    Console.WriteLine("koht {0} on vaba", p);
                    t = true;
                }
                else
                {
                    Console.WriteLine("koht {0} kinni", p);
                    t = false;
                    ost = new int[mitu];
                    k = 0;
                    p = (kohad - mitu) / 2;
                    break;
                }
                p = p + 1;
                k++;
            } while (mitu != k);
            if (t == true) //Если t истина, то тебе говорят, где ты занял место
            {
                Console.WriteLine("Sinu kohad on:");
                foreach (var koh in ost)
                {
                    Console.WriteLine("{0}\n", koh);
                }
            }
            else // В противном случае тебе скажут, что мест нету и спросят занять другое место
            {
                Console.WriteLine("Selles reas ei ole vabu kohti. Kas tahad teises reas otsida?");
            }
        }
        public static void Main(string[] args) //Начало программы
        {
            int suurus = Saali_suurus(); //Функция Saali_suurus() которая спрашивает тебя, какой размер зала ты хочешь
            Saali_taitmine(suurus); //Делает нужный размер зала смотря какой вариант размера ты выбрал
            while (true) //После каждой совершонного занятого места, выбор вырианта покупки будет повторяться
            {
                Saal_ekraanile(); //Будет выводиться весь зал (активируется функция Saal_ekraanile())
                Console.WriteLine("1-ise valik, 2-masina valik"); //спрашивает вариант покупки билетов
                int valik = int.Parse(Console.ReadLine()); //Переменная valik для введения варианта
                if (valik==1)
                {
                    int koh = 0;
                    Console.WriteLine("Mitu pileteid tahad osta?");
                    int kogus = int.Parse(Console.ReadLine());
                    bool muuk = true;
                    while (muuk != false)
                    {
                        for (int i = 0; i < (kohad-1)*(read-1); i++)
                        {
                            muuk = Muuk_ise();
                            if (muuk)
                            {
                                koh++;
                            }
                        }
                        if (koh == kogus)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    bool muuk = false;
                    while (muuk != true)
                    {
                        muuk = Muuk();
                    }
                    
                }
            }
            //Console.ReadLine();
        }
    }
}