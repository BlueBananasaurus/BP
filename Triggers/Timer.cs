namespace Monogame_GL
{
    public class Timer
    {
        public float DelayMilisec { set; get; }
        public float ActualTimeLeft { set; get; }
        public bool Ready { set; get; }

        public Timer(float delayMilisec, bool startReady = false)
        {
            DelayMilisec = delayMilisec;
            if (startReady == true)
                ActualTimeLeft = 0;
            else
                ActualTimeLeft = DelayMilisec;
            Ready = true;
        }

        public void Reset()
        {
            ActualTimeLeft = DelayMilisec;
        }

        public void Update()
        {
            ActualTimeLeft -= Game1.Delta;

            if (ActualTimeLeft <= 0)
            {
                Ready = true;
            }
            else
            {
                Ready = false;
            }
        }
    }
}