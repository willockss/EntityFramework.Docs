﻿using EntityFrameworkCore.RelationalProviderStarter.Infrastructure;
using EntityFrameworkCore.RelationalProviderStarter.Metadata;
using EntityFrameworkCore.RelationalProviderStarter.Migrations;
using EntityFrameworkCore.RelationalProviderStarter.Query.ExpressionTranslators.Internal;
using EntityFrameworkCore.RelationalProviderStarter.Query.Sql;
using EntityFrameworkCore.RelationalProviderStarter.Storage;
using EntityFrameworkCore.RelationalProviderStarter.Update;
using EntityFrameworkCore.RelationalProviderStarter.ValueGeneration;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EntityFrameworkServicesBuilderExtensions
    {
        public static EntityFrameworkServicesBuilder AddMyRelationalProvider(this EntityFrameworkServicesBuilder builder)
        {
            var serviceCollection = builder.AddRelational().GetInfrastructure();

            serviceCollection.TryAddEnumerable(ServiceDescriptor
                .Singleton
                <IDatabaseProvider,
                    DatabaseProvider<MyRelationalDatabaseProviderServices, MyRelationalProviderOptionsExtension>>());

            serviceCollection.TryAdd(new ServiceCollection()
                // all singleton services
                .AddSingleton<MyValueGeneratorCache>()
                .AddSingleton<MyRelationalAnnotationProvider>()
                .AddSingleton<MyRelationalCompositeMemberTranslator>()
                .AddSingleton<MyRelationalCompositeMethodCallTranslator>()
                .AddSingleton<MyRelationalSqlGenerationHelper>()
                .AddSingleton<MyModelSource>()
                // all scoped services
                .AddScoped<MyRelationalDatabaseCreator>()
                .AddScoped<MyRelationalDatabaseProviderServices>()
                .AddScoped<MyHistoryRepository>()
                .AddScoped<MyModificationCommandBatchFactory>()
                .AddScoped<MyQuerySqlGeneratorFactory>()
                .AddScoped<MyRelationalConnection>()
                .AddScoped<MyRelationalDatabaseCreator>()
                .AddScoped<MyUpdateSqlGenerator>());

            return builder;
        }
    }
}