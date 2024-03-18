using System;

public static class EventHelper
{
    public static void Raise(object sender, EventHandler eventHandler)
    {
        if (eventHandler != null)
        {
            eventHandler(sender, EventArgs.Empty);
        }
    }

    public static void Raise<T>(object sender, EventHandler eventHandler, T eventArgs) where T : EventArgs
    {
        if (eventHandler != null)
        {
            eventHandler(sender, eventArgs);
        }
    }
}