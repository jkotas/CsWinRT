using System;
using Windows.Storage;
namespace UseGlobalProjection
{
    public class GlobalScope
    {
        public Windows.Foundation.Collections.StringMap GlobalStringMap { get; }
        public GlobalScope()
        {
            GlobalStringMap = new();
            GlobalStringMap["global"] = "scope";
        }

        public void GlobalAlpha(Windows.Foundation.Collections.StringMap strMap)
        {
            return;
        }
    }
}
