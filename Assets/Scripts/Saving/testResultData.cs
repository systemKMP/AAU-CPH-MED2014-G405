using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("Total Result")]
public class testResultData
{
    [XmlArray("Time used to reach next room")]
    public List<testResult> timeForNextRoom = new List<testResult>();
}
