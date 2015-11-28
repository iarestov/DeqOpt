﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace DeqSort
{

    internal class Deq<T>
    {
        private Node<T> _front;
        private Node<T> _current;
        private Node<T> _back;
        private int _size = 0;

        /// <summary>
        /// Контруктор
        /// </summary>
        public Deq()
        {
            _size = 0;
            _front = _current = _back = null;
        }

        /// <summary>
        /// Количество элементов в деке
        /// </summary>
        public int Count { get { return _size; } }

        /// <summary>
        /// Пуста ли дека
        /// </summary>
        public bool Empty { get { return _size == 0; } }

        /// <summary>
        /// Посмотреть на элемент в начале деки
        /// </summary>
        /// <returns>(T)Элемент</returns>
        public T Front
        {
            get
            {
                if (_front == null) throw new InvalidOperationException();
                return _front.Value;
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
                if (_back == null) throw new InvalidOperationException();
                return _back.Value;
            }
        }

        /// <summary>
        /// Вставить элемент в конец деки
        /// </summary>
        /// <param name="item">(T)Элемент</param>
        public void PushBack(T item)
        {
            var node = new Node<T>(item);
            if (_back == null && _front == null)
                _back = _front = node;
            else {
                node.Prev = _back;
                _back.Next = node;
                _back = node;
            }
            _size++;
        }

        /// <summary>
        /// Вытянуть элемент с начала деки
        /// </summary>
        /// <returns>(T)Элемент</returns>
        public void PushFront(T item)
        {
            var node = new Node<T>(item);
            if (_back == null && _front == null)
                _back = _front = node;
            else {
                node.Next = _front;
                _front.Prev = node;
                _front = node;
            }
            _size++;
        }

        /// <summary>
        /// Вытянуть элемент с конца деки
        /// </summary>
        /// <returns>(T)Элемент</returns>
        public T PopBack()
        {
            var item = Back;
            if (_front != _back)
                _back.Prev.Next = null;
            _back = _back.Prev;
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
            if (_front != _back)
                _front.Next.Prev = null;
            _front = _front.Next;
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
        /// Получить элемент деки по номеру
        /// </summary>
        /// <returns>(T)Элемент</returns>
        public T GetDeqElementByPosition(int position) 
        {
            if (position < 0 || position >= _size) throw new IndexOutOfRangeException();
            if(Count == 1)
                return PopBack();

            for (var i = 0; i <= position; i++)
                PushBack(PopFront());

            var result = Front;

            for (var i = 0; i <= position; i++)
                PushFront(PopBack());

            return result;
        }

        /// <summary>
        /// Вставить элемент с перезаписью в деку по номеру
        /// </summary>
        /// <returns>(T)Элемент</returns>
        public void SetDeqElementByPosition(int position, T value)
        {
            if (position < 0 || position > _size) throw new IndexOutOfRangeException();
            if (Empty) {
                PushBack(value);
                return;
            }
            if (position == Count) {
                PushFront(value);
                return;
            }

            for (var i = 0; i <= position; i++)
                PushBack(PopFront());

            PopFront();
            PushFront(value);

            for (var i = 0; i <= position; i++)
                PushFront(PopBack());
        }


        public override string ToString()
        {
            string result = "";
            var count = Count;

            for (var i = 0; i < count; i++)
            {
                PushBack(PopFront());
                result += Front + ",";
            }

            for (var i = 0; i < count; i++)
                PushFront(PopBack());

            return result.Substring(0, result.Length - 1);
        }

        internal class Node<TValue>
        {
            public Node(TValue value)
            {
                Value = value;
            }

            public TValue Value { get; set; }
            public Node<TValue> Next { get; set; }
            public Node<TValue> Prev { get; set; }
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
            Measure(String.Empty, () => BinarySorting(RandomArray(100)), true);

            var deq = RandomArray(1000);
            
            // тест
            Measure("BinarySort", () => BinarySorting(deq));

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

        /// <summary>
        /// Сортировать деку
        /// </summary>
        /// <param name="unsortedArray">Не отсортированная дека Deq<T></param>
        /// <returns>Отсортированная дека Deq<T></returns>
        private static Deq<int> BinarySorting(Deq<int> unsortedArray)
        {
            for (var i = 1; i < unsortedArray.Count; i++) {
                Console.WriteLine("Выполнено " + i + " из " + unsortedArray.Count + "("
                                  + i / (unsortedArray.Count / 100) + "%)");
                var ins = BinarySearch(unsortedArray, 0, i, unsortedArray[i]);
                var tmp = unsortedArray[i];

                for (var j = i - 1; j >= ins; j--)
                    unsortedArray[j + 1] = unsortedArray[j];

                unsortedArray[ins] = tmp;
            }

            return unsortedArray;
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

            if (key > array[mid])
                return BinarySearch(array, mid + 1, high, key);
            else if (key < array[mid])
                return BinarySearch(array, low, mid, key);

            return mid;
        }
    }
}
