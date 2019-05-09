using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB3_SocialNetwork.Models;
using Newtonsoft.Json.Linq;

namespace DAB3_SocialNetwork
{
    public class DetailJsonConverter : JsonCreationConverter<Detail>
    {
        protected override Detail Create(Type objectType, JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException("jObject");

            if (jObject["Text"] != null)
            {
                return new DetailText();
            }
            else if (jObject["PathToImage"] != null)
            {
                return new DetailImage();
            }
            else
            {
                return new Detail();
            }
        }
    }
}
