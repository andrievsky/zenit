using System;

namespace SharedCode.Extensions
{
    public static class ActionExtensions
    {
        public static void Dispatch(this Action act)
        {
            if (act == null)
            {
                return;
            }

            act();
        }
    }
}