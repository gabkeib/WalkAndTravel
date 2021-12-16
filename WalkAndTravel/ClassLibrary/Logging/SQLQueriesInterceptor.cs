using Microsoft.EntityFrameworkCore.Diagnostics;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary.Logging
{
    public class SQLQueriesInterceptor : DbCommandInterceptor
    {
        public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            Log.Information($"Command was called: {command.CommandText} and it took: {eventData.Duration.TotalMilliseconds}");
            return base.ReaderExecuted(command, eventData, result);
        }

     
    }
}
