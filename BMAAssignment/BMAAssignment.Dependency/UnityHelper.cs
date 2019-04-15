
namespace BMAAssignment.Dependency
{
    using BMAAssignment.Dao;
    using BMAAssignment.Interfaces.Dao;
    using BMAAssignment.Interfaces.Services;
    using BMAAssignment.Services;
    using Microsoft.Practices.Unity;

    public class UnityHelper
    {
        private static IUnityContainer _container;

        public static IUnityContainer Container
        {
            get
            {
                if (_container == null)
                {
                    _container = new UnityContainer();

                    ConfigureContainer(_container);
                }
                return _container;
            }
        }


        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        private static void ConfigureContainer(IUnityContainer container)
        {
            container.RegisterType<IAppointmentDao, AppointmentDao>();
            container.RegisterType<IAppointmentService, AppointmentService>();
            container.RegisterType<IEmployeeDao, EmployeeDao>();
            container.RegisterType<IEmployeeService, EmployeeService>();
        }


    }
}
