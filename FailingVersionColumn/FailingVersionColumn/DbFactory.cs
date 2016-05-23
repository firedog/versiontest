using System.Configuration;
using System.Linq;
using System.Reflection;
using NPoco;
using NPoco.FluentMappings;

namespace FailingVersionColumn
{
    public class DbFactory
    {
        private readonly DatabaseFactory factory;
        private readonly DatabaseType databaseType = DatabaseType.SqlServer2012;

        public DbFactory()
        {
            var fluentConfig = FluentMappingConfiguration.Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.IncludeTypes(x => x.IsClass && !x.IsAbstract && x.Namespace.EndsWith("Models") );
                scanner.WithSmartConventions();
                scanner.PrimaryKeysNamed(m => "Id"); // override primary key name convention from "SmartConventions"
                scanner.Columns
                    .IgnoreWhere(mi => ColumnInfo.FromMemberInfo(mi).IgnoreColumn)
                    .ResultWhere(mi => ColumnInfo.FromMemberInfo(mi).ResultColumn)
                    .ComputedWhere(mi => ColumnInfo.FromMemberInfo(mi).ComputedColumn)
                    .VersionWhere(mi => ColumnInfo.FromMemberInfo(mi).VersionColumn)
                    .Named(mi => mi.GetCustomAttributes<ColumnAttribute>().Any() ? mi.GetCustomAttribute<ColumnAttribute>().Name : mi.Name)
                    .Aliased(mi => mi.GetCustomAttributes<AliasAttribute>().Any() ? mi.GetCustomAttribute<AliasAttribute>().Alias : null)
                    .DbColumnTypeAs(mi => mi.GetCustomAttributes<ColumnTypeAttribute>().Any() ? mi.GetCustomAttribute<ColumnTypeAttribute>().Type : null);
            });

            var connString = ConfigurationManager.ConnectionStrings["test"].ConnectionString;

            factory = DatabaseFactory.Config(x =>
            {
                x.UsingDatabase(() => new Database(connString, databaseType));
                x.WithFluentConfig(fluentConfig);
            });
        }


        public IDatabase GetDatabase()
        {
            return factory.GetDatabase();
        }
    }
}
