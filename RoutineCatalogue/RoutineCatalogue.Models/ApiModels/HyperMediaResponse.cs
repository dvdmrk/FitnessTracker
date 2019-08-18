using RoutineCatalogue.Models.Types;
using System;
using System.Collections.Generic;
namespace RoutineCatalogue.Models.ApiModels
{
    public class HyperMediaResponse<T>
    {
        public List<HyperMedia<T>> HyperMedia;
        public HyperMediaResponse(Guid? id = null)
        {
            HyperMedia = new List<HyperMedia<T>>();
            if (typeof(T).Name != "Set") HyperMedia.Add(createHyperMedia("GET ALL", "GET"));
            foreach (var action in Enum.GetNames(typeof(HyperMediaType)))
            {
                if (id == null && action != "Post") continue;
                HyperMedia.Add(createHyperMedia(action, action, id));
            }
        }
        private HyperMedia<T> createHyperMedia(string rel, string action, Guid? id = null)
        {
            var media = new HyperMedia<T>
            {
                Rel = rel + " " + typeof(T).Name,
                Href = "/api/" + typeof(T).Name + "/" + (id == Guid.Empty ? "" : id.ToString()),
                Action = action.ToString(),
            };
            return media;
        }
    }
}
