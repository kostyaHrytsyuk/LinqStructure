using System;

namespace LinqStructure
{
    public sealed class LinqService
    {
        private static readonly Lazy<LinqService> lazyService = new Lazy<LinqService>(() => new LinqService());

        public static LinqService Service { get { return lazyService.Value; } }
                
        private LinqService()
        {

        }

    }
}
