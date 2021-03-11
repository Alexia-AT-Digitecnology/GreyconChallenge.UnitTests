using System;
using System.IO.Enumeration;
using GreyconChallenge.Base;
using Xunit;

namespace GreyconChallenge.UnitTests
{
    public class Tests
    {
        [Theory]
        [InlineData("Resources/test1.dat", 1)]
        [InlineData("Resources/test2.dat", 2)]
        [InlineData("Resources/test3.dat", 5)]
        [InlineData("Resources/test4.dat", 20)]
        public void PreconciliateTests(string fileName, int result)
        {
            HardDiskContainer container = new HardDiskContainer();
            
            container.LoadFromFile(fileName);
            
            Assert.Equal(result, container.Preconciliate());
        }

        [Fact]
        public void TestRandomDisk()
        {
            HardDisk.GenerateRandom();
        }
        
        [Fact]
        public void TestRandomEnvironment()
        {
            HardDiskContainer container = new HardDiskContainer();
            
            container.GenerateRandom();
        }

        [Theory]
        [InlineData(1, 1000, true)]
        [InlineData(50, 1000, true)]
        [InlineData(509, 1000, true)]
        [InlineData(40, 30, false)]
        [InlineData(40, 2000, false)]
        public void CreateDisks(int used, int total, bool works)
        {
            bool result = false;
            
            try
            {
                HardDisk disk = new HardDisk(used, total);
                result = true;
            }
            catch (Exception e)
            {
                result = false;
            }
            
            Assert.Equal(works, result);
        }

        [Theory]
        [InlineData(20, true)]
        [InlineData(10, true)]
        [InlineData(50, true)]
        [InlineData(100, false)]
        public void AddDisksToLimit(int disks, bool works)
        {
            HardDiskContainer container = new HardDiskContainer();
            bool result = false;

            try
            {
                for (int i = 0; i <= disks - 1; i++)
                {
                    container.AddDisk(HardDisk.GenerateRandom());
                }

                result = true;
            }
            catch (Exception e)
            {
                result = false;
            }
            
            Assert.Equal(works, result);
        }
    }
}