using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSimpleStocks.Entities;
using System.Collections;
using System.Runtime.Serialization;
using SuperSimpleStocks.BLL;
using System.Globalization;

namespace SuperSimpleStocks
{
    class Program
    {
        /// <summary>
        ///  Initialization dictionary
        /// </summary>
        private static Dictionary<Stock, List<Trade>> map;

        /// <summary>
        /// Initializazion call To BLL
        /// </summary>
        private static StockCalculator calc = new StockCalculator();

        /// <summary>
        ///  Method Main
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string answer = string.Empty;

            do
            {
                map = new Dictionary<Stock, List<Trade>>();

                List<Stock> stocks = PopulateTable();

                List<Trade> trades = GetListTrade(stocks);

                Console.WriteLine("+--------+----------------+-----------+-------------+");
                Console.WriteLine("| Symbol | Dividend yield | P/E ratio | Stock price |");
                Console.WriteLine("+--------+----------------+-----------+-------------+");

                Calculations(stocks, trades);

                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("+----------------+");
                Console.WriteLine("| GBCE All share |");
                Console.WriteLine("+----------------+");

                CalculateAllShare(stocks);

                Console.WriteLine();
                Console.WriteLine();

                Console.Write("Are you done? [yes] [no] : ");
                answer = Console.ReadLine();
            } while (answer != "yes");
                
        }

        /// <summary>
        /// Method for initializazion Table with Sample data from the Global Beverage Corporation Exchange
        /// </summary>
        /// <returns></returns>
        static List<Stock> PopulateTable()
        {
            Stock tea = new Stock(StockSymbol.TEA, StockType.Common, 0d, 0, 100d, 110d);
            Stock pop = new Stock(StockSymbol.POP, StockType.Common, 8d, 0, 100d, 120d);
            Stock ale = new Stock(StockSymbol.ALE, StockType.Common, 23d, 0, 60d, 55d);
            Stock gin = new Stock(StockSymbol.GIN, StockType.Preferred, 8d, 2d, 100d, 100d);
            Stock joe = new Stock(StockSymbol.JOE, StockType.Common, 13d, 0, 250d, 246.12d);

            List<Stock> stocks = new List<Stock>();

            stocks.Add(tea);
            stocks.Add(pop);
            stocks.Add(ale);
            stocks.Add(gin);
            stocks.Add(joe);

            return stocks;
        }

        /// <summary>
        /// Methos that return a random Type of Buy or Sell
        /// </summary>
        /// <returns></returns>
        static BuyOrSellEnum GetRandomType()
        {
            BuyOrSellEnum result;

            Random rand = new Random();
            int randomValue = (rand.Next(((100 - 10) + 1)) + 10);
            if (randomValue > 50)
                result = BuyOrSellEnum.Sell;
            else
                result = BuyOrSellEnum.Buy;

            return result;
        }

        /// <summary>
        /// Method that return a list of random Trades
        /// </summary>
        /// <param name="stocks">List of Stocks</param>
        /// <returns></returns>
        static List<Trade> GetListTrade(List<Stock> stocks)
        {
            Stock stock = null;
            Trade trade = null;
            Random rand = new Random();
            List<Trade> trades = null;

            int nbTrades = (rand.Next(((100 - 10) + 1)) + 10);

            for (int i = 0; (i < nbTrades); i++)
            {
                stock = stocks[(rand.Next((stocks.Count() - 1)))];

                if (map.ContainsKey(stock))
                    trades = map[stock];

                if ((trades == null))
                {
                    trades = new List<Trade>();

                    map.Add(stock, trades);
                }

                trade = new Trade(stock, new DateTime(), (rand.NextDouble() * 1000d + 50d), GetRandomType(), (rand.NextDouble() * 1000d + 50d));

                trades.Add(trade);
            }

            return trades;
        }

        /// <summary>
        /// Method that performs calculations of Dividend Yield , P/E Ratio, Geometric Mean and Volume Weighted Stock Price
        /// </summary>
        /// <param name="stocks">List of Stocks</param>
        /// <param name="trades">List of Trades</param>
        static void Calculations(List<Stock> stocks, List<Trade> trades)
        {
            foreach (Stock stockForCalculs in stocks)
            {
                string stockType = string.Empty;
                string stockPrice = string.Empty;

                var _trade = trades.Where(x => x.getStock().StockSymbol == stockForCalculs.StockSymbol).ToList();

                string simbols = stockForCalculs.getStockSymbol().ToString();

                if (stockForCalculs.getStockType().Equals(StockType.Common))
                    stockType = calc.calculateDividendYieldCommon(stockForCalculs.getParValue(), stockForCalculs.getLastDividend()).ToString("0.00", CultureInfo.InvariantCulture);
                else
                    stockType = calc.calculateDividendYieldPreferred(stockForCalculs.getParValue(), stockForCalculs.getParValue(), stockForCalculs.getFixedDividend()).ToString("0.00", CultureInfo.InvariantCulture);

                string ratio = calc.calculatePeRatio(stockForCalculs.getParValue(), stockForCalculs.getLastDividend()).ToString("0.00", CultureInfo.InvariantCulture);

                stockPrice = calc.calculateStockPrice(_trade).ToString("0.00", CultureInfo.InvariantCulture);

                Console.WriteLine("|" + simbols.PadLeft(8, ' ') + "|" + stockType.PadLeft(16, ' ') + "|" + ratio.PadLeft(11, ' ') + "|" + stockPrice.PadLeft(13, ' ') + "|");

                Console.WriteLine("+--------+----------------+-----------+-------------+");
            }
        }

        /// <summary>
        /// Method that performs calculations of Shares Indexes
        /// </summary>
        /// <param name="stocks">List of Stocks</param>
        static void CalculateAllShare(List<Stock> stocks)
        {
            var sharesIndexes = calc.calculateSharesIndexes(stocks);

            Console.WriteLine("|" + sharesIndexes.ToString("0.00", CultureInfo.InvariantCulture).PadLeft(16, ' ') + "|");

            Console.WriteLine("+----------------+");
        }
    }
}