# SummerBatchCore for MySQL
Summer Batch is a lightweight, reliable, efficient, open-source batch framework for the C# community. This version is dedicated to .Net Core.

For more information, see https://github.com/SummerBatch/SummerBatchCore and https://github.com/SummerBatch/SummerBatch .

Summer Batch does not officially support MySQL.
It is a support implementation for MySQL.

## Usage
### Required NuGet package
- MySql.Data

### Configure
- App.config
  ```c#
  <add name="Default"
   providerName="MySql.Data.MySqlClient"
   connectionString="Server=localhost; User ID=userName; Password=password; Initial Catalog=databaseName;" />
  ```

### Code
The DatabaseExtensionManager uses AppDomain.CurrentDomain.GetAssemblies(),
so you need to load extension assembly as blow:
- Program.cs
  ```c#
  // load MySQL extension
  Summer.Batch.Support.MySql.MySqlExtension.Load();
  // register for MySQL
  DbProviderFactories.RegisterFactory("MySql.Data.MySqlClient", MySql.Data.MySqlClient.MySqlClientFactory.Instance);
  ```