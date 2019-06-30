using System.Speech.Synthesis;

namespace GTAPanicButton
{
    class SoundHandler
    {
        public SpeechSynthesizer speech;
        
        public SoundHandler()
        {
            speech = new SpeechSynthesizer();
            speech.SetOutputToDefaultAudioDevice();
        }

        public void PlayBeep(bool high)
        {
            if(high)
                System.Console.Beep(400, 100);
            else
                System.Console.Beep(200, 100);
        }
    }
}
