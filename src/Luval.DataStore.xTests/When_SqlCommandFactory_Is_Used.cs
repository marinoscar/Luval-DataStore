using Luval.DataStore.Database;
using System;
using System.Data.SqlClient;
using Xunit;

namespace Luval.DataStore.xTests
{
    public class When_SqlCommandFactory_Is_Used
    {

        [Fact]
        public void It_Should_Throw_Exception_Creating_Instance_With_Null_Connection()
        {
            var success = false;
            try
            {
                var a = new SqlCommandFactory(null);
            }
            catch (ArgumentNullException)
            {
                success = true;
            }
            Assert.True(success, "Should throw ArgumentNullException exception");
        }

        [Fact]
        public void It_Should_Throw_Exception_Creating_Instance_With_Null_Options()
        {
            var success = false;
            try
            {
                var a = new SqlCommandFactory(new SqlConnection(), null);
            }
            catch (ArgumentNullException)
            {
                success = true;
            }

            Assert.True(success, "Should throw ArgumentNullException exception");
        }
    }
}
