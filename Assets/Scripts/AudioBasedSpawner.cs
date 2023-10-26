using System;
using System.Collections.Generic;
using NAudio.Dsp;
using NAudio.Wave;
using UnityEngine;
using Game.Player;

namespace Audio
{
    public class AudioBasedSpawner : MonoBehaviour
    {
        public PlayerConfig PlayerConfig;
        public Transform Player;
        
        private void Awake()
        {
            Vector3 playerStartPosition = Player.position;

            IBeatDetector beatDetector = new BeatDetectorBase();
            string audioFilePath = "./Assets/test2.mp3"; // Замените на путь к вашему аудиофайлу

            List<double> beatTimes = beatDetector.DetectBeats(audioFilePath);

            Debug.Log(beatTimes.Count);
            
            // if(beatTimes.Count > 2000) return;

            for (int i = 0; i < beatTimes.Count; i++)
            {
                Debug.Log(beatTimes[i]);
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //S = V * T
                sphere.transform.position = new Vector3(((float)beatTimes[i] * PlayerConfig.VerticalPlayerSpeed), 0, playerStartPosition.z);
            }
        }

        private void Start()
        {
            
        }
    }

    public interface IBeatDetector
    {
        List<double> DetectBeats(string audioFilePath);
    } 
    
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