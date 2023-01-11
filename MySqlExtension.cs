using Summer.Batch.Data;
using Summer.Batch.Data.Incrementer;
using Summer.Batch.Data.Parameter;
using System.Collections.Generic;

namespace Summer.Batch.Support.MySql
{
    public class MySqlExtension : IDatabaseExtension
    {
        public IEnumerable<string> ProviderNames
        {
            get
            {
                return new[] { "MySql.Data.MySqlClient" };
            }
        }

        public IPlaceholderGetter PlaceholderGetter
        {
            get
            {
                return new PlaceholderGetter(name => "@" + name, true);
            }
        }

        public IDataFieldMaxValueIncrementer Incrementer
        {
            get
            {
                return new MySqlIncrementer { ColumnName = "ID" };
            }
        }

        /// <summary>
        /// This is used for load extension assembly because the <see cref="DatabaseExtensionManager"/>
        /// uses <c>AppDomain.CurrentDomain.GetAssemblies()</c>.
        /// </summary>
        public static string Load() => "Load MySQL extension";
    }
}
