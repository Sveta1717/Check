using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Check
{
    
    public struct Check : IEnumerable
    {
        public Inform[] inf;
        public Check(Inform[] c)
        {
            inf = new Inform[c.Length];
            for (int i = 0; i < c.Length; i++)
            {
                inf[i] = new Inform(c[i]);
            }
        }
        public Check(Inform c)
        {
            inf = new Inform[1];
            inf[1] = c;

        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        private IEnumerator GetEnumerator()
        {
            return new InformEnum(inf);
        }

        public void AddProduct(ref Check ch, Inform p)
        {
            Array.Resize(ref ch.inf, ch.inf.Length + 1);
            ch.inf[ch.inf.Length - 1] = new Inform(p);
        }

        public void PrintCheck(Check check)
        {            
            Console.WriteLine("Чек N 78114" + "\nТОВ \"СIЛЬПО - ФУД\"");
            Console.WriteLine("_____________________");
            double sum = 0;
            foreach (Inform inf in check)
            {
                inf.Print();
                sum += inf.sum;
            }
            Console.WriteLine("Всього до сплати:" + sum);
        }
    }
    public struct Inform
    {
        public string name { get; set; }
        public double count { get; set; }
        public double price { get; set; }
        public double discount { get; set; }
        public double sum { get; set; }

        public Inform(string n, double c, double p, double d)
        {
            name = n;
            count = c;
            price = p;
            discount = d;

            sum = (price - price * (discount / 100)) * count;
        }
        public Inform(Inform inf)
        {
            name = inf.name;
            count = inf.count;
            price = inf.price;
            discount = inf.discount;
            sum = (price - price * (discount / 100)) * count;


        }
        public void Print()
        {
            Console.WriteLine("Найменування : " + name + "\nКiлькiсть : " + count +
                "\nЦiна : " + price + " грн\nЗнижка: " + discount + " %\nВартiсть: " + sum + " грн");
        }
    }
    class InformEnum : IEnumerator
    {
        public Inform[] _inf;
        int position = -1;
        public InformEnum(Inform[] list)
        {
            _inf = list;
        }

        public object Current
        {
            get
            {
                try
                {
                    return _inf[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
        public bool MoveNext()
        {
            position++;
            return (position < _inf.Length);
        }

        public void Reset()
        {
            position = -1;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Inform[] inf = new Inform[3];
            inf[0] = new Inform("Мило", 2, 29.99, 5);
            inf[1] = new Inform("Шампунь", 1, 109.99, 5);     
           
            Check c = new Check(inf);
            c.PrintCheck(c);
           
            Inform inf1 = new Inform("Дезодорант", 1, 85, 10);
            Console.WriteLine();
            Console.WriteLine();            
            c.AddProduct(ref c, inf1);
            c.PrintCheck(c);           
        }        
    }
}
