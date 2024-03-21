using Liara.Pinecone.Model;

namespace Palaven.VectorIndexing.Commands;

public class GoldenArticleUpsertDataBuilder
{
    private UpsertDataModel? _target;

    public GoldenArticleUpsertDataBuilder NewWithNamespace(string dataNamespace)
    {
        _target = new UpsertDataModel
        {
            Namespace = dataNamespace,
            Vectors = new List<Vector>()
        };
        return this;
    }

    public GoldenArticleUpsertDataBuilder AddVector(IList<double> vector, Guid articleId, IList<string> llmFunctions)
    {
        var newVector = new Vector
        {
            Id = Guid.NewGuid().ToString(),
            Values = vector
        };

        newVector.Metadata.Add("articleId", articleId.ToString());
        newVector.Metadata.Add("llmfunctions", string.Join("|", llmFunctions));

        _target!.Vectors.Add(newVector);

        return this;
    }

    public UpsertDataModel? Build()
    {
        return _target;
    }
}
