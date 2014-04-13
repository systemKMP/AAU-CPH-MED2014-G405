using System.Collections.Generic;
using System.Xml.Serialization;

public class ResultData {

    [XmlArray("GuessAttempts")]
    public int[] guessAttempts = new int[6];

    [XmlArray("CorrectGuesses")]
    public int[] correctGuesses = new int[6];

}
