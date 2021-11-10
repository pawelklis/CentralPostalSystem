using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CentralPostalSystem
{
    class DimmensionCategoryType
    {
        public string Code { get; set; }
        public string ParcelTypeCode { get; set; }
        public decimal Price { get; set; }
        public int MinLenght { get; set; }
        public int MaxLenght { get; set; }
        public int MinWidth { get; set; }
        public int MaxWidth { get; set; }
        public int MinDepth { get; set; }
        public int MaxDepth { get; set; }
        public int MinWeight { get; set; }
        public int MaxWeight { get; set; }

    }
}
