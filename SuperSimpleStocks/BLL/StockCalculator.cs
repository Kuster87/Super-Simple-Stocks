using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSimpleStocks.Entities;

namespace SuperSimpleStocks.BLL
{
    class StockCalculator
    {
        #region "Main Method"
        public double calculateStockPrice(List<Trade> trades)
        {

            double[] tradesPrices = new double[trades.Count()];
            double[] tradesQuantities = new double[trades.Count()];
            int i = 0;
            foreach (Trade trade in trades)
            {
                tradesPrices[i] = trade.getPrice();
                tradesQuantities[i] = trade.getSharesQuantity();
                i++;
            }

            return calculateStockPrice(tradesPrices, tradesQuantities);

        }

        public double calculateSharesIndexes(List<Stock> stocks)
        {
            double[] tradesPrices = new double[stocks.Count()];
            double totalParValues = 0;
            int i = 0;
            foreach (Stock stock in stocks)
            {
                totalParValues = (totalParValues + stock.getParValue());
                tradesPrices[i] = stock.getPrice();
                i++;
            }

            double geometricMean = calculateGeometricMean(tradesPrices);

            return (geometricMean / totalParValues);
        }

        public double calculateGeometricMean(List<Stock> stocks)
        {
            double[] tradesPrices = new double[stocks.Count()];
            double totalParValues = 0;
            int i = 0;
            foreach (Stock stock in stocks)
            {
                totalParValues = (totalParValues + stock.getParValue());
                tradesPrices[i] = stock.getPrice();
                i++;
            }

            double geometricMean = calculateGeometricMean(tradesPrices);

            return (geometricMean / totalParValues);
        }

        #endregion

        #region "Operation"

        public double calculateDividendYieldCommon(double tickerPrice, double lastDividend)
        {
            return (lastDividend / tickerPrice);
        }

        public double calculateDividendYieldPreferred(double tickerPrice, double parValue, double fixedDividend)
        {
            return ((fixedDividend * parValue) / tickerPrice);
        }

        public double calculatePeRatio(double tickerPrice, double lastDividend)
        {
            return (tickerPrice / lastDividend);
        }

        public double calculateGeometricMean(double[] tradesPrices)
        {

            double geometricMean = tradesPrices[0];
            for (int i = 1; (i < tradesPrices.Count()); i++)
            {
                geometricMean = (geometricMean * tradesPrices[i]);
            }

            double n = tradesPrices.Count();

            return Math.Pow(geometricMean, 1.0d / n);
        }

        public double calculateStockPrice(double[] tradesPrices, double[] tradesQuantities)
        {
            double pricesPerQuantities = 0;
            double quantities = 0;

            for (int i = 1; (i < tradesPrices.Count()); i++)
            {
                pricesPerQuantities = (pricesPerQuantities + (tradesPrices[i] * tradesQuantities[i]));
                quantities = (quantities + tradesQuantities[i]);
            }

            double stockPrice = (pricesPerQuantities / quantities);
             
            return double.IsNaN(stockPrice) ? 0D : stockPrice;
        }
        #endregion
    }
}
