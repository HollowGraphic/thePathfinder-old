using System;
namespace BigBiteStudios.Logging
  {
    public static class Msg
    {
        private const string WatchCommand = "\nCPAPI:{\"cmd\":\"Watch\" \"name\":\"";
      public static string CreateTags(params string[] tags)
      {
        string extractedTags = String.Empty;

        foreach (var tag in tags)
        {
          extractedTags = String.Concat(extractedTags, ",", tag);
        }
        return String.Concat("[", extractedTags, "]");
      }

      public static string Watch(string watchName, object value)
      {
        string msg = String.Concat("/set ", watchName, " = ", value.ToString());
        return msg;
      }

        public static string BuildWatch(string title, string value)
        {
            return String.Concat(title, " : ", value, WatchCommand, title, "\"}");
        }
    }
  }