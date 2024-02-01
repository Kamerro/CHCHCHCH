using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CHCHCHCH
{
    internal class GameServiceUpdater
    {
        internal void GameServiceUpdateMousePosition(List<CustomButton> listOfButtons)
        {

            foreach (var obj in listOfButtons)
                    obj.Update();
        }
    }
}