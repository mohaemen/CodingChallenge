using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;
        private readonly Dictionary<Color, int> _colorCount = new Dictionary<Color, int>();
        private readonly Dictionary<Size, int> _sizeCount = new Dictionary<Size, int>();

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;

            Color.All.ForEach(c => _colorCount.Add(c, 0));
            Size.All.ForEach(s => _sizeCount.Add(s, 0));
        }


        public SearchResults Search(SearchOptions options)
        {
            List<Shirt> shirts = new List<Shirt>();

            foreach (Shirt shirt in _shirts)
            {
                if ((!options.Colors.Any() || options.Colors.Contains(shirt.Color)) &&
                    (!options.Sizes.Any() || options.Sizes.Contains(shirt.Size)))
                {
                    shirts.Add(shirt);

                    _colorCount[shirt.Color]++;
                    _sizeCount[shirt.Size]++;
                }
            }

            return new SearchResults
            {
                Shirts = shirts,
                SizeCounts = _sizeCount.Select(sc => new SizeCount { Size = sc.Key, Count = sc.Value }).ToList(),
                ColorCounts = _colorCount.Select(cc => new ColorCount { Color = cc.Key, Count = cc.Value }).ToList()
            };
        }
    }
}