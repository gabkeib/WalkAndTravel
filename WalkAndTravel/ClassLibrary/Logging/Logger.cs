using Castle.DynamicProxy;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary.Logging
{
    public class Logger : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                invocation.Proceed();
                sw.Stop();
     
                Log.Logger.Information($"{ invocation.TargetType.Name}, " + $"Method {invocation.Method.Name} " + $"Which is declared in {invocation.Method.DeclaringType}, took {sw.ElapsedMilliseconds}ms and was" +
                    $"called with these parameters: {JsonConvert.SerializeObject(invocation.Arguments, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}" + $"and returned value type of: {invocation.Method.ReturnType}");
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"Error happened in method: {invocation.Method}. Error: {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }
    }
}
