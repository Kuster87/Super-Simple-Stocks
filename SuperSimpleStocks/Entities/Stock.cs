using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimpleStocks.Entities
{

    /// <summary>
    /// Enum of Stock Types
    /// </summary>
    public enum StockType
    {
        Common,
        Preferred
    }

    /// <summary>
    /// Enum of Stock Symbols
    /// </summary>
    public enum StockSymbol
    {
        TEA,
        POP,
        ALE,
        GIN,
        JOE
    }

    /// <summary>
    /// Class of Stocks
    /// </summary>
    public class Stock
    {
        #region "Members"
        public double LastDividend { get; set; }
        public double FixedDividend { get; set; }
        public double ParValue { get; set; }
        public double Price { get; set; }
        public StockType StockType { get; set; }
        public StockSymbol StockSymbol { get; set; }

        #endregion

        #region "Constructor"

        public Stock(StockSymbol stockSymbol, StockType stockType, double lastDividend, double fixedDividend, double parValue, double price)
        {
            this.StockSymbol = stockSymbol;
            this.StockType = stockType;
            this.LastDividend = lastDividend;
            this.FixedDividend = fixedDividend;
            this.ParValue = parValue;
            this.Price = price;
        }

        #endregion

        #region "Public Method"

        public StockSymbol getStockSymbol()
        {
            return this.StockSymbol;
        }

        public StockType getStockType()
        {
            return this.StockType;
        }

        public double getLastDividend()
        {
            return this.LastDividend;
        }

        public double getFixedDividend()
        {
            return this.FixedDividend;
        }

        public double getParValue()
        {
            return this.ParValue;
        }

        public double getPrice()
        {
            return this.Price;
        }

        #endregion
    }
}