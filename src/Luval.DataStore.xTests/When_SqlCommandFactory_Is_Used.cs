using Luval.DataStore.Database;
using Luval.DataStore.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace Luval.DataStore.xTests
{
    public class When_SqlCommandFactory_Is_Used
    {


        [Fact]
        public void It_Should_Throw_Exception_Creating_Instance_With_Null_Options()
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
        public void It_Should_Have_The_Correct_Options()
        {
            var sql = "SELECT * FROM TABLE";
            var options = new SqlCommandOptions()
            {
                CommandTimeout = 50,
                CommandType = CommandType.Text,
                Parameters = new List<IDbDataParameter>(new[] { new SqlParameter(), new SqlParameter(), new SqlParameter() })
            };
            var factory = new SqlCommandFactory(options);
            var cmd = factory.Create(sql.ToCommand(), new SqlConnection());


            Assert.True(cmd != null);
            Assert.True(cmd.CommandText == sql);
            Assert.True(cmd.CommandTimeout == 50);
            Assert.True(cmd.Parameters.Count == 3);
        }

        [Fact]
        public void It_Should_Have_The_Proper_Options()
        {
            var factory = new SqlCommandFactory(new SqlCommandOptions());

            Assert.True(factory.Options != null);
            Assert.True(factory.Options.Parameters != null);
            Assert.True(factory.Options.IsolationLevel ==  IsolationLevel.ReadCommitted);
            Assert.True(factory.Options.CommandType == CommandType.Text);
        }
    }
}

