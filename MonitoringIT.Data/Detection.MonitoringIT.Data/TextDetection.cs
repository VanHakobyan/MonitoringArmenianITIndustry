using SimMetricsApi;
using SimMetricsMetricUtilities;

namespace Detection.MonitoringIT.Data
{
    public class TextDetection
    {
        public double GetSimilarity(string str1, string str2, string type)
        {
            IStringMetric stringMetric;

            switch (type)
            {
                case AlgorithmTypes.BlockDistance:
                    stringMetric = new BlockDistance();
                    break;
                case AlgorithmTypes.ChapmanLengthDeviation:
                    stringMetric = new ChapmanLengthDeviation();
                    break;
                case AlgorithmTypes.ChapmanMeanLength:
                    stringMetric = new ChapmanMeanLength();
                    break;
                case AlgorithmTypes.CosineSimilarity:
                    stringMetric = new CosineSimilarity();
                    break;
                case AlgorithmTypes.DiceSimilarity:
                    stringMetric = new DiceSimilarity();
                    break;
                case AlgorithmTypes.EuclideanDistance:
                    stringMetric = new EuclideanDistance();
                    break;
                case AlgorithmTypes.JaccardSimilarity:
                    stringMetric = new JaccardSimilarity();
                    break;
                case AlgorithmTypes.Jaro:
                    stringMetric = new Jaro();
                    break;
                case AlgorithmTypes.JaroWinkler:
                    stringMetric = new JaroWinkler();
                    break;
                case AlgorithmTypes.Levenstein:
                    stringMetric = new Levenstein();
                    break;
                case AlgorithmTypes.MatchingCoefficient:
                    stringMetric = new MatchingCoefficient();
                    break;
                case AlgorithmTypes.MongeElkan:
                    stringMetric = new MongeElkan();
                    break;
                case AlgorithmTypes.NeedlemanWunch:
                    stringMetric = new NeedlemanWunch();
                    break;
                case AlgorithmTypes.OverlapCoefficient:
                    stringMetric = new OverlapCoefficient();
                    break;
                case AlgorithmTypes.QGramsDistance:
                    stringMetric = new QGramsDistance();
                    break;
                case AlgorithmTypes.SmithWaterman:
                    stringMetric = new SmithWaterman();
                    break;
                case AlgorithmTypes.SmithWatermanGotoh:
                    stringMetric = new SmithWatermanGotoh();
                    break;
                case AlgorithmTypes.SmithWatermanGotohWindowedAffine:
                    stringMetric = new SmithWatermanGotohWindowedAffine();
                    break;
                default:
                    stringMetric = new SmithWatermanGotoh();
                    break;
            }

            var similarity = stringMetric.GetSimilarity(str1.Trim(), str2.Trim());
            return similarity;
        }
    }
}