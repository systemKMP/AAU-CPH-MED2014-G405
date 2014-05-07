using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("Guess list")]
public class ResultData {

    [XmlArray("Guess")]
    public List<Result> angleOffsets = new List<Result>();

}
