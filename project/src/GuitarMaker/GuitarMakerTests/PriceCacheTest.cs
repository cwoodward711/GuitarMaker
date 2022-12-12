using GuitarMaker;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarMakerTests
{
    public class PriceCacheTest
    {
        [Test]
        public void TestPriceCache()
        {
            IPriceCache priceCache = new ConcretePriceCache("testCacheFile.json");
            priceCache.SetPrice("componentA", 1);

            Assert.AreEqual(priceCache.GetPrice("componentA"), 1);

            priceCache.ClearCache();
            Assert.AreEqual(priceCache.GetPrice("componentA"), -1);
        }

        [Test]
        public void TestPriceCacheNoFile()
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string guitarMakerFolder = Path.Combine(appDataFolder, "GuitarMaker");
            Directory.CreateDirectory(guitarMakerFolder);
            string filepath = Path.Combine(guitarMakerFolder, "testCacheFile.json");
            
            IPriceCache priceCache = new ConcretePriceCache("testCacheFile.json");
            File.Delete(filepath);
            Assert.AreEqual(-1, priceCache.GetPrice("componentA"));
        }

    }
}
