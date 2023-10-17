#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;

namespace Atdl4net.Providers
{
    public class StrategyKey
    {
        private readonly Guid _key = Guid.NewGuid();

        public override string ToString()
        {
            return _key.ToString();
        }

        public static implicit operator string(StrategyKey key)
        {
            return key.ToString();
        }
    }
}
