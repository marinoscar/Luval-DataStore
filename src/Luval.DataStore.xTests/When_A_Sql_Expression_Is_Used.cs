using Luval.DataStore.DataAnnotations;
using Luval.DataStore.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Luval.DataStore.xTests
{
    public class When_A_Sql_Expression_Is_Used
    {
        [Fact]
        public void It_Should_Generate_A_Valid_Where_Statement_With_String_Constant_Comparison()
        {
            var printer = new AnsiSqlExpressionPrinter();
            Expression<Func<Entity, bool>> expression = (i => i.Name == "Oscar");
            var sql = printer.Where(expression);
            Assert.True(sql == "(Name = 'Oscar')");
        }

        [Fact]
        public void It_Should_Generate_A_Valid_Where_Statement_With_String_Multiple_Constant_Comparison()
        {
            var printer = new AnsiSqlExpressionPrinter();
            Expression<Func<Entity, bool>> expression = (i => i.Name == "Oscar" && i.Id == "25" || i.Id == "30");
            var sql = printer.Where(expression);
            Assert.True(sql == "(((Name = 'Oscar') AND (Id = '25')) OR (Id = '30'))");
        }

        [Fact]
        public void It_Should_Generate_A_Valid_Where_Statement_With_Annotated_Column()
        {
            var printer = new AnsiSqlExpressionPrinter();
            Expression<Func<Entity, bool>> expression = (i => i.Salary == 15d);
            var sql = printer.Where(expression);
            Assert.True(sql == "(SalaryColumn = 15)");
        }
    }

    public class Entity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime AgeDate { get; set; }
        [ColumnName("SalaryColumn")]
        public double? Salary { get; set; }
    }
}
