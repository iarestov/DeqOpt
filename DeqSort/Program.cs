﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace DeqSort
{

    internal class Deq<T>
    {
        private Node<T> First;
        private Node<T> Current;
        private Node<T> Last;
        private int size = 0;

        /// <summary>
        /// Контруктор
        /// </summary>
        public Deq()
        {
            size = 0;
            First = Current = Last = null;
        }

        /// <summary>
        /// Количество элементов в деке
        /// </summary>
        public int Count { get { return size; } }

        /// <summary>
        /// Пуста ли дека
        /// </summary>
        public bool Empty { get { return size > 0; } }

        /// <summary>
        /// Посмотреть на элемент в начале деки
        /// </summary>
        /// <returns>(T)Элемент</returns>
        public T Front { get { return First.Value; } }

        /// <summary>
        /// Посмотреть на элемент в конце деки
        /// </summary>
        /// <returns>(T)Элемент</returns>
        public T Back { get { return Last.Value; } }

        /// <summary>
        /// Вставить элемент в конец деки
        /// </summary>
        /// <param name="item">(T)Элемент</param>
        public void PushBack(T item)
        {
            var node = new Node<T>(item);
            if (Last == null && First == null)
                Last = First = node;
            else {
                node.Prev = Last;
                Last.Next = node;
                Last = node;
            }
            size++;
        }

        /// <summary>
        /// Вытянуть элемент с начала деки
        /// </summary>
        /// <returns>(T)Элемент</returns>
        public void PushFront(T item)
        {
            var node = new Node<T>(item);
            if (Last == null && First == null)
                Last = First = node;
            else {
                node.Next = First;
                First.Prev = node;
                First = node;
            }
            size++;
        }

        /// <summary>
        /// Вытянуть элемент с конца деки
        /// </summary>
        /// <returns>(T)Элемент</returns>
        public T PopBack()
        {
            var item = Last.Value;
            if (First != Last)
                Last.Prev.Next = null;
            Last = Last.Prev;
            size--;
            return item;
        }

        /// <summary>
        /// Вытянуть элемент с начала деки
        /// </summary>
        /// <returns>(T)Элемент</returns>
        public T PopFront()
        {
            var item = First.Value;
            if (First != Last)
                First.Next.Prev = null;
            First = First.Next;
            size--;
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
            for (var i = 0; i <= position; i++)
                PushBack(PopFront());

            PopFront();
            PushFront(value);

            for (var i = 0; i <= position; i++)
                PushFront(PopBack());
        }
    }

    internal class Node<T>
    {
        private T _Number;
        private Node<T> _Next;
        private Node<T> _Prev;
        public T Value { get { return _Number; } set { _Number = value; } }

        public Node(T Number)
        {
            this._Number = Number;
        }

        public Node<T> Next { get { return this._Next; } set { this._Next = value; } }
        public Node<T> Prev { get { return this._Prev; } set { this._Prev = value; } }
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

            // Создаём, сортируем и выводим деку
            Console.WriteLine(PrintArray(BinarySorting(RandomArray(1000))) + "\n");

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

        /// <summary>
        /// Сформировать строку представляющую массив
        /// </summary>
        /// <param name="sortedArray">Дек</param>
        /// <returns>Строка из массива</returns>
        private static string PrintArray(Deq<int> sortedArray)
        {
            string result = "";
            var count = sortedArray.Count;

            for (var i = 0; i < count; i++) {
                sortedArray.PushBack(sortedArray.PopFront());
                result += sortedArray.Front + ",";
            }

            for (var i = 0; i < count; i++)
                sortedArray.PushFront(sortedArray.PopBack());

            return result.Substring(0, result.Length - 1);
        }

        /// <summary>
        /// Сортировать деку
        /// </summary>
        /// <param name="unsortedArray">Не отсортированная дека Deq<T></param>
        /// <returns>Отсортированная дека Deq<T></returns>
        private static Deq<int> BinarySorting(Deq<int> unsortedArray)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            for (var i = 1; i < unsortedArray.Count; i++) {
                Console.WriteLine("Выполнено " + i + " из " + unsortedArray.Count + "("
                                  + i / (unsortedArray.Count / 100) + "%)");
                var ins = BinarySearch(unsortedArray, 0, i, unsortedArray[i]);
                var tmp = unsortedArray[i];

                for (var j = i - 1; j >= ins; j--)
                    unsortedArray[j + 1] = unsortedArray[j];

                unsortedArray[ins] = tmp;
            }
            stopWatch.Stop();
            Console.WriteLine("Прошло " + stopWatch.Elapsed + "милисекунд");

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