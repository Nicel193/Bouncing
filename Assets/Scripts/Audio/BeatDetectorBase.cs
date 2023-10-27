using System.Collections.Generic;
using NAudio.Wave;

namespace Audio
{
    public class BeatDetectorBase : IBeatDetector
    {
        private static double Threshold = 0.2f; // Adjust as needed
        private static double SamplingRate = 44100; // Adjust to match your audio

        public List<double> DetectBeats(string audioFilePath)
        {
            List<double> beatTimes = new List<double>();

            using (var reader = new AudioFileReader(audioFilePath))
            {
                // SamplingRate = reader.WaveFormat.SampleRate;
                
                int bufferSize = 1024;
                var buffer = new float[bufferSize];
                int read;
                double lastEnergy = 0;

                while ((read = reader.Read(buffer, 0, bufferSize)) > 0)
                {
                    double energy = 0;
                    for (int i = 0; i < read; i++)
                    {
                        energy += buffer[i] * buffer[i];
                    }

                    energy /= read;
                    if (energy > lastEnergy && energy > Threshold)
                    {
                        // Beat detected, convert sample position to seconds
                        double beatTimeInSeconds =
                            reader.CurrentTime.TotalSeconds + (double) reader.Position / SamplingRate;
                        beatTimes.Add(reader.CurrentTime.TotalSeconds);
                    }

                    lastEnergy = energy;
                }
            }

            return beatTimes;
        }
    }
}