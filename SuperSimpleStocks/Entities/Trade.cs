using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimpleStocks.Entities
{

    /// <summary>
    /// Enum of Buy Or Sell
    /// </summary>
    public enum BuyOrSellEnum
    {
        Buy,
        Sell
    }

    /// <summary>
    /// Class of Trades
    /// </summary>
    class Trade
    {
        #region "Members"
        Stock Stock { get; set; }
        DateTime Timestamp { get; set; }
        double SharesQuantity { get; set; }
        BuyOrSellEnum BuyOrSellIndicator { get; set; }
        double Price { get; set; }

        #endregion

        #region "Constructor"

        public Trade(Stock stock, DateTime timestamp, Double sharesQuantity, BuyOrSellEnum buyOrSellIndicator, Double price)
        {
            this.Stock = stock;
            this.Timestamp = timestamp;
            this.SharesQuantity = sharesQuantity;
            this.BuyOrSellIndicator = buyOrSellIndicator;
            this.Price = price;
        }

        public Stock getStock()
        {
            return this.Stock;
        }

        #endregion

        #region "Public Method"

        public Double getSharesQuantity()
        {
            return this.SharesQuantity;
        }

        public Double getPrice()
        {
            return this.Price;
        }

        #endregion
    }
}