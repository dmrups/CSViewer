using CSVOnlineEditor.Parsers;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CSVOnlineEditor.Tests
{
    public class ParseTests
    {
        [Theory]
        [InlineData("+7(927)666-66-66")]
        [InlineData(" 7 927 666 66 66 ")]
        [InlineData("79276666666")]
        public void Phone(string value)
        {
            Assert.True(new ValueParser().ParsePhone(value) == "79276666666");
        }

        [Fact]
        public void EmptyName()
        {
            var name = new ValueParser().ParseName("");
            Assert.True(name.Item1 == "");
            Assert.True(name.Item2 == "");
            Assert.True(name.Item3 == "");
        }

        [Fact]
        public void DoubleName()
        {
            var name = new ValueParser().ParseName("Let Srock");
            Assert.True(name.Item1 == "Let");
            Assert.True(name.Item2 == "Srock");
            Assert.True(name.Item3 == "");
        }

        [Fact]
        public void Name()
        {
            var name = new ValueParser().ParseName(" Let   Srock   Nroll ");
            Assert.True(name.Item1 == "Let");
            Assert.True(name.Item2 == "Srock");
            Assert.True(name.Item3 == "Nroll");
        }

        [Theory]
        [InlineData("")]
        [InlineData("10-10-1000")]
        [InlineData("10-10-10000")]
        public void Date(string value)
        {
            var date = new ValueParser().ParseDate(value);

            Assert.True(SqlDateTime.MinValue.Value <= date);
            Assert.True(SqlDateTime.MaxValue.Value >= date);
        }
    }
}
