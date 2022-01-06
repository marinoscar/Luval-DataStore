using Luval.DataStore.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Luval.DataStore.xTests
{
    public class When_Db_DataStore_Is_Used
    {
        [Fact]
        public void When_Is_Created_Should_Throw_Exception_For_Null_Factory()
        {
            var res = false;
            try
            {
                new DbDataStore(null, new SqlCommandFactory(), new ReflectionDataRecordMapper());
            }
            catch (ArgumentNullException e)
            {
                res = true;
            }
            Assert.True(res);
            res = false;
            try
            {
                new DbDataStore(() => new SqlConnection(), null, new ReflectionDataRecordMapper());
            }
            catch (ArgumentNullException)
            {
                res = true;
            }
            Assert.True(res);
            res = false;
            try
            {
                new DbDataStore(() => new SqlConnection(), new SqlCommandFactory(), null);
            }
            catch (ArgumentNullException)
            {
                res = true;
            }
            Assert.True(res);
        }
    }
}
