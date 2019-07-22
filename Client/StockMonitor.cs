using Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class StockMonitor : IObserver<List<Stock>>
    {
        private List<string> _watchList;
        private List<Stock> history = new List<Stock>();
        public StockMonitor(List<string> watchList)
        {
            _watchList = watchList;
        }
        
        public void OnCompleted()
        {
            Console.WriteLine("End of trading day");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine(error.Message);
        }

        public void OnNext(List<Stock> stocks)
        {
            _watchList.ForEach(ticker =>
            {
                Stock stock = stocks.Where(s => s.Ticker == ticker).First();
                Console.WriteLine(($"Ticker -  {stock.Ticker} ----- Price - {stock.Price} ----- Updated - {stock.Updated}"));
                history.Add(stock);

            });
        
        }
    }
}
