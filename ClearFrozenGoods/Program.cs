using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiMiao.ClearFrozenGoods
{
    class Program
    {
        private static readonly BLL.TD_Shop_Goods goodsBLL = new BLL.TD_Shop_Goods();
        static void Main(string[] args)
        {
            Task.WaitAll(Task.Run(() => {
                goodsBLL.ClearFrozenGoods();
            }));
        }
    }
}
