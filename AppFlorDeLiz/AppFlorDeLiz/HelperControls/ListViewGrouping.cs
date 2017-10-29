﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AppFlorDeLiz.HelperControls
{
    public class ListViewGrouping<K, T> : Collection<T>
    {
        public K Key { get; private set; }

        public ListViewGrouping(K key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items)
                this.Items.Add(item);

            {

            }
        }
    }
}

