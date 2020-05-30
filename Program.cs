using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace lab14
{
    class Program
    {
        static Engine eng1 = new Engine();
        static InternalCEngine iEng1 = new InternalCEngine();
        static Diesel dies1 = new Diesel();
        static TurbojetEngine turb1 = new TurbojetEngine();
        static int InputMenu(int maxNum)
        {
            int menu;
            string input;
            bool ok;
            do
            {
                input = Console.ReadLine();
                ok = int.TryParse(input, out menu);
                if (!ok) Console.WriteLine("Некорректный ввод");
                else if (menu < 0 || menu > maxNum) Console.WriteLine($"Некорректный ввод.Выберите вариант от 0 до {maxNum} из меню");
            } while (!ok || menu < 0 || menu > maxNum);

            return menu;
        }

        static int InputNum(int maxNum)
        {
            int num;
            string input;
            bool ok;
            do
            {
                input = Console.ReadLine();
                ok = int.TryParse(input, out num);
                if (!ok) Console.WriteLine("Некорректный ввод");
                else if (num < 0 || num > maxNum) Console.WriteLine($"Некорректный ввод. Введите число не больше {maxNum}");
            } while (!ok || num < 0 || num > maxNum);

            return num;

        }

        static void Main(string[] args)
        {

            Collections col = new Collections(10);

            col.Show();
            Console.WriteLine("");

            Sample(4000, col);      
            Count(4000, col);      
            Variety("CC", "TT", col); 
            Grouping(6000, col);   
            Aggregation(col);   
        }

        public static void Sample(int power, Collections col)
        {
            Console.WriteLine($"Выборка всех двигателей в мастерской, мощностью > {power}");

            var linq = from eng in col.stackEng where eng.Power > power select eng;
            foreach (Engine eng in linq)
            {
                eng.Show();
            }

            var methods = col.stackEng.Where(eng => eng.Power > power).Select(eng => eng);
            Console.WriteLine();
            foreach (Engine eng in methods)
            {
                eng.Show();
            }

            if (linq.Count() == methods.Count())
                Console.WriteLine("Результаты совпадают");
            Console.WriteLine();
        }

        public static void Count(int power, Collections col)
        {
            var linq = (from eng in col.listEng where eng.Power<power select eng).Count();

            var methods = (col.listEng.Where(eng => eng.Power < power).Select(eng => eng)).Count();
           

            Console.WriteLine($"Кол-во двигателей в машине, мощностью меньше {power}: {linq};{methods}");
            if (linq == methods)
                Console.WriteLine("Результаты совпадают");
            Console.WriteLine();
        }

        public static void Variety(string min, string max, Collections col)
        {
            Console.WriteLine($"Двигатели в машине, с именем от \"{min}\" и до \"{max}\" ");

            var linq = (from eng in col.listEng where eng.Name.CompareTo(min)==1 select eng)
                .Except(from eng in col.listEng where eng.Name.CompareTo(max) == 1 select eng);

            var methods =
                (col.listEng.Where(eng => eng.Name.CompareTo(min) == 1).Select(eng => eng)).Except
                    (col.listEng.Where(eng => eng.Name.CompareTo(max) == 1).Select(eng => eng));

            foreach (Engine eng in linq)
            {
                eng.Show();
            }
            Console.WriteLine();
            foreach (Engine eng in methods)
            {
                eng.Show();
            }

            if (linq.Count() == methods.Count())
                Console.WriteLine("Результаты совпадают");
            Console.WriteLine();
        }

        public static void Aggregation(Collections col)
        {
            Engine SubAggregate(Engine max, Engine eng)
            {
                if (max.Power < eng.Power) return eng;
                else return max;
            }


            var linq = (from eng in col.stackEng select eng.Power).Max();

            var methods = (col.stackEng.Aggregate(SubAggregate)).Power;

            Console.WriteLine($"Максимумальная мощность двигателя в мастерской: {linq}; {methods}");

            if (linq == methods)
                Console.WriteLine("Результаты LINQ запроса совпадают с результатами запроса с использованием методов расширения");

            Console.WriteLine();
        }

        public static void Grouping(int num, Collections col)
        {
            Console.WriteLine($"Группировкка:Мощность двигателя в мастерской > {num}");

            var linq = from eng in col.stackEng
                       group eng by eng.Power > num;

            var methods = col.stackEng.GroupBy(eng => eng.Power > num);

            foreach (IGrouping<bool, Engine> g in linq)
            {
                Console.WriteLine(g.Key ? $"Мощность > {num}" : $"Мощность <= {num}");
                foreach (var t in g)
                    t.Show();
                Console.WriteLine();
            }
            if (linq.Count() == methods.Count())
                Console.WriteLine("Результаты совпадают");
            Console.WriteLine();
        }
    

    }

    class Collections
    {
        public Stack<Engine> stackEng = new Stack<Engine>();
        public List<Engine> listEng = new List<Engine>();


        public int Size { get; set; }

        public Collections(int size)
        {
            Random rand = new Random();
            int menu;
            Size = size;
            InternalCEngine iEng = new InternalCEngine();
            Diesel dies = new Diesel();
            Engine eng = new Engine();
            TurbojetEngine turb = new TurbojetEngine();
            for (int i = 0; i < size; i++)
            {
                menu = rand.Next(4);

                switch (menu)
                {
                    case 0:
                        stackEng.Push(eng.MakeRandom());
                        listEng.Add(eng.MakeRandom());
                        break;
                    case 1:
                        stackEng.Push(dies.MakeRandom());
                        listEng.Add(dies.MakeRandom());
                        break;
                    case 2:
                        stackEng.Push(iEng.MakeRandom());
                        listEng.Add(iEng.MakeRandom());
                        break;
                    case 3:
                        stackEng.Push(turb.MakeRandom());
                        listEng.Add(turb.MakeRandom());
                        break;
                }
            

            }
        }

        public void Show()
        {
            Console.WriteLine("stack-мастерская");
            foreach (Engine elem in stackEng)
            {
                Console.WriteLine(elem.ToString());
            }
            Console.WriteLine("list-машины");
            foreach (Engine elem in listEng)
            {
                Console.WriteLine(elem.ToString());
            }
        }

        static int InputNum(int maxNum)
        {
            int num;
            string input;
            bool ok;
            do
            {
                input = Console.ReadLine();
                ok = int.TryParse(input, out num);
                if (!ok) Console.WriteLine("Некорректный ввод");
                else if (num < 0 || num > maxNum) Console.WriteLine($"Некорректный ввод. Введите число не больше {maxNum}");
            } while (!ok || num < 0 || num > maxNum);

            return num;

        }
        public void Add()
        {
            int menu;
            Random rand = new Random();
            InternalCEngine iEng = new InternalCEngine();
            Diesel dies = new Diesel();
            Engine eng = new Engine();
            TurbojetEngine turb = new TurbojetEngine();
            menu = rand.Next(4);

            switch (menu)
            {
                case 0:
                    stackEng.Push(eng.MakeRandom());
                    listEng.Add(eng.MakeRandom());
                    break;
                case 1:
                    stackEng.Push(dies.MakeRandom());
                    listEng.Add(dies.MakeRandom());
                    break;
                case 2:
                    stackEng.Push(iEng.MakeRandom());
                    listEng.Add(iEng.MakeRandom());
                    break;
                case 3:
                    stackEng.Push(turb.MakeRandom());
                    listEng.Add(turb.MakeRandom());
                    break;
            }
            Size++;
        }
        public void Delete()
        {
            stackEng.Pop();
            listEng.RemoveAt(0);


            Console.WriteLine("Элементы удален");
            Size--;
        }

    }

}
