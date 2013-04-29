using System;
using System.Collections.Generic;

namespace Nest
{
  using Nest.Domain.Settings;

  public class SimilaritySettings
  {
    public string BaseSimilarity { get; private set; }
    
    public List<CustomSimilaritySettings> CustomSimilarities { get; private set; }

    public SimilaritySettings()
    {
      CustomSimilarities = new List<CustomSimilaritySettings>();
    }

    public SimilaritySettings(string baseSimilarity) : this()
    {
      BaseSimilarity = baseSimilarity;
    }
  }
}
