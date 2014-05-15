using System.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MoviesManager.Models;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace MoviesManager.Data
{
    public class Database
    {
        public static void Configure()
        {
            
            Fluently.Configure()
               .Database(MsSqlConfiguration.MsSql2008
                       .ConnectionString(c => c.FromConnectionStringWithKey("MoviesConnectionString")))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<MovieMap>())
                .ExposeConfiguration(cfg =>
                {
                    var recreateSchema = bool.Parse(ConfigurationManager.AppSettings["RecreateDatabase"]);

                    if (recreateSchema)
                    {
                        var schema = new SchemaExport(cfg);
                        schema.Drop(false, true);
                        schema.Create(false, true);

                        // the lookup is dependend on the version of the rat (so we need to wait for that seeder to complete)
                        cfg.SetProperty("adonet.batch_size", "300");
                        cfg.SetProperty("generate_statistics", "true");
                    }

                }).BuildConfiguration();

        }

        public static ISessionFactory BuildSessionFactory()
        {
            IPersistenceConfigurer configurer = null;
            ISessionFactory factory = null;

            configurer =
                            MsSqlConfiguration.MsSql2008.ConnectionString(
                                c => c.FromConnectionStringWithKey("MoviesConnectionString"));

            factory = Fluently.Configure()
                .Database(configurer)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<MovieMap>())
                .ExposeConfiguration(cfg => cfg.SetProperty("adonet.batch_size", "100"))
                .ExposeConfiguration(cfg => cfg.SetProperty("generate_statistics", "true"))
                .ExposeConfiguration(cfg => cfg.SetProperty("prepare_sql", "true"))
                .BuildSessionFactory();

            return factory;
        }
    }
}