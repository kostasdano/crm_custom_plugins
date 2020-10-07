using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Microsoft.Xrm.Sdk;

namespace CustomPlugins
{
    public class CustomPlugins : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
			var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

			var entity = context.InputParameters["Target"] as Entity;

			if (entity.Attributes.ContainsKey("orfium_custommusictype"))
			{
				OptionSetValueCollection genres = new OptionSetValueCollection();
				foreach (var option in entity.GetAttributeValue<string>("orfium_custommusictype").Split(','))
				{
					genres.Add(new OptionSetValue(int.Parse(option)));
				}

				entity["orfium_typeofmusic"] = genres;
			}
		}
    }
}
