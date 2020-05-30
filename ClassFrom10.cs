using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace lab14
{
    public interface IExecutable
    {
        void Show();
        int ToWatt();

    }
    public class SortByPower : IComparer
    {
        int IComparer.Compare(object obj1, object obj2)
        {
            Engine eng1 = (Engine)obj1;
            Engine eng2 = (Engine)obj2;
            if (eng1.Power < eng2.Power) return -1;
            else if (eng1.Power == eng2.Power) return 0;
            else return 1;
        }
    }
    public class Engine : IExecutable, IComparable, ICloneable
    {
        protected int power;
        protected string name;
        public int[] mas = new int[] { 1, 1, 1, 1, 1 };
        Random rnd = new Random();
        public int Power
        {
            get { return power; }
            set
            {
                if (value < 0) power = 0;
                else power = value;
            }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public Engine()
        {
            power = 0;
            name = "-";
        }
        public Engine(string n, int f)
        {
            Name = n;
            Power = f;
        }
        public override bool Equals(object e)
        {
            return Name == ((Engine)e).Name && Power == ((Engine)e).Power;
        }
        public virtual void Show()
        {
            Console.WriteLine($"Двигатель {name}; сила тяги - {power} лошадиных сил");
        }
        public override string ToString()
        {
            return $"Двигатель {name}; сила тяги - {power} лошадиных сил";
        }
        public void ShowNoVirt()
        {
            Console.WriteLine($"Двигатель {name}; сила тяги - {power} лошадиных сил");
        }
        public int ToWatt()
        {
            return (int)Math.Round(power * 735.5);
        }
        public int CompareTo(object obj)
        {
            Engine eng = (Engine)obj;
            return string.Compare(name, eng.name);
        }
        public Engine ShallowCopy()
        {
            return (Engine)MemberwiseClone();
        }
        public object Clone()
        {
            return new Engine(name, power);
        }
        public Engine MakeRandom()
        {
            string[] names = new string[] {"SS","RR","TT","DD","FKIL","UYTG","MMNN","BHNG","LL-NN","VV-J","PP-I",
            "CV","ND","KT","SL","XP","QL","SP","OU","WM","PP-N","CJ_NN","KKS","RT-3R","MK-4V","LLK-5","BB-6H"};
            return new Engine(names[rnd.Next(27)], (rnd.Next(99) + 1) * 100);
        }
    }
    public class InternalCEngine : Engine
    {
        Random rnd = new Random();
        public override void Show()
        {
            Console.WriteLine($"Двигатель внутреннего сгорания {name}; сила тяги - {power} лошадиных сил");
        }
        public override string ToString()
        {
            return $"Двигатель внутреннего сгорания {name}; сила тяги - {power} лошадиных сил";
        }
        public new void ShowNoVirt()
        {
            Console.WriteLine($"Двигатель внутреннего сгорания {name}; сила тяги - {power} лошадиных сил");
        }
        public InternalCEngine() : base() { }
        public InternalCEngine(string n, int f) : base(n, f) { }
        public override bool Equals(object e)
        {
            return Name == ((InternalCEngine)e).Name && Power == ((InternalCEngine)e).Power;
        }
        public object Clone()
        {
            return new InternalCEngine(name, power);
        }
        public InternalCEngine MakeRandom()
        {
            string[] names = new string[] {"SS","RR","TT","DD","FKIL","UYTG","MMNN","BHNG","LL-NN","VV-J","PP-I",
            "CV","ND","KT","SL","XP","QL","SP","OU","WM","PP-N","CJ_NN","KKS","RT-3R","MK-4V","LLK-5","BB-6H"};
            return new InternalCEngine(names[rnd.Next(27)], (rnd.Next(99) + 1) * 100);
        }
        public Engine BaseEngine()
        {
            return new Engine(name, power);
        }
    }
    public class Diesel : InternalCEngine
    {
        int numOfStroke;
        Random rnd = new Random();
        public int NumOfStroke
        {
            get { return numOfStroke; }
            set
            {
                if (value == 4) numOfStroke = 4;
                else numOfStroke = 2;
            }
        }
        public override void Show()
        {
            Console.WriteLine($"Дизельный {numOfStroke}-тактный двигатель {name}; сила тяги - {power} лошадиных сил");
        }
        public override string ToString()
        {
            return $"Дизельный {numOfStroke}-тактный двигатель {name}; сила тяги - {power} лошадиных сил";
        }
        public new void ShowNoVirt()
        {
            Console.WriteLine($"Дизельный {numOfStroke}-тактный двигатель {name}; сила тяги - {power} лошадиных сил");
        }
        public Diesel() : base() { NumOfStroke = 2; }
        public Diesel(string n, int f, int stroke) : base(n, f) { NumOfStroke = stroke; }
        public override bool Equals(object e)
        {
            return Name == ((Diesel)e).Name && Power == ((Diesel)e).Power && NumOfStroke == ((Diesel)e).NumOfStroke;
        }
        public object Clone()
        {
            return new Diesel(name, power, numOfStroke);
        }
        public Diesel MakeRandom()
        {
            string[] names = new string[] {"SS","RR","TT","DD","FKIL","UYTG","MMNN","BHNG","LL-NN","VV-J","PP-I",
            "CV","ND","KT","SL","XP","QL","SP","OU","WM","PP-N","CJ_NN","KKS","RT-3R","MK-4V","LLK-5","BB-6H"};
            return new Diesel(names[rnd.Next(27)], (rnd.Next(99) + 1) * 100, rnd.Next(2) * 2 + 2);
        }
        public Engine BaseEngine()
        {
            return new Engine(name, power);
        }
    }
    public class TurbojetEngine : InternalCEngine
    {
        bool afterburner;
        Random rnd = new Random();
        public bool Afterburner
        {
            get { return afterburner; }
            set { afterburner = value; }
        }
        public override void Show()
        {
            string extra = "";
            if (afterburner) extra = "; с форсажной камерой";
            Console.WriteLine($"Турбореактивный двигатель {name}; сила тяги - {power} лошадиных сил" + extra);
        }
        public override string ToString()
        {
            string extra = "";
            if (afterburner) extra = "; с форсажной камерой";
            return $"Турбореактивный двигатель {name}; сила тяги - {power} лошадиных сил" + extra;
        }
        public new void ShowNoVirt()
        {
            string extra = "";
            if (afterburner) extra = "; с форсажной камерой";
            Console.WriteLine($"Турбореактивный двигатель {name}; сила тяги - {power} лошадиных сил" + extra);
        }
        public TurbojetEngine() : base() { afterburner = false; }
        public TurbojetEngine(string n, int f, bool a) : base(n, f) { afterburner = a; }

        public override bool Equals(object e)
        {
            return Name == ((TurbojetEngine)e).Name && Power == ((TurbojetEngine)e).Power && Afterburner == ((TurbojetEngine)e).Afterburner;
        }
        public object Clone()
        {
            return new TurbojetEngine(name, power, afterburner);
        }
        public TurbojetEngine MakeRandom()
        {
            string[] names = new string[] {"SS","RR","TT","DD","FKIL","UYTG","MMNN","BHNG","LL-NN","VV-J","PP-I",
            "CV","ND","KT","SL","XP","QL","SP","OU","WM","PP-N","CJ_NN","KKS","RT-3R","MK-4V","LLK-5","BB-6H"};
            return new TurbojetEngine(names[rnd.Next(27)], (rnd.Next(99) + 1) * 100, rnd.Next(2) == 1);
        }
        public Engine BaseEngine()
        {
            return new Engine(name, power);
        }
    }
}
