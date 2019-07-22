using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            IObservable<List<Stock>> stockTicker = new StockTicker();

            List<string> watchList = new List<string>();

            watchList.Add(Stocks.Stock1.ToString()); 
            //watchList.Add(Stocks.Stock2.ToString());

            StockMonitor monitor = new StockMonitor(watchList);

            stockTicker.Subscribe(monitor);
        }
    }

}
