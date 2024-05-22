using ClassLibraryLab10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork12
{
    // Узел двунаправленного списка
    internal class PointBiList<T> where T : IInit, ICloneable, new()
    {
        public T Data { get; set; }                 // Поле для хранения данных
        public PointBiList<T> Next { get; set; }    // Ссылка на следующий узел  
        public PointBiList<T> Prev { get; set; }    // Ссылка на предыдущий узел


        // Конструктор с параметром:    data
        public PointBiList(T data)
        {
            Data = data;    // Присваивание данных
            Next = null;    // Инициализация ссылки на следующий узел как null
            Prev = null;    // Инициализация ссылки на предыдущий узел как null
        }
    }
}
