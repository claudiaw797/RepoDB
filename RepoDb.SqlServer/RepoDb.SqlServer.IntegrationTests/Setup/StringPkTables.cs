using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepoDb.SqlServer.IntegrationTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepoDb.SqlServer.IntegrationTests.Setup
{
    public static class StringPkTables
    {
        #region Constants/Publics

        public const string NVARCHAR_TABLE_NAME = "NVarCharTable";
        public const string VARCHAR_TABLE_NAME = "VarCharTable";

        #endregion

        #region Methods

        public static string GetStringPkTableCreationQuery(StringType pkType, int size) =>
            $"""
            IF (NOT EXISTS(SELECT 1 FROM [sys].[objects] WHERE type = 'U' AND name = '{pkType}Table'))
                BEGIN
                    CREATE TABLE [dbo].[{pkType}Table] (
                        [Id] {pkType}({size})    NOT NULL,
                        [Value] {pkType}({size}) NULL,
                        CONSTRAINT [PK_{pkType}Table] PRIMARY KEY CLUSTERED
                        (
                            [Id] ASC
                        )
                    ) ON [PRIMARY]
                END
            """;

        public static List<StringPkTable> CreateStringPkTables(int count, int fieldLength) =>
            [.. Enumerable.Range(0, count).Select(i => CreateStringPkTable(i, fieldLength))];

        public static StringPkTable CreateStringPkTable(int fieldValue, int size)
        {
            var value = new string(Convert.ToChar(fieldValue + 65), size);
            var table = new StringPkTable() {
                Id = value,
                Value = value,
            };
            return table;
        }

        public static void AssertFieldLengthEquals(this StringPkTable table, int expectedFieldLength)
        {
            Assert.AreEqual(expectedFieldLength, table.Id.Length);
            Assert.AreEqual(expectedFieldLength, table.Value.Length);
        }

        #endregion

        #region Enumerations

        public enum StringType
        {
            Char,
            NChar,
            VarChar,
            NVarChar,
        }

        #endregion 
    }
}
