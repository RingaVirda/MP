using System;

namespace Lab1
{
    public class TimeAT
    {
        public uint Seconds { get; private set; }
        public uint Minutes { get; private set; }
        public uint Hours { get; private set; }

        public TimeAT()
            => (Seconds, Minutes, Hours) = (0, 0, 0);

        public TimeAT(uint seconds, uint minutes, uint hours)
        {
            Seconds = (seconds < 60) ? seconds : throw new ValueException("Seconds is out of range");
            Minutes = (minutes < 60) ? minutes : throw new ValueException("Minutes is out of range");
            Hours = (hours < 24) ? hours : throw new ValueException("Hours is out of range");
        }

        public TimeAT(DateTime date)
        {
            Seconds = (uint) date.Second;
            Minutes = (uint) date.Minute;
            Hours = (uint) date.Hour;
        }

        public string Humaise()
            => (Hours > 12) 
                ? $"{Hours - 12}:{Minutes}:{Seconds} PM" 
                : $"{Hours}:{Minutes}:{Seconds} AM";

        public override string ToString()
        {
            return Humaise();
        }

        public TimeAT Add(TimeAT time)
        {
            var newTime = new TimeAT();
            
            newTime.Seconds = Seconds + time.Seconds;
            if (newTime.Seconds >= 60)
            {
                newTime.Seconds -= 60;
                newTime.Minutes += 1;
            }
            
            newTime.Minutes += Minutes + time.Minutes;
            if (newTime.Minutes >= 60)
            {
                newTime.Minutes -= 60;
                newTime.Hours += 1;
            }

            newTime.Hours += Hours + time.Hours;
            if (newTime.Hours >= 24)
            {
                newTime.Hours -= 24;
            }

            return newTime;
        }

        public static TimeAT operator +(TimeAT time0, TimeAT time1)
        {
            var newTime = new TimeAT();
            
            newTime.Seconds = time0.Seconds + time1.Seconds;
            if (newTime.Seconds >= 60)
            {
                newTime.Seconds -= 60;
                newTime.Minutes += 1;
            }
            
            newTime.Minutes += time0.Minutes + time1.Minutes;
            if (newTime.Minutes >= 60)
            {
                newTime.Minutes -= 60;
                newTime.Hours += 1;
            }

            newTime.Hours += time0.Hours + time1.Hours;
            if (newTime.Hours >= 24)
            {
                newTime.Hours -= 24;
            }

            return newTime;
        }

        public TimeAT Sub(TimeAT time)
        {
            var newTime = new TimeAT {Seconds = Seconds, Minutes = Minutes, Hours = Hours};
            if (newTime.Seconds < time.Seconds)
            {
                if (newTime.Minutes == 0)
                {
                    if (newTime.Hours == 0)
                    {
                        newTime.Hours = 23;
                    }

                    newTime.Minutes = 59;
                    newTime.Hours--;
                }
                newTime.Seconds += 60 - time.Seconds;
                newTime.Minutes--;
            }
            else
            {
                newTime.Seconds -= time.Seconds;
            }

            if (newTime.Minutes < time.Minutes)
            {
                if (newTime.Hours == 0)
                {
                    newTime.Hours = 23;
                }

                newTime.Minutes += 60 - time.Minutes;
                newTime.Hours--;
            }
            else
            {
                newTime.Minutes -= time.Minutes;
            }

            if (newTime.Hours < time.Hours)
            {
                newTime.Hours += 24 - time.Hours;
            }
            else
            {
                newTime.Hours -= time.Hours;
            }
            
            return newTime;
        }

        public static TimeAT operator -(TimeAT time0, TimeAT time1)
        {
            var newTime = new TimeAT {Seconds = time0.Seconds, Minutes = time0.Minutes, Hours = time0.Hours};
            if (newTime.Seconds < time1.Seconds)
            {
                if (newTime.Minutes == 0)
                {
                    if (newTime.Hours == 0)
                    {
                        newTime.Hours = 23;
                    }

                    newTime.Minutes = 59;
                }
                newTime.Seconds += 60 - time1.Seconds;
            }
            else
            {
                newTime.Seconds -= time1.Seconds;
            }

            if (newTime.Minutes < time1.Minutes)
            {
                if (newTime.Hours == 0)
                {
                    newTime.Hours = 23;
                }

                newTime.Minutes += 60 - time1.Minutes;
                newTime.Hours--;
            }
            else
            {
                newTime.Minutes -= time1.Minutes;
            }

            if (newTime.Hours < time1.Hours)
            {
                newTime.Hours += 24 - time1.Hours;
            }
            else
            {
                newTime.Hours -= time1.Hours;
            }
            
            return newTime;
        }
    }
}