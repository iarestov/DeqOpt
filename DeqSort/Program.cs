using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace DeqSort
{

    internal class Deq<T>
    {
        private readonly Node<T> _head = new Node<T>();
        private int _size;

        /// <summary>
        /// Контруктор
        /// </summary>
        public Deq()
        {
            _head.Next = _head.Prev = _head;
        }

        /// <summary>
        /// Количество элементов в деке
        /// </summary>
        public int Count { get { return _size; } }

        /// <summary>
        /// Пуста ли дека
        /// </summary>
        public bool Empty { get { return Count == 0; } }

        /// <summary>
        /// Посмотреть на элемент в начале деки
        /// </summary>
        /// <returns>(T)Элемент</returns>
        public T Front
        {
            get
            {
                if (_head.Next == _head) throw new InvalidOperationException();
                return _head.Next.Value;
            }
        }

        /// <summary>
        /// Посмотреть на элемент в конце деки
        /// </summary>
        /// <returns>(T)Элемент</returns>
        public T Back
        {
            get
            {
                if (_head.Prev == _head) throw new InvalidOperationException();
                return _head.Prev.Value;
            }
        }

        /// <summary>
        /// Вставить элемент в конец деки
        /// </summary>
        /// <param name="item">(T)Элемент</param>
        public void PushBack(T item)
        {
            var node = new Node<T>(item) {Prev = _head.Prev, Next = _head};

            _head.Prev.Next = node;
            _head.Prev = node;

            _size++;
        }

        /// <summary>
        /// Вставить элемент в начало деки
        /// </summary>
        /// <returns>(T)Элемент</returns>
        public void PushFront(T item)
        {
            var node = new Node<T>(item) {Next = _head.Next, Prev = _head};

            _head.Next.Prev = node;
            _head.Next = node;

            _size++;
        }

        /// <summary>
        /// Вытянуть элемент с конца деки
        /// </summary>
        /// <returns>(T)Элемент</returns>
        public T PopBack()
        {
            var item = Back;
            _head.Prev.Prev.Next = _head;
            _head.Prev = _head.Prev.Prev;
            _size--;
            return item;
        }

        /// <summary>
        /// Вытянуть элемент с начала деки
        /// </summary>
        /// <returns>(T)Элемент</returns>
        public T PopFront()
        {
            var item = Front;
            _head.Next.Next.Prev = _head;
            _head.Next = _head.Next.Next;
            _size--;
            return item;
        }

        /// <summary>
        /// Перегрузка операции массива
        /// </summary>
        public T this[int key]
        {
            get { return GetDeqElementByPosition(key); }
            set { SetDeqElementByPosition(key, value); }
        }

        /// <summary>
        /// поиск элемента по индексу, перебираем с начала или к конца, в зависимости от того, что ближе
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private Node<T> FindNodeByPosition(int position)
        {
            if (position < 0 || position >= _size) throw new IndexOutOfRangeException();

            var mid = _size / 2;
            Node<T> n;
            if (position <= mid)
            {
                n = _head.Next;

                while (position-- > 0)
                {
                    n = n.Next;
                }
                return n;
            }

            n = _head.Prev;
            var val = _size - position;
            while (--val > 0)
            {
                n = n.Prev;
            }

            return n;
        }

        /// <summary>
        /// Получить элемент деки по номеру
        /// </summary>
        /// <returns>(T)Элемент</returns>
        public T GetDeqElementByPosition(int position)
        {
            return FindNodeByPosition(position).Value;
        }

        /// <summary>
        /// Вставить элемент с перезаписью в деку по номеру
        /// </summary>
        /// <returns>(T)Элемент</returns>
        public void SetDeqElementByPosition(int position, T value)
        {
            FindNodeByPosition(position).Value = value;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            var n = _head.Next;

            while (n != _head) {

                if (sb.Length > 0) {
                    sb.Append(", ");
                }

                sb.Append(n.Value);

                n = n.Next;
            }

            return sb.ToString();
        }

        internal class Node<TValue>
        {
            public Node()
            {
            }

            public Node(TValue value)
            {
                Value = value;
            }

            public TValue Value { get; set; }
            public Node<TValue> Next { get; set; }
            public Node<TValue> Prev { get; set; }
        }
    }

    internal static class Sorter
    {
        /// <summary>
        /// Сортировать деку
        /// </summary>
        /// <param name="unsortedArray">Не отсортированная дека Deq<T></param>
        /// <returns>Отсортированная дека Deq<T></returns>
        public static Deq<int> Sort(Deq<int> unsortedArray)
        {
            for (int i = 0; i < unsortedArray.Count; i++)
            {
                Console.WriteLine("Выполнено " + i + " из " + unsortedArray.Count + "("
                                  + (double)i / (
(double)unsortedArray.Count / 100) + "%)");

                var current = unsortedArray[i];
                var ins = BinarySearch(unsortedArray, 0, i, current);
                
                for (var j = i - 1; j >= ins; j--)
                    unsortedArray[j + 1] = unsortedArray[j];

                unsortedArray[ins] = current;
            }

            return unsortedArray;
        }

        public static Deq<int> Sort2(Deq<int> unsortedArray)
        {
            if (unsortedArray.Count > 1) {
                QSort(unsortedArray, 0, unsortedArray.Count-1);
            }
            return unsortedArray;
        }

        private static void QSort(Deq<int> array, int lo, int hi)
        {
            if (lo < hi) {
                var p = QSortPartition(array, lo, hi);
                QSort(array, lo, p);
                QSort(array, p + 1, hi);
            }
        }

        private static int QSortPartition(Deq<int> array, int lo, int hi)
        {
            var pivot = array[lo];
            var i = lo - 1;
            var j = hi + 1;
            for (;;) {
                do {
                    j--;
                } while (array[j] > pivot);
                do {
                    i++;
                } while (array[i] < pivot);
                if (i < j) {
                    var t = array[i];
                    array[i] = array[j];
                    array[j] = t;
                } else {
                    return j;
                }
            }

        }

        /// <summary>
        /// Бинарный поиск подходящей позиции элемента в массиве
        /// </summary>
        /// <param name="array">Массив элементов</param>
        /// <param name="low">Нижняя граница</param>
        /// <param name="high">Верхняя граница</param>
        /// <param name="key">Что ищем</param>
        /// <returns>Позиция для вставки значения в деку</returns>
        private static int BinarySearch(Deq<int> array, int low, int high, int key)
        {
            if (low == high)
                return low;

            var mid = low + ((high - low) / 2);

            var current = array[mid];
            if (key > current)
                return BinarySearch(array, mid + 1, high, key);
            else if (key < current)
                return BinarySearch(array, low, mid, key);

            return mid;
        }
    }


    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Программа сортировки бинарной вставкой");

            // Создаём, сортируем и выводим деку
            //Console.WriteLine(PrintArray(BinarySorting(RandomArray(10))) + "\n");

            // Создаём, сортируем и выводим деку
            //Console.WriteLine(PrintArray(BinarySorting(RandomArray(100))) + "\n");

            // Создаём, сортируем и выводим деку
            //Console.WriteLine(PrintArray(BinarySorting(RandomArray(1000))) + "\n");

            // разогрев
            // в управляемых средах типа Java/.NET надо вызвать все функции до теста чтобы они скомпилировались JIT компилятором
            Measure(String.Empty, () => Sorter.Sort2(RandomArray(100)), true);

            var deq = RandomArray(1000);
            
            // тест
            Measure("BinarySort", () => Sorter.Sort2(deq));

            // выводим деку
            Console.WriteLine(deq);

            // Держим окно открытым
            Console.ReadLine();
        }

        /// <summary>
        /// Создаём деку на рандом
        /// </summary>
        /// <returns>Deq<int></returns>
        private static Deq<int> RandomArray(int size)
        {
            var rnd = new Random();
            var deq = new Deq<int>();
            for (var i = 0; i < size; i++)
                deq.PushBack(rnd.Next(0, 100));

            return deq;
        }



        private static void Measure(string name, Action action, bool silent = false)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            action();
            stopWatch.Stop();
            if (!silent)
                Console.WriteLine("{0} заняло {1} милисекунд", name, stopWatch.Elapsed);
        }
    }
}
