﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Melanchall.DryWetMidi.Smf.Interaction
{
    public static class LengthedObjectUtilities
    {
        #region Methods

        public static TLength LengthAs<TLength>(this ILengthedObject obj, TempoMap tempoMap)
            where TLength : ILength
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            if (tempoMap == null)
                throw new ArgumentNullException(nameof(tempoMap));

            return LengthConverter.ConvertTo<TLength>(obj.Length, obj.Time, tempoMap);
        }

        public static IEnumerable<TObject> StartAtTime<TObject>(this IEnumerable<TObject> objects, long time)
            where TObject : ILengthedObject
        {
            return AtTime(objects, time, LengthedObjectPart.Start);
        }

        public static IEnumerable<TObject> EndAtTime<TObject>(this IEnumerable<TObject> objects, long time)
            where TObject : ILengthedObject
        {
            return AtTime(objects, time, LengthedObjectPart.End);
        }

        public static IEnumerable<TObject> StartAtTime<TObject>(this IEnumerable<TObject> objects, ITime time, TempoMap tempoMap)
            where TObject : ILengthedObject
        {
            return AtTime(objects, time, tempoMap, LengthedObjectPart.Start);
        }

        public static IEnumerable<TObject> EndAtTime<TObject>(this IEnumerable<TObject> objects, ITime time, TempoMap tempoMap)
            where TObject : ILengthedObject
        {
            return AtTime(objects, time, tempoMap, LengthedObjectPart.End);
        }

        public static IEnumerable<TObject> AtTime<TObject>(this IEnumerable<TObject> objects, long time, LengthedObjectPart matchBy)
            where TObject : ILengthedObject
        {
            if (objects == null)
                throw new ArgumentNullException(nameof(objects));

            if (time < 0)
                throw new ArgumentOutOfRangeException(nameof(time), time, "Time is negative.");

            return objects.Where(o => IsObjectAtTime(o, time, matchBy));
        }

        public static IEnumerable<TObject> AtTime<TObject>(this IEnumerable<TObject> objects, ITime time, TempoMap tempoMap, LengthedObjectPart matchBy)
            where TObject : ILengthedObject
        {
            if (objects == null)
                throw new ArgumentNullException(nameof(objects));

            if (time == null)
                throw new ArgumentNullException(nameof(time));

            if (tempoMap == null)
                throw new ArgumentNullException(nameof(tempoMap));

            var convertedTime = TimeConverter.ConvertFrom(time, tempoMap);
            return AtTime(objects, convertedTime, matchBy);
        }

        private static bool IsObjectAtTime<TObject>(TObject obj, long time, LengthedObjectPart matchBy)
            where TObject : ILengthedObject
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            if (time < 0)
                throw new ArgumentOutOfRangeException(nameof(time), time, "Time is negative.");

            var startTime = obj.Time;
            if (startTime == time && (matchBy == LengthedObjectPart.Start || matchBy == LengthedObjectPart.Entire))
                return true;

            var endTime = startTime + obj.Length;
            if (endTime == time && (matchBy == LengthedObjectPart.End || matchBy == LengthedObjectPart.Entire))
                return true;

            return matchBy == LengthedObjectPart.Entire && time >= startTime && time <= endTime;
        }

        #endregion
    }
}
