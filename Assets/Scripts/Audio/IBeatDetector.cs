using System.Collections.Generic;

namespace Audio
{
    public interface IBeatDetector
    {
        /// <summary>
        /// Return list of times beats 
        /// </summary>
        List<double> DetectBeats(string audioFilePath);
    }
}