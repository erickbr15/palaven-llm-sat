﻿using Palaven.Model.Contracts;

namespace Palaven.Model.Domain;

public class Title : ITitle
{
    public Guid TitleId { get; set; }
    public Guid LawId { get; set; }
    public string Name { get; set; } = default!;
    public string? Topic { get; set; }
    public IList<ICorrelation> Correlations { get; set; } = new List<ICorrelation>();
    public IList<string> AdditionalInformation { get; set; } = new List<string>();
}
