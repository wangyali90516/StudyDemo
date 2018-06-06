using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternDemo
{
    public class Coin
    {
        /// <summary>
        ///     面额所需资金
        /// </summary>
        private Coin(long par, long amount)
        {
            this.Par = par;
            this.Amount = amount;
        }

        private Coin(long par, Coin baseCoin)
        {
            this.Par = par;
            this.Amount = this.Par / baseCoin.Par * baseCoin.Amount;
        }

        public long Amount { get; set; }
        public int Number { get; set; }
        public long Par { get; set; }

        public static Coin Create100(long amount100)
        {
            return new Coin(100 * 100, amount100);
        }

        public static Coin Create1000(long amount100)
        {
            return new Coin(1000 * 100, Create100(amount100));
        }

        public static Coin Create10000(long amount100)
        {
            return new Coin(10000 * 100, Create100(amount100));
        }

        public static Coin Create200(long amount100)
        {
            return new Coin(200 * 100, Create100(amount100));
        }

        public static Coin Create2000(long amount100)
        {
            return new Coin(2000 * 100, Create100(amount100));
        }

        public static Coin Create500(long amount100)
        {
            return new Coin(500 * 100, Create100(amount100));
        }

        public static Coin Create5000(long amount100)
        {
            return new Coin(5000 * 100, Create100(amount100));
        }

        public static List<Coin> CreateCoinGroupByAmount(long amount, long amount100)
        {
            List<Coin> fullCoin = CreateFullCoin(amount100);
            return fullCoin.Where(u => u.Amount <= amount).OrderByDescending(u => u.Par).ToList();
        }

        public static List<Coin> CreateFullCoin(long amount100)
        {
            return new List<Coin> { Create10000(amount100), Create5000(amount100), Create2000(amount100), Create1000(amount100), Create500(amount100), Create200(amount100), Create100(amount100) };
        }

        public Coin Clone()
        {
            return new Coin(this.Par, this.Amount);
        }
    }

    public class Matched
    {
        public long Amount { get; set; }
        public string assetId { get; set; }
    }

    public class OnSellAsset
    {
        /// <summary>
        /// 资产Id
        /// </summary>
        public string AssetId { get; set; }

        /// <summary>
        /// 每份金额
        /// </summary>
        public long EachAmount { get; set; }

        /// <summary>
        /// 剩余金额
        /// </summary>
        public long RemainderAmount { get; set; }
    }

    internal class 分配资产
    {
        //public static void Main()
        //{
        //    List<Matched> amount = new List<Matched>();
        //    List<OnSellAsset> assets = new List<OnSellAsset>();
        //    for (int i = 0; i < 10; i++)
        //    {
        //        OnSellAsset onSellAsset = new OnSellAsset
        //        {
        //            AssetId = Guid.NewGuid().ToString("N"),
        //            EachAmount = 9780,
        //            RemainderAmount = 3960900
        //        };
        //        assets.Add(onSellAsset);
        //    }
        //    while (assets.Any(u => u.RemainderAmount > 0))
        //    {
        //        foreach (OnSellAsset asset in assets.Where(u => u.RemainderAmount > 0))
        //        {
        //            Coin baseCoin = Coin.Create100(asset.EachAmount);
        //            List<Coin> assetCoins = Coin.CreateCoinGroupByAmount(asset.RemainderAmount, baseCoin.Amount);

        //            if (assetCoins.Any())
        //            {
        //                Coin matchCoin = assetCoins.First();
        //                amount.Add(Build(asset, matchCoin));
        //                asset.RemainderAmount -= matchCoin.Amount;
        //            }
        //            else
        //            {
        //            }
        //        }
        //    }
        //    foreach (var item in amount)
        //    {
        //        Console.WriteLine(item.assetId + "   " + item.Amount);
        //    }
        //    Console.ReadKey();
        //}

        /// <summary>
        ///     主订单债权关系
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="coin"></param>
        /// <returns></returns>
        private static Matched Build(OnSellAsset asset, Coin coin)
        {
            return new Matched
            {
                assetId = asset.AssetId,
                Amount = coin.Amount
            };
        }
    }
}