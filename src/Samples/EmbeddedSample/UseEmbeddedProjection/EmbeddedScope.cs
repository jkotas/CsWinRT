using System;
using TestEmbedded;
using Windows.Foundation;

namespace UseEmbeddedProjection
{
    public class EmbeddedScope
    {
        public Windows.Foundation.Collections.StringMap EmbeddedMapType { get; }

        public EmbeddedScope()
        {
            EmbeddedMapType = new();
            EmbeddedMapType["embedded"] = "scope";
        }

        public void EmbeddedAlpha(Windows.Foundation.Collections.StringMap strMap)
        {
            return;
        }
    }
}
