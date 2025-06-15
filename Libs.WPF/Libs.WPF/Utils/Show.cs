using Libs.WPF.Controls.Windows;
using System;
using System.Windows;

namespace Libs.WPF.Utils
{
    public class Show
    {
        public static void Info(object ob, bool isShortMsg = true, Window owner = null)
        {
            new DialogWindow(ob.ToString(), DialogType.Information, isShortMsg, owner).ShowDialog();
        }

        public static void Warning(object ob, bool isShortMsg = true, Window owner = null)
        {
            new DialogWindow(ob.ToString(), DialogType.Warning, isShortMsg, owner).ShowDialog();
        }

        public static void Error(object ob, bool isShortMsg = true, Window owner = null)
        {
            new DialogWindow(ob.ToString(), DialogType.Error, isShortMsg, owner).ShowDialog();
        }

        public static void ElapsedTime(DateTime startTime, Window owner = null)
        {
            new DialogWindow((DateTime.Now - startTime).ToString(@"hh\:mm\:ss\.fff"), DialogType.Information, owner: owner).Show();
        }
    }
}