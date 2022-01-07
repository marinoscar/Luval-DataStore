using Luval.DataStore.Database;
using Luval.DataStore.Extensions;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Luval.DataStore.xTests
{
    public class When_Sql_DataStore_Is_Used
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

        [Fact]
        public void When_Scalar_Is_Called_Proper_Method_Is_Used()
        {
            var scalar = "PASS";
            var cmMock = new Mock<IDbCommand>();
            cmMock.Setup(c => c.ExecuteScalar()).Returns(scalar);
            var cnMock = GetConnectionMock(cmMock, GetTransactionMock());

            var db = new DbDataStore(() => { return cnMock.Object; }, new SqlCommandFactory(), new ReflectionDataRecordMapper());
            var res = db.ExecuteScalar("".ToCommand());
            cmMock.Verify(c => c.ExecuteScalar(), Times.Once());
            Assert.True(scalar.Equals(res));
        }

        [Fact]
        public void When_Scalar_Is_Called_Proper_Casting_Is_Done()
        {
            ScalarTest<DateTime>(DateTime.Today);
            ScalarTest<double>(100d);
            ScalarTest<string>("HELLO");
            ScalarTest<Guid>(Guid.NewGuid());
            ScalarTest<DBNull>(DBNull.Value);
        }

        private void ScalarTest<T>(T value)
        {
            var cmMock = new Mock<IDbCommand>();
            cmMock.Setup(c => c.ExecuteScalar()).Returns(value);
            var cnMock = GetConnectionMock(cmMock, GetTransactionMock());

            var db = new DbDataStore(() => { return cnMock.Object; }, new SqlCommandFactory(), new ReflectionDataRecordMapper());
            var res = db.ExecuteScalar<T>("".ToCommand());
            cmMock.Verify(c => c.ExecuteScalar(), Times.Once());
            Assert.Equal<T>(value, res);

        }


        private Mock<IDbConnection> GetConnectionMock(Mock<IDbCommand> cmMock, Mock<IDbTransaction> trMock)
        {
            var moq = new Mock<IDbConnection>();
            moq.Setup(m => m.CreateCommand()).Returns(cmMock.Object);
            moq.Setup(m => m.BeginTransaction()).Returns(trMock.Object);
            moq.Setup(m => m.BeginTransaction(It.IsAny<IsolationLevel>())).Returns(trMock.Object);
            moq.SetupGet(p => p.ConnectionTimeout).Returns(30);
            cmMock.SetupGet(p => p.Connection).Returns(moq.Object);
            trMock.SetupGet(p => p.Connection).Returns(moq.Object);
            return moq;
        }

        private Mock<IDbTransaction> GetTransactionMock()
        {
            var moq = new Mock<IDbTransaction>();
            moq.SetupGet(p => p.IsolationLevel).Returns(IsolationLevel.ReadCommitted);
            return moq;
        }
    }
}
