[assembly: Xunit.TestFramework("Batcher.Tests.DI.ConfigureTestFramework", "Batcher.Tests")]

namespace Batcher.Tests.DI
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Autofac;

    using Xunit.Abstractions;
    using Xunit.Frameworks.Autofac;
    using Xunit.Sdk;

    public class ConfigureTestFramework : AutofacTestFramework
    {
        private const string TestSuffixConvention = "Tests";

        private readonly List<Tuple<Type, Type>> typesToRegister = new List<Tuple<Type, Type>>
        {
            new Tuple<Type, Type>(typeof(BatcherService), typeof(IBatcherService)),
            new Tuple<Type, Type>(typeof(Helpers.AccessHelper), typeof(Helpers.IAccessHelper)),
            new Tuple<Type, Type>(typeof(BatcherService), typeof(BatcherService)),
        };

        public ConfigureTestFramework(IMessageSink diagnosticMessageSink) : base(diagnosticMessageSink)
        {
        }

        protected override void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                  .Where(t => t.Name.EndsWith(TestSuffixConvention));

            containerBuilder.Register(context => new TestOutputHelper())
                .AsSelf()
                .As<ITestOutputHelper>()
                .InstancePerLifetimeScope();

            foreach (var typeToRegister in this.typesToRegister)
            {
                containerBuilder.RegisterType(typeToRegister.Item1).As(typeToRegister.Item2).InstancePerLifetimeScope();
            }
        }
    }
}