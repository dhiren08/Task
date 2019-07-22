using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service
{
    public class StockTicker : IObservable<List<Stock>>
    {
        List<IObserver<List<Stock>>> observers = new List<IObserver<List<Stock>>>();

        public IDisposable Subscribe(IObserver<List<Stock>> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            Notify();
            return new Unsubscriber(observers, observer);
        }

        public void Notify()
        {
            Random r = new Random();

            while (true)
            {

                foreach (var o in observers)
                {
                    List<Stock> stockList = new List<Stock>();

                    var stock1 = new Stock { Price = r.Next(240, 270), Ticker = Stocks.Stock1.ToString(), Updated = DateTime.Now };
                    var stock2 = new Stock { Price = r.Next(180, 210), Ticker = Stocks.Stock2.ToString(), Updated = DateTime.Now };
                    stockList.Add(stock1);
                    stockList.Add(stock2);

                    o.OnNext(stockList);
                }
                Thread.Sleep(1000);
            }
        }
    }

    public class Unsubscriber : IDisposable
    {
        private List<IObserver<List<Stock>>> _observers;
        private IObserver<List<Stock>> _observer;

        public Unsubscriber(List<IObserver<List<Stock>>> observers, IObserver<List<Stock>> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }

    public class Stock
    {
        public string Ticker { get; set; }
        public int Price { get; set; }
        public DateTime Updated { get; set; }
    }

    public enum Stocks {

        Stock1,
        Stock2
    }



}
