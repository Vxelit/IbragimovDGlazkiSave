//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IbragimovDGlazkiSave
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductSale
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int AgentID { get; set; }
        public System.DateTime SaleDate { get; set; }

        // продажи за год(все) вывод.
        // скидки подаравить
        // сортировка по скидке
        // selectionchanged listview
        // окно пир изменении приоритета. максимальное приходит через параметр.

        public int ProductCount { get; set; }

        public decimal Stoimost
        {
            get
            {
                return Product.MinCostForAgent * ProductCount;
            }
        }

        public virtual Agent Agent { get; set; }
        public virtual Product Product { get; set; }
    }
}
