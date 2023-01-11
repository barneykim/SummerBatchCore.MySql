using NLog;
using Summer.Batch.Data.Incrementer;
using System;
using System.Linq;

namespace Summer.Batch.Support.MySql
{
    /// <summary>
    /// Column maximum value incrementer for the mysql database.
    /// <seealso href="https://github.com/spring-projects/spring-framework/blob/main/spring-jdbc/src/main/java/org/springframework/jdbc/support/incrementer/MySQLMaxValueIncrementer.java"/>
    /// </summary>
    public class MySqlIncrementer : AbstractColumnMaxValueIncrementer
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private long[] _valueCache;
        private int _nextValueIndex = -1;

        /// <summary>
        /// Get next value from column.
        /// </summary>
        /// <returns></returns>
        public override long NextLong()
        {
            if (_nextValueIndex < 0 || _nextValueIndex >= CacheSize)
            {
                _valueCache = new long[CacheSize];
                _nextValueIndex = 0;
                using var connection = GetConnection();
                using (var updateCommand = GetCommand(string.Format("UPDATE {0} SET {1}=last_insert_id({2}+{3}) LIMIT 1;", IncrementerName, ColumnName, ColumnName, CacheSize), connection))
                {
                    var result = updateCommand.ExecuteNonQuery();
                    if (result != 1)
                    {
                        Logger.Error("update sequence table failed. {0}", result);
                    }
                }
                using (var insertCommand = GetCommand(string.Format("SELECT last_insert_id();"), connection))
                {
                    for (var i = 0; i < CacheSize; i++)
                    {
                        var result = insertCommand.ExecuteScalar();
                        _valueCache[i] = Convert.ToInt64(result);
                    }
                }
                var maxValue = _valueCache.Last();
            }
            return _valueCache[_nextValueIndex++];
        }
    }
}
