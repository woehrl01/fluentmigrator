﻿using System.Data;
using FluentMigrator.Runner.Generators.Base;

namespace FluentMigrator.Runner.Generators.Firebird
{
    internal class FirebirdTypeMap : TypeMapBase
    {
        private const int DecimalCapacity = 19;
        private const int FirebirdMaxVarcharSize = 32765;
        private const int FirebirdMaxCharSize = 32767;
        private const int FirebirdMaxUnicodeCharSize = 4000;

        protected override void SetupTypeMaps()
        {
            /*
             * Values were taken from the Interbase 6 Data Definition Guide
             * 
             * */
            SetTypeMap(DbType.AnsiStringFixedLength, "CHAR(255)");
            SetTypeMap(DbType.AnsiStringFixedLength, "CHAR($size)", FirebirdMaxCharSize);
            SetTypeMap(DbType.AnsiString, "VARCHAR(255)");
            SetTypeMap(DbType.AnsiString, "VARCHAR($size)", FirebirdMaxVarcharSize);
            SetTypeMap(DbType.AnsiString, "BLOB SUB_TYPE TEXT", int.MaxValue);
            SetTypeMap(DbType.Binary, "BLOB SUB_TYPE BINARY");
            SetTypeMap(DbType.Binary, "BLOB SUB_TYPE BINARY", int.MaxValue);
            SetTypeMap(DbType.Boolean, "SMALLINT"); //no direct boolean support
            SetTypeMap(DbType.Byte, "SMALLINT");
            SetTypeMap(DbType.Currency, "DECIMAL(18, 4)");
            SetTypeMap(DbType.Date, "DATE");
            SetTypeMap(DbType.DateTime, "TIMESTAMP");
            SetTypeMap(DbType.Decimal, "DECIMAL(14,5)");
            SetTypeMap(DbType.Decimal, "DECIMAL($size, $precision)", DecimalCapacity);
            SetTypeMap(DbType.Double, "DOUBLE PRECISION"); //64 bit double precision
            SetTypeMap(DbType.Guid, "CHAR(16) CHARACTER SET OCTETS"); //no guid support, "only" uuid is supported(via gen_uuid() built-in function)
            SetTypeMap(DbType.Int16, "SMALLINT");
            SetTypeMap(DbType.Int32, "INTEGER");
            SetTypeMap(DbType.Int64, "BIGINT");
            SetTypeMap(DbType.Single, "FLOAT");
            SetTypeMap(DbType.StringFixedLength, "CHAR(255) CHARACTER SET NONE"); //charset utf8 may fail at index creation, because of to small database pagesize
            SetTypeMap(DbType.StringFixedLength, "CHAR($size) CHARACTER SET NONE", FirebirdMaxUnicodeCharSize);
            SetTypeMap(DbType.String, "VARCHAR(255) CHARACTER SET NONE");
            SetTypeMap(DbType.String, "VARCHAR($size) CHARACTER SET NONE", FirebirdMaxUnicodeCharSize);
            SetTypeMap(DbType.String, "BLOB SUB_TYPE TEXT", int.MaxValue);
            SetTypeMap(DbType.Time, "TIME");
        }
    }
}