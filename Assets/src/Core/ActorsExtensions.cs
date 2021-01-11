using Pixeye.Actors;
using UnityEngine;
using System.Runtime.CompilerServices;
using BigBiteStudios.Logging;
using System.Diagnostics;
using System;
using Debug = UnityEngine.Debug;

namespace ThePathfinder
{
//#if UNITY_EDITOR
#pragma warning disable
    public static class ActorsExtensions
    {
        //Processor
        [Conditional("UNITY_EDITOR")]
        public static void Log(this Processor context, string message, LogType logType = LogType.Log,
            [CallerMemberName] string memberName = "")
        {
            Log(context.GetType(), message, memberName, logType);
        }

        [Conditional("UNITY_EDITOR")]
        public static void Watch(this Processor context, string title, string value)
        {
            Watch(title, value);
        }

        [Conditional("UNITY_EDITOR")]
        public static void WatchFunction(this Processor context, string value, [CallerMemberName] string caller = "")
        {
            Watch(caller, value);
        }

        //Layer
        [Conditional("UNITY_EDITOR")]
        public static void Log(this Layer context, string message, LogType logType = LogType.Log,
            [CallerMemberName] string memberName = "")
        {
            Log(context.GetType(), message, memberName, logType);
        }

        [Conditional("UNITY_EDITOR")]
        public static void Watch(this Layer context, string title, string value)
        {
            Watch(title, value);
        }

        //Actor
        [Conditional("UNITY_EDITOR")]
        public static void Log(this Actor context, string message, LogType logType = LogType.Log,
            [CallerMemberName] string memberName = "")
        {
            Log(context.GetType(), message, memberName, logType);
        }

        public static void Watch(this Actor context, string title, string value)
        {
            Watch(title, value);
        }

        [Conditional("UNITY_EDITOR")]
        private static void Watch(string title, string value)
        {
            var msg = string.Concat("/set ", title, "= ", value);
            Debug.Log(msg);
        }

        [Conditional("UNITY_EDITOR")]
        private static void Log(Type type, string message, string memberName, LogType logType = LogType.Log)
        {
            var msg = string.Concat(Msg.CreateTags(type.Name, memberName), message);
            switch (logType)
            {
                case LogType.Warning:
                    Debug.LogWarning(msg);
                    break;
                case LogType.Error:
                    Debug.LogError(msg);
                    break;
                case LogType.Log:
                case LogType.Assert:
                case LogType.Exception:
                    Debug.Log(msg);
                    break;
            }
        }
    }

//#endif
}