using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity.Configuration;

namespace Artech.UnityExtensions.Configuration
{
    public class AutoInterceptionConfigurationExtension : SectionExtension
    {
        public override void AddExtensions(SectionExtensionContext context)
        {
            context.AddElement<AutoInterceptionElement>("autoInterception");
            context.AddAlias<AutoInterception>("AutoInterception");
        }
    }
}
