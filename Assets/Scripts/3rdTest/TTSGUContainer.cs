using System.Collections.Generic;
using System.Xml.Serialization;
/// <summary>
/// Container for a list of Third test single guess units
/// </summary>

[XmlRoot("ThirdTest")]
public class TTSGUContainer {

    [XmlArray("Individual guess list")]
    public List<TTSGU> guesses = new List<TTSGU>();

}
