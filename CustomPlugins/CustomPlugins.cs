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
				if (entity.GetAttributeValue<string>("orfium_custommusictype") == "") {
					entity["orfium_typeofmusic"] = "";
				} else {
					OptionSetValueCollection genres = new OptionSetValueCollection();
					foreach (var option in entity.GetAttributeValue<string>("orfium_custommusictype").Split(','))
					{
						genres.Add(new OptionSetValue(int.Parse(option)));
					}
					entity["orfium_typeofmusic"] = genres;
				}
			}
			else if (entity.Attributes.ContainsKey("orfium_typeofmusic"))
			{
				
				if (!(entity.Attributes.Contains("orfium_typeofmusic") && entity.Attributes["orfium_typeofmusic"] != null))
				{
					entity["orfium_custommusictype"] = "";
				} else
                {
					entity["orfium_custommusictype"] = string.Join(",", entity.GetAttributeValue<IEnumerable<OptionSetValue>>("orfium_typeofmusic").Select(o => o.Value.ToString()));
				}
			}
		}
    }
}
