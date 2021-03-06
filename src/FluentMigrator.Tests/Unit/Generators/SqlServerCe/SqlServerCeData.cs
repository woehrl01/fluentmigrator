﻿using System;
using FluentMigrator.Runner.Extensions;
using FluentMigrator.Runner.Generators.SqlServer;
using NUnit.Framework;
using NUnit.Should;

namespace FluentMigrator.Tests.Unit.Generators.SqlServerCe
{
    public class SqlServerCeData : GeneratorTestBase
    {
        protected SqlServerCeGenerator generator;

        [SetUp]
        public void Setup()
        {
            generator = new SqlServerCeGenerator();
        }

        [Test]
        public void CanInsertData()
        {
            var expression = GeneratorTestHelper.GetInsertDataExpression();
            var sql = generator.Generate(expression);

            var expected = "INSERT INTO [TestTable1] ([Id], [Name], [Website]) VALUES (1, 'Just''in', 'codethinked.com');";
            expected += @" INSERT INTO [TestTable1] ([Id], [Name], [Website]) VALUES (2, 'Na\te', 'kohari.org')";

            sql.ShouldBe(expected);
        }

        [Test]
        public void CanInsertDataWithIdentityInsert()
        {
            var expression = GeneratorTestHelper.GetInsertDataExpression();
            expression.AdditionalFeatures.Add(SqlServerExtensions.IdentityInsert, true);
            var sql = generator.Generate(expression);

            var expected = "SET IDENTITY_INSERT [TestTable1] ON;";
            expected += " INSERT INTO [TestTable1] ([Id], [Name], [Website]) VALUES (1, 'Just''in', 'codethinked.com');";
            expected += @" INSERT INTO [TestTable1] ([Id], [Name], [Website]) VALUES (2, 'Na\te', 'kohari.org');";
            expected += " SET IDENTITY_INSERT [TestTable1] OFF";

            sql.ShouldBe(expected);
        }

        [Test]
        public void CanInsertDataWithIdentityInsertInStrictMode()
        {
            var expression = GeneratorTestHelper.GetInsertDataExpression();
            expression.AdditionalFeatures.Add(SqlServerExtensions.IdentityInsert, true);
            generator.compatabilityMode = Runner.CompatabilityMode.STRICT;
            var sql = generator.Generate(expression);

            var expected = "SET IDENTITY_INSERT [TestTable1] ON;";
            expected += " INSERT INTO [TestTable1] ([Id], [Name], [Website]) VALUES (1, 'Just''in', 'codethinked.com');";
            expected += @" INSERT INTO [TestTable1] ([Id], [Name], [Website]) VALUES (2, 'Na\te', 'kohari.org');";
            expected += " SET IDENTITY_INSERT [TestTable1] OFF";

            sql.ShouldBe(expected);
        }

        [Test]
        public void CanDeleteData()
        {
            var expression = GeneratorTestHelper.GetDeleteDataExpression();

            var sql = generator.Generate(expression);

            sql.ShouldBe("DELETE FROM [TestTable1] WHERE [Name] = 'Just''in' AND [Website] IS NULL");
        }

        [Test]
        public void CanDeleteDataAllRows()
        {
            var expression = GeneratorTestHelper.GetDeleteDataAllRowsExpression();

            var sql = generator.Generate(expression);

            sql.ShouldBe("DELETE FROM [TestTable1] WHERE 1 = 1");
        }

        [Test]
        public void CanDeleteDataMulitpleRows()
        {
            var expression = GeneratorTestHelper.GetDeleteDataMultipleRowsExpression();

            var sql = generator.Generate(expression);

            sql.ShouldBe("DELETE FROM [TestTable1] WHERE [Name] = 'Just''in' AND [Website] IS NULL; DELETE FROM [TestTable1] WHERE [Website] = 'github.com'");
        }

        [Test]
        public void CanInsertGuidData()
        {

            var expression = GeneratorTestHelper.GetInsertGUIDExpression();

            var sql = generator.Generate(expression);

            var expected = String.Format("INSERT INTO [TestTable1] ([guid]) VALUES ('{0}')", GeneratorTestHelper.TestGuid.ToString());

            sql.ShouldBe(expected);
        }

        [Test]
        public void CanUpdateData()
        {
            var expression = GeneratorTestHelper.GetUpdateDataExpression();

            var sql = generator.Generate(expression);
            sql.ShouldBe("UPDATE [TestTable1] SET [Name] = 'Just''in', [Age] = 25 WHERE [Id] = 9 AND [Homepage] IS NULL");
        }

        [Test]
        public void CanUpdateDataForAllRows()
        {
            var expression = GeneratorTestHelper.GetUpdateDataExpressionWithAllRows();

            var sql = generator.Generate(expression);
            sql.ShouldBe("UPDATE [TestTable1] SET [Name] = 'Just''in', [Age] = 25 WHERE 1 = 1");
        }
    }
}
