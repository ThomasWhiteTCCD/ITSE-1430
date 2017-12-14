using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace ReflectionDemo {
    class Program {
        static void Main( string[] args )
        {
            //var typeName = "System.DateTime";

            //var dt = DateTime.Now;

            //Type type = dt.GetType();
            //DisplayType(type);
            //Console.ReadLine();

            // Load assembly
            var asm = FindAssembly("Nile.*");

            // Find controllers
            var controllers = FindControllers(asm);

            var route = "Product/List";

            var method = FindAction(controllers, route);
            
            // Create instance

            // Call action
        }

        static MethodInfo FindAction (IEnumerable<Type> controllers, string url)
        {
            var tokens = url.Split('/');
            var controller = tokens[0] + "Controller";
            var action = (tokens.Length > 1) ? tokens[1] : "Index";

            // Find controller
            var controllerType = controllers.FirstOrDefault(t => 
                                String.Compare(t.Name, controller, true) == 0);

            if (controllerType == null)
                return null;

            // Find method
            var flag = BindingFlags.IgnoreCase;
            var method = controllerType.GetMethod(action, flag);

            return method;
        }

        static Assembly FindAssembly (string prefix)
        {
            var files = Directory.GetFiles(Environment.CurrentDirectory, prefix);
            var file = files.FirstOrDefault();

            return Assembly.LoadFile(file);
        }

        static List<Type> FindControllers (Assembly asm)
        {
            // get all public types
            var types = asm.GetExportedTypes().OfType<Type>();

            // get creatable types
            types = from t in types
                    where !t.IsAbstract && t.IsClass
                    select t;

            // Derives from Controller
            types = from t in types
                    where t.IsSubclassOf(typeof(Controller))
                    select t;

            // Name ends with -Controlller
            types = from t in types
                    where t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)
                    select t;

            return types.ToList();
        }

        static void DisplayType( Type type )
        {
            Console.WriteLine($"Name = {type.Name}");
            Console.WriteLine($"FullName = {type.FullName}");
            Console.WriteLine($"Base Type = {type.BaseType}");
            Console.WriteLine($"Base Type = {type.CustomAttributes}");
        }
    }
}
