using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MessageHandler.Runtime
{
    public class Container : IContainer
    {
        private readonly IDictionary<Type, IList<ContainerRegistration>> _registrations = new Dictionary<Type, IList<ContainerRegistration>>();

        public Container()
        {
            Register<IResolveDependencies>(()=> this) ;
            Register<IRegisterDependencies>(() => this);
        }

        public void Register<T>()
        {
            Register(typeof(T));
        }

        public void Register<T>(Func<T> factory)
        {
            Register(typeof(T), () => factory());
        }

        public void Register<T>(Lifecycle lifecycle)
        {
            Register(typeof(T), lifecycle);
        }

        public void Register<T>(Lifecycle lifecycle, Func<T> factory)
        {
            Register(typeof(T), lifecycle, () => factory());
        }

        public void Register(Type type)
        {
            Register(type, Lifecycle.InstancePerCall);
        }

        public void Register(Type type, Lifecycle lifecycle)
        {
            var factory = DetermineFactory(type);

            Register(type, lifecycle, factory);
        }

        public void Register(Type type, Func<object> factory)
        {
            Register(type, Lifecycle.InstancePerCall, factory);
        }

        public void Register(Type type, Lifecycle lifecycle, Func<object> factory)
        {
            var registration = new ContainerRegistration
            {
                Type = type,
                Lifecycle = lifecycle,
                Factory = factory
            };

            foreach (var t in type.GetBaseTypes().Concat(new[] { type }))
            {
                if (!_registrations.ContainsKey(t))
                {
                    _registrations[t] = new List<ContainerRegistration>();
                }

                _registrations[t].Insert(0, registration);
            }

        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type type)
        {
            if (!_registrations.ContainsKey(type))
            {
                var defaultConstructor = type.GetConstructor(Type.EmptyTypes);
                return defaultConstructor != null ? Activator.CreateInstance(type) : null;
            }
            else
            {
                var registration = _registrations[type].First();
                return Instantiate(registration);
            }
        }


        public IEnumerable<T> ResolveAll<T>()
        {
            return ResolveAll(typeof(T)).Cast<T>();
        }

        public IEnumerable<object> ResolveAll(Type type)
        {
            if (!_registrations.ContainsKey(type)) yield break;

            foreach (var registration in _registrations[type])
            {
                yield return Instantiate(registration);
            }
        }

        private Func<object> DetermineFactory(Type type)
        {
            var constructor = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public)
                .OrderByDescending(c => c.GetParameters().Count())
                .FirstOrDefault();

            if (constructor == null)
                return () => Activator.CreateInstance(type);

            return () =>
            {
                var args = constructor.GetParameters().Select(p => Resolve(p.ParameterType)).ToArray();

                return Activator.CreateInstance(type, args, null);
            };

        }

        private static object Instantiate(ContainerRegistration registration)
        {
            if (registration.Lifecycle == Lifecycle.Singleton)
            {
                return registration.SingletonInstance ?? (registration.SingletonInstance = registration.Factory());
            }
            return registration.Factory();
        }
    }
}
