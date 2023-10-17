#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Diagnostics;
using System.Linq;
using Atdl4net.Diagnostics;
using Atdl4net.Resources;

namespace Atdl4net.Utility
{
    public static class ProcessExtensions
    {
        private static readonly string ExceptionContext = "Atdl4net.Utility.ProcessExtensions";

        public static bool IsVSDesigner(this Process process)
        {
            if (process == null)
                throw ThrowHelper.New<NullReferenceException>(ExceptionContext, ErrorMessages.IllegalUseOfNullError);

            if (process.MainModule != null)
                return (process.MainModule.ModuleName.Contains("devenv.exe"));

            return false;
        }
    }
}
